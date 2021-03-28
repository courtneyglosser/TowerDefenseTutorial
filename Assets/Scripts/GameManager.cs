using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager> {
    public TowerBtn ClickedBtn {get; set;}

    private int currency;

    public int Currency {
        get {
            return currency;
        }
        set {
            this.currency = value;
            this.currencyTxt.text = "<color=lime>$</color>" + value.ToString();
        }
    }

    [SerializeField]
    private Text currencyTxt;

    // Start is called before the first frame update
    void Start() {
        Currency = 5;
    }

    // Update is called once per frame
    void Update() {
        HandleEscape();
    }

    public void PickTower(TowerBtn towerBtn) {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }

    public void BuyTower() {
        this.ClickedBtn = null;
        Hover.Instance.Deactivate();
    }

    private void HandleEscape() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Hover.Instance.Deactivate();
        }
    }
}
