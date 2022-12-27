using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveTower : Draggable
{
    public TowerCard myTower;
    public int powerMod = 0;
    public int currDur;

    Transform rangeCircle;

    // Start is called before the first frame update
    void Start()
    {
        this.displayCard();
    }

    public void displayCard(){
        currDur = myTower.durability;
        this.transform.Find("CardArt").GetComponent<SpriteRenderer>().sprite = myTower.cardArt;
        Transform stats = this.transform.Find("TowerStats");
        stats.Find("Power").GetComponent<TMPro.TextMeshProUGUI>().text = myTower.power.ToString();
        stats.Find("Durability").GetComponent<TMPro.TextMeshProUGUI>().text = currDur.ToString();

        rangeCircle = this.transform.Find("Range");
        rangeCircle.localScale = new Vector3(myTower.range, myTower.range, 1.0F);
    }

    public void displayRange(bool value){
        rangeCircle.gameObject.SetActive(value);
    }

    public override void play(){
        dragMode = false;
        this.displayRange(false);
    }

    void OnMouseOver(){
        this.displayRange(true);
    }

    void OnMouseExit(){
        if (!dragMode){
            this.displayRange(false);
        }      
    }

    public void tickDurability(){
        this.currDur -= 1;
        this.transform.Find("TowerStats").Find("Durability").GetComponent<TMPro.TextMeshProUGUI>().text = currDur.ToString();
        if (currDur == 0){
            this.occupy(false);
            GameObject.Destroy(this.transform.gameObject);
        }
    }
}
