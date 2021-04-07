using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {
    public Point GridPosition { get; private set; }
    public bool IsEmpty { get; set; }

    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    public SpriteRenderer SpriteRenderer { get; set; }
    public bool Debugging { get; set; }

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
    void Start() {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Setup (Point gridPos, Vector3 worldPos, Transform parent) {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        IsEmpty = true;
        // Using Singleton pattern to access LevelManager remotely.
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver() {
        bool isClickingBtn = EventSystem.current.IsPointerOverGameObject();
        bool hasClickedBtn = GameManager.Instance.ClickedBtn != null;

        if (!isClickingBtn && hasClickedBtn) {
            // ASSERT:  Pointer is not over a tower purchase button, and
            // a tower has been selected for placement.
            if (IsEmpty && !Debugging) {
                ColorTile(emptyColor);
            }
            if (!IsEmpty && !Debugging) {
                ColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0) && IsEmpty) {
                PlaceTower();
            }
        }
    }

    private void OnMouseExit() {
        if (!Debugging) {
            ColorTile(Color.white);
        }
    }

    private void PlaceTower() {
        Debug.Log("(" + GridPosition.X + ", " + GridPosition.Y + ")");

        GameObject prefab = GameManager.Instance.ClickedBtn.TowerPrefab;
        Vector2 pos = transform.position;
        Quaternion rotation = Quaternion.identity;
        
        GameObject tower = (GameObject)Instantiate(prefab, pos, rotation);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);

        IsEmpty = false;
        ColorTile(Color.white);

        GameManager.Instance.BuyTower();
    }

    private void ColorTile(Color32 newColor) {
        SpriteRenderer.color = newColor;
    }
}
