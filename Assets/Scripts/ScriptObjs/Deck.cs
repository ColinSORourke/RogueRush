using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : ScriptableObject
{
    public TowerCard[] theDeck = new TowerCard[10];

    public List<TowerCard> ShuffleTowers(){
        List<TowerCard> ShuffleResult = new List<TowerCard>();

        int[] shuffleOrder = { 0,1,2,3,4,5,6,7,8,9 };
        int i = 9;
        while (i >= 1){
            int j = Random.Range(0,i+1);
            if (j != i){
                int temp = shuffleOrder[i];
                shuffleOrder[i] = shuffleOrder[j];
                shuffleOrder[j] = temp;
            }
            i -= 1;
        }
        while (i < 10){
            TowerCard added = theDeck[ shuffleOrder[i] ];
            ShuffleResult.Add(added);
            i += 1;
        }
        return ShuffleResult;
    }
}
