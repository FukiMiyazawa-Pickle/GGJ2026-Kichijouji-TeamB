using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
	private int playerCount = 0;
	public void OnPlayerJoined(PlayerInput player)
	{
		playerCount++;
	}
}
