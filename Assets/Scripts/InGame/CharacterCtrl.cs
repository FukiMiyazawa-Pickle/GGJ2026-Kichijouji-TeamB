using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCtrl : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private List<Sprite> _hideSprites;

	private Vector3 velocity;
	private Rigidbody2D rb;
	private Vector2 moveInput;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();

		ChangeRandomSprite();
    }

	private void FixedUpdate()
	{
		rb.linearVelocity = moveInput * _moveSpeed;
	}

	private void ChangeRandomSprite()
	{
		var index = UnityEngine.Random.Range(0, _hideSprites.Count);

        _spriteRenderer.sprite = _hideSprites[index];
    }

	// Input System
	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{

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
