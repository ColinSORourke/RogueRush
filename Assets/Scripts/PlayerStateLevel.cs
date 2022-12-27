using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateLevel : MonoBehaviour
{
    public int energy;
    int maxEnergy;
    public List<TowerCard> deck;
    public bool prepPhase = true;
    public int handSize;

    public float waveTimer;

    public GameObject cardPrefab;
    public Canvas playerUI;
    
    TextMeshProUGUI manaText;
    Button endTurnButton;

    

    // Start is called before the first frame update
    void Start()
    {
        // deck = GameManager.playerDeck.ShuffleTowers;
        manaText = playerUI.transform.Find("ManaCounter").Find("ManaCountText").GetComponent<TMPro.TextMeshProUGUI>();
        endTurnButton = playerUI.transform.Find("Button").GetComponent<Button>();

        this.draw();
        this.draw();
        this.draw();
        energy = 3;
        maxEnergy = 3;
    }

    void Update(){
        if (!prepPhase){
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0.0f){
                this.newPrepTurn();
            }
        }
    }

    public void draw(){
        if (deck.Count != 0){
            foreach (Transform child in this.transform.Find("Hand")){
                child.localPosition -= new Vector3(0.75f, 0.0f, 0.0f);
            }

            TowerCard drawnCard = deck[0];
            deck.RemoveAt(0);
            GameObject newCard = Instantiate(cardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            newCard.transform.SetParent(this.transform.Find("Hand"));
            newCard.transform.localPosition = new Vector3(0.75f * handSize, 0.0f, 0.0f);
            HandTower cardInHand = newCard.GetComponent<HandTower>();
            cardInHand.myCard = drawnCard;
            cardInHand.displayCard();
            handSize += 1;
        }
    }

    public void playedTower(){
        handSize -= 1;
        foreach (Transform child in this.transform.Find("Hand")){
            if (child.localPosition.x < 0){
                child.localPosition += new Vector3(0.75f, 0.0f, 0.0f);
            } else {
                child.localPosition -= new Vector3(0.75f, 0.0f, 0.0f);
            }
        }
    }

    public void spendEnergy(int spent){
        energy -= spent;
        manaText.text = energy.ToString();
    }

    public void newPrepTurn(){
        foreach (Transform child in this.transform.Find("ActiveTowers")){
            child.GetComponent<LiveTower>().tickDurability();
        }


        this.draw();
        prepPhase = true;
        maxEnergy += 1;
        energy = maxEnergy;
        manaText.text = energy.ToString();
        endTurnButton.interactable = true;
    }

    public void endPrepTurn(){
        prepPhase = false;
        waveTimer = 10.0f;
    }
}
