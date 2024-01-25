using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;


[System.Serializable]
public struct HumorTaste
{
   public HumorType humorType;
   public EHumorType taste;
}

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
   public string characterName;
   public string characterBio;
   public HumorTaste taste1;
   public HumorTaste taste2;
}
