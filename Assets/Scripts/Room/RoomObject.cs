using UnityEngine;

public class RoomObject : MonoBehaviour {
    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
