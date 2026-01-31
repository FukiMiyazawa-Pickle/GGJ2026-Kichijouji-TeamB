using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCtrl : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 5f;

	private Vector3 velocity;
	private Rigidbody2D rb;
	private Vector2 moveInput;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		rb.linearVelocity = moveInput * _moveSpeed;
	}

	// Input System�p
	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}

	public void OnAttack(InputAction.CallbackContext context)
	{

	}
}
