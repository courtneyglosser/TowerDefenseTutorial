using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    public float TileSize {
        get {
            // ASSERT:  Assume all tiles are the same size.
            return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel() {
        string[] mapData = ReadLevelText();

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        // Calculates world start point, and sets it at the top left.  (0,0) would be bottom left.
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapYSize; y++) {
            char[] rowTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapXSize; x++) {
                string tileType = rowTiles[x].ToString();
                maxTile = PlaceTile(tileType, x, y, worldStart);
            }
        }

        Vector3 limit = new Vector3(maxTile.x + TileSize, maxTile.y - TileSize);

        cameraMovement.SetLimits(limit);
    }

    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart) {
        int tileIndex = int.Parse(tileType);
        // Creates a new tile based on prefab assigned in Unity Editor.
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        // Positions the new tile based on TileSize information and x,y coords.
        float xf = worldStart.x + (TileSize * x);
        float yf = worldStart.y - (TileSize * y);
        newTile.transform.position = new Vector3(xf, yf, 0);
        return newTile.transform.position;
    }

    private string[] ReadLevelText() {
        TextAsset fileData = Resources.Load("Level") as TextAsset;

        string data = fileData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
