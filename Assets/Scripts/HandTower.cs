using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTower : MonoBehaviour
{
    public TowerCard myCard;
    public int costModifier;

    public PlayerStateLevel psl;

    // Start is called before the first frame update
    void Start()
    {
        this.displayCard();
    }

    public void displayCard(){
        this.transform.Find("CardArt").GetComponent<SpriteRenderer>().sprite = myCard.cardArt;
        Transform cardText = this.transform.Find("CardText");
        cardText.Find("TextBox").GetComponent<TMPro.TextMeshProUGUI>().text = myCard.cardText;
        cardText.Find("NameBox").GetComponent<TMPro.TextMeshProUGUI>().text = myCard.cardName;
        cardText.Find("Cost").GetComponent<TMPro.TextMeshProUGUI>().text = myCard.energyCost.ToString();
        cardText.Find("Power").GetComponent<TMPro.TextMeshProUGUI>().text = myCard.power.ToString();
        cardText.Find("Durability").GetComponent<TMPro.TextMeshProUGUI>().text = myCard.durability.ToString();

        psl = this.transform.parent.parent.GetComponent<PlayerStateLevel>();
    }

    public int canBePlayed(){
        if (psl.prepPhase){
            if (myCard.energyCost + costModifier <= psl.energy){
                return 1;
            } else {
                return -1;
            }
        } else {
            return -2;
        }
    }

    public void play(){
        psl.spendEnergy(myCard.energyCost + costModifier);
        psl.playedTower();
    }
}
