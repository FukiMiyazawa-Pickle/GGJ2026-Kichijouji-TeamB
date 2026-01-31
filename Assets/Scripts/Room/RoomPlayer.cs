using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomPlayer : MonoBehaviour
{
    public List<RoomObject> roomObjects;

    Gamepad gamepad;
    new Rigidbody2D rigidbody;

    void Awake() {
        if (roomObjects.Count > 0)
            Instantiate(roomObjects[Random.Range(0, roomObjects.Count)], transform);

        gamepad = Gamepad.current;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(gamepad.leftStick.value * 50f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
