using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour
{
    [SerializeField]
    private TileScript goal;
    [SerializeField]
    private TileScript start;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
    }

    private void ClickTile() {
        if (Input.GetMouseButtonDown(1) ) {
            // ASSERT:  1 = "right click"
            Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction);

            if (hit.collider != null) {
                // ASSERT:  Mouse click was "on" something.  What was it?
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if (tmp != null) {
                    // ASSERT:  We only get a TileScript component if we click a
                    // tile.

                    if (start == null) {
                        start = tmp;
                        start.Debugging = true;
                        start.SpriteRenderer.color = new Color32(132, 132, 255, 255);
                    }
                    else if (goal == null) {
                        goal = tmp;
                        goal.Debugging = true;
                        goal.SpriteRenderer.color = new Color32(132, 255, 132, 255);
                    }

                }
            }
        }
    }
}
