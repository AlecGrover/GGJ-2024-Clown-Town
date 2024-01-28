using System;
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

    [SerializeField]
    private CardHolder player;
    
    private List<NPC> highlightedNPCs = new List<NPC>();
    
    private bool isHighlighted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        ToggleHighlights(isHighlighted);
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

        isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f);
        if (highlightImage != null)
        {
            highlightImage.color = Color.clear;
            if (highlightImage != null) highlightImage.enabled = false;
        }

        isHighlighted = false;
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

    void ToggleHighlights(bool newHighlight = true)
    {
        if (player == null) return;
        
        foreach (var npc in highlightedNPCs)
        {
            if (npc.AffectedHighlight != null) npc.AffectedHighlight.color = Color.clear;
        }
        highlightedNPCs.Clear();
        
        if (CardData == null || !newHighlight) return;
        var npcs = NPC.FindNPCsInRadius(player.transform.position, player.BaseRange * CardData.range, -1, new List<NPC>());
        foreach (var npc in npcs)
        {
            if (npc.AffectedHighlight != null)
            {
                npc.AffectedHighlight.color = highlightColor;
                highlightedNPCs.Add(npc);
            }
        }
    }
    
}
