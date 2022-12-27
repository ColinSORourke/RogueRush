using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerCard", menuName = "ScriptableObjects/TowerCard", order = 1)]
public class TowerCard : ScriptableObject
{
    // Values displayed on card
    public int energyCost = 1;
    public int power = 1;
    public int durability = 1;
    public string cardName = "Default";
    [TextAreaAttribute(3,5)]
    public string cardText = "Default Text";
    public Sprite cardArt;

    // Values not displayed on card
    public float attackSpeed = 1;
    public float range = 2.25F;
}