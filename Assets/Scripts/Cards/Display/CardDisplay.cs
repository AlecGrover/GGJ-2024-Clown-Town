using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CardDisplay : MonoBehaviour
{

    public Card cardData;

    public Image cardArt;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardDescriptionText;

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
        if (cardArt != null) cardArt.sprite = cardData.cardArt;
        if (cardNameText != null) cardNameText.text = cardData.cardName;
        if (cardDescriptionText != null) cardDescriptionText.text = cardData.cardDescription;
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
