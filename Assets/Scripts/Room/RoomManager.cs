using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomManager : MonoBehaviour
{
    public List<Sprite> wallSprites;
    public List<RoomObject> wallObjects;
    public List<Sprite> floorSprites;
    public List<RoomObject> floorObjects;

    public SpriteRenderer wall;
    public SpriteRenderer floor;

    private void Awake() {
        if (wallSprites.Count > 0) {
            wall.sprite = wallSprites[Random.Range(0, wallSprites.Count)];
            wall.transform.position = new Vector2(Random.Range(-18f, 18f), Random.Range(6f, 8f));
        }

        if (wallObjects.Count > 0)
            Instantiate(wallObjects[Random.Range(0, wallObjects.Count)], new Vector2(Random.Range(-16f, 16f), Random.Range(1.6f, 2.6f)), Quaternion.identity, transform);

        if (floorSprites.Count > 0) {
            floor.sprite = floorSprites[Random.Range(0, floorSprites.Count)];
            floor.transform.position = new Vector2(Random.Range(-11f, 11f), Random.Range(-0.5f, -6f));
        }

        if (floorObjects.Count > 0) {
            int n = Random.Range(7, 10);
            Vector2 positino;
            for (int i = 0; i < n; ++i) {
                do {
                    positino = new Vector2(Random.Range(-17f, 17f), Random.Range(2f, -10f));
                } while (Physics2D.OverlapCircle(positino, 0.3f));
                Instantiate(floorObjects[Random.Range(0, floorObjects.Count)], positino, Quaternion.identity, transform);
            }
        }
    }
}
