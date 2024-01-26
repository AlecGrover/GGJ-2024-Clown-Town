using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CardDisplay : MonoBehaviour
{

    [Header("Card Data")]
    public Card cardData;

    [Header("Card Details")]
    public Image cardArt;
    public Image cardBackArt;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardDescriptionText;

    [Header("Universal Sprites")]
    public List<Sprite> PowerSprites = new List<Sprite>(3);
    public List<Sprite> TimeSprites = new List<Sprite>(6);
    public List<Sprite> RangeSprites = new List<Sprite>(4);

    [Header("Images")]
    public Image Power;
    public Image Time;
    public Image Range;

    public Image Humor1Background;
    public Image Humor1Label;
    public Image Humor2Background;
    public Image Humor2Label;

    [Header("Color Overrides")]
    public List<TextMeshProUGUI> textColors;
    public List<Image> primaryCardColors;
    public List<Image> boldCardColors;
    public List<Image> lightCardColors;
    public List<Image> accentCardColors;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateCardDisplay();
    }

    private void UpdateCardDisplay()
    {
        if (cardData == null) return;
        
        if (cardArt != null) cardArt.sprite = cardData.cardArt;
        if (cardBackArt != null) cardBackArt.sprite = cardData.cardBackArt;
        if (cardNameText != null) cardNameText.text = cardData.cardName;
        if (cardDescriptionText != null) cardDescriptionText.text = cardData.cardDescription;
        if (cardData.humorType1 != null)
        {
            if (Humor1Background != null)
                Humor1Background.sprite = cardData.humorStyle1 == EHumorType.Classy
                    ? cardData.humorType1.ClassyBackgroundSprite
                    : cardData.humorType1.CrassBackgroundSprite;
            if (Humor1Label != null)
                Humor1Label.sprite = cardData.humorStyle1 == EHumorType.Classy
                    ? cardData.humorType1.ClassySprite
                    : cardData.humorType1.CrassSprite;
        }
        
        if (cardData.humorType2 != null)
        {
            if (Humor2Background != null)
                Humor2Background.sprite = cardData.humorStyle2 == EHumorType.Classy
                    ? cardData.humorType2.ClassyBackgroundSprite
                    : cardData.humorType2.CrassBackgroundSprite;
            if (Humor2Label != null)
                Humor2Label.sprite = cardData.humorStyle2 == EHumorType.Classy
                    ? cardData.humorType2.ClassySprite
                    : cardData.humorType2.CrassSprite;
        }

        if (PowerSprites.Count > 0 && Power != null)
        {
            int powerIndex = Mathf.Clamp(cardData.power - 1, 0, PowerSprites.Count - 1);
            Power.sprite = PowerSprites[powerIndex];
        }
        
        if (TimeSprites.Count > 0 && Time != null)
        {
            int timeIndex = Mathf.Clamp(cardData.cooldownTier - 1, 0, TimeSprites.Count - 1);
            Time.sprite = TimeSprites[timeIndex];
        }
        
        if (RangeSprites.Count > 0 && Range != null)
        {
            int rangeIndex = Mathf.Clamp(cardData.range - 1, 0, RangeSprites.Count - 1);
            Range.sprite = RangeSprites[rangeIndex];
        }
        
        UpdateColors();
    }

    private void UpdateColors()
    {
        foreach (TextMeshProUGUI color in textColors)
        {
            if (color == null) continue;
            color.color = cardData.textColor;
        }

        foreach (Image color in primaryCardColors)
        {
            if (color == null) continue;
            color.color = cardData.cardColor;
        }

        foreach (Image color in boldCardColors)
        {
            if (color == null) continue;
            color.color = cardData.cardBoldColor;
        }

        foreach (Image color in lightCardColors)
        {
            if (color == null) continue;
            color.color = cardData.cardLightColor;
        }

        foreach (Image color in accentCardColors)
        {
            if (color == null) continue;
            color.color = cardData.cardAccentColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        UpdateCardDisplay();
#endif
    }
    
    
    
}
