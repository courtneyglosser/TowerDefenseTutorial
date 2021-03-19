using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    public Vector2 WorldPosition {
        get{
            float xOffset = GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float yOffset = GetComponent<SpriteRenderer>().bounds.size.y / 2;
            float x = transform.position.x + xOffset;
            float y = transform.position.y - yOffset;
            return new Vector2 (x, y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup (Point gridPos, Vector3 worldPos, Transform parent) {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        // Using Singleton pattern to access LevelManager remotely.
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }
}
