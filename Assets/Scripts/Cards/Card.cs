using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    
    [Header("Information")]
    public string cardName = "New Card";
    public string cardDescription;
    
    [Header("Humor Type 1")]
    public HumorType humorType1;
    public EHumorType humorStyle1;

    [Header("Humor Type 1")]
    public HumorType humorType2;
    public EHumorType humorStyle2;

    [Header("Parameters")]
    public int power = 1;
    public float range = 1.0f;
    public float cooldown = 1.0f;
    public BaseCardEffect cardEffect;
    public ERarity rarity;
    
    [Header("Appearance")]
    public Sprite cardArt;
    public Color cardColor;
    public Color cardBoldColor;
    public Color cardLightColor;
    public Color cardAccentColor;

}
