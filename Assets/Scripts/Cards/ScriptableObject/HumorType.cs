using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Humor Type", menuName = "Humor Type")]
public class HumorType : ScriptableObject
{
    [SerializeField]
    private string classy;
    [SerializeField]
    private Sprite classySprite;
    [SerializeField]
    private Sprite classyBackgroundSprite;
    [SerializeField]
    private string crass;
    [SerializeField]
    private Sprite crassSprite;
    [SerializeField]
    private Sprite crassBackgroundSprite;
    
    public string Classy => classy;
    public Sprite ClassySprite => classySprite;
    public Sprite ClassyBackgroundSprite => classyBackgroundSprite;
    public string Crass => crass;
    public Sprite CrassSprite => crassSprite;
    public Sprite CrassBackgroundSprite => crassBackgroundSprite;
}
