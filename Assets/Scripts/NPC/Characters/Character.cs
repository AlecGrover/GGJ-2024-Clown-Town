using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor.Animations;
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

   [SerializeField] [Range(1, 5)]
   public int Patience;
   
   public string characterName;
   public string characterBio;
   public HumorTaste taste1;
   public HumorTaste taste2;
   
   public Sprite sprite;
   public AnimatorController animatorController;
}
