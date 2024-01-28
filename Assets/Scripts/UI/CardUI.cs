using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(CardDisplay))]
public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    public Color highlightColor = Color.cyan;
    public Image highlightImage;

    [FormerlySerializedAs("cardData")] public Card CardData;
    private CardDisplay cardDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        if (highlightImage != null) highlightImage.enabled = false;
        cardDisplay = GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.cardData = CardData;
        }
    }

    void OnEnable()
    {
        Init();
    }

    public void SetCardData(Card cardData)
    {
        CardData = cardData;
        if (cardDisplay!= null) cardDisplay.cardData = CardData;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f, 1.1f);
        if (highlightImage != null)
        {
            highlightImage.color = highlightColor;
            if (highlightImage != null) highlightImage.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f);
        if (highlightImage != null)
        {
            highlightImage.color = Color.clear;
            if (highlightImage != null) highlightImage.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardData != null)
        {
            FindObjectOfType<CardHolder>().PlayCard(this);
        }
    }

    public void SetDisplayDirty()
    {
        if (cardDisplay == null) return;
        cardDisplay.bIsDirty = true;
    }
    
}
