using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static CharacterCtrl;
using static UnityEngine.EventSystems.EventTrigger;

public class CharacterCtrl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int _hp = 3;
    [SerializeField] private float _attackRange = 1.2f;
    [SerializeField] private float _hitRadius = 0.4f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _attackEffectPrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private List<Sprite> _hideSprites;

    [SerializeField, Tooltip("再生中確認用")]
    private string currentActionMapName;

    public enum CharacterType
	{
		Hide = 0,	// 隠れる人
		Seek,		// 鬼
	}

	private Vector3 velocity;
	private Vector2 moveInput;

    private Vector2 facingDirection = Vector2.right;
    private CharacterType characterType = CharacterType.Hide;


    private void Awake()
	{
		Debug.Log($"CurrentAction：{_playerInput.currentActionMap}\n Active：{_playerInput.currentActionMap.enabled}");

		ChangeRandomSprite();
    }

    private void Start()
    {
        if (_playerInput.currentActionMap != null)
        {
            currentActionMapName = _playerInput.currentActionMap.name;
        }
        else
        {
            currentActionMapName = "None";
        }
    }

    private void FixedUpdate()
	{
        _rb.linearVelocity = moveInput * _moveSpeed;
	}

	private void ChangeRandomSprite()
	{
		var index = UnityEngine.Random.Range(0, _hideSprites.Count);

        _spriteRenderer.sprite = _hideSprites[index];
    }

    public CharacterType GetPlayerType()
    {
        return characterType; 
    }

	public void ChangePlayerType(CharacterType type)
	{
        characterType = type;
        _playerInput.SwitchCurrentActionMap(type == CharacterType.Hide ? "Hide" : "Seek");
	}

    private void Attack(Vector2 dir)
    {
        // Rigidbody2D.position を基準にする
        Vector2 basePos = _rb.position;

        // AttackPoint のローカル位置をワールド方向へ変換
        Vector2 localOffset = _attackPoint.localPosition;
        Vector2 worldOffset = transform.TransformDirection(localOffset);

        // 最終的な攻撃中心
        Vector2 attackCenter =
            basePos +
            worldOffset +
            facingDirection * _attackRange;

        // --- 当たり判定 ---
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackCenter,
            _hitRadius,
            _enemyLayer
        );

        bool isPlaySe = false;

        foreach (var hit in hits)
        {
            if(hit.gameObject == _spriteRenderer.gameObject)
            {
                continue;
            }
            hit.gameObject.transform.parent.GetComponent<CharacterCtrl>()?.TakeDamage(1);

            if(!isPlaySe)
            {
                SoundManager.Instance.PlaySE("Hit");
                isPlaySe = true;
            }
        }

        SpawnAttackEffect(attackCenter, facingDirection, _hitRadius);
    }

    private void TakeDamage(int damage)
    {
        _hp--;

        if(_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void SpawnAttackEffect(Vector2 center, Vector2 dir, float radius)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        GameObject effect = Instantiate(
            _attackEffectPrefab,
            new Vector3(center.x, center.y, 0f),
            rot
        );

        SpriteRenderer sr = effect.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float diameter = radius * 2f;

            // 半円サイズ（横半分）
            sr.size = new Vector2(diameter, diameter);

            // 半円を前方にずらす
            effect.transform.position +=
                (Vector3)(dir.normalized * radius * 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        if (_rb == null || _attackPoint == null) return;

        // Rigidbody2D.position を基準
        Vector2 basePos = _rb.position;

        // AttackPoint のローカル → ワールド方向
        Vector2 localOffset = _attackPoint.localPosition;
        Vector2 worldOffset = transform.TransformDirection(localOffset);

        Vector2 attackCenter =
            basePos +
            worldOffset +
            facingDirection * _attackRange;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, _hitRadius);
    }


    // Input System
    public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude > 0.01f)
        {
            facingDirection = moveInput.normalized;
        }
    }

	public void OnAttack(InputAction.CallbackContext context)
	{
        if (!context.performed)
        {
            return;
        }

        Attack(facingDirection);
	}

	public void OnChange(InputAction.CallbackContext context)
	{
		if(!context.performed)
		{
			return;
		}

        ChangeRandomSprite();
    }
}
