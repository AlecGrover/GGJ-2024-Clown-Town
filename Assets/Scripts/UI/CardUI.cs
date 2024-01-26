using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardDisplay))]
public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    public Color highlightColor = Color.cyan;
    public Image highlightImage;

    private Card cardData;
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
            cardData = cardDisplay.cardData;
        }
    }

    void OnEnable()
    {
        Init();
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
        if (cardData != null)
        {
            FindObjectOfType<CardHolder>().PlayCard(cardData);
        }
    }
    
}
