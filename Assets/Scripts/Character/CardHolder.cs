using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardHolder : MonoBehaviour
{

    // Game Parameters
    public int StartingHandSize = 4;
    public float BaseRange = 10f;
    
    // Cards
    public List<Card> hand = new List<Card>();
    [SerializeField] private List<Card> deck;
    [SerializeField] private List<Card> discardPile;
    [SerializeField] private List<CardUI> handSlots;
    
    void Start()
    {
        StartingHandSize = Mathf.Clamp(StartingHandSize, 0, handSlots.Count);
        foreach (CardUI cardUI in handSlots)
        {
            cardUI.SetCardData(null);
            cardUI.gameObject.SetActive(false);
        }
        
        Shuffle();
        for (int i = 0; i < StartingHandSize; i++)
        {
            if (deck.Count == 0) return;
            DrawCard();
        }
    }

    private void Shuffle()
    {
        int count = deck.Count;
        int last = count - 1;
        for (int i = 0; i < last; i++)
        {
            int randIndex = Random.Range(i, count);
            (deck[i], deck[randIndex]) = (deck[randIndex], deck[i]);
        }
    }

    void Update()
    {
        
    }

    bool DrawCard()
    {
        if (deck.Count == 0)
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
        }
        if (hand.Count >= handSlots.Count) return false;
        
        hand.Add(deck.First());
        deck.Remove(deck.First());

        RefreshHandUI();
        return true;
    }

    bool DrawCardToIndex(int index)
    {
        if (deck.Count == 0)
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
            Shuffle();
        }
        if (index < 0 || index >= handSlots.Count) return false;
        hand[index] = deck.First();
        deck.Remove(deck.First());
        
        RefreshHandUI();
        return true;
    }

    private void RefreshHandUI()
    {
        for (int i = 0; i < handSlots.Count; i++)
        {
            handSlots[i].SetDisplayDirty();
            if (i < hand.Count)
            {
                handSlots[i].SetCardData(hand[i]);
                handSlots[i].gameObject.SetActive(true);
            }
            else
            {
                handSlots[i].gameObject.SetActive(false);
            }
        }
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public void PlayRandomCard()
    {
        if (hand.Count > 0)
        {
            Card playedCard = hand[0];
            hand.Remove(playedCard);
            if (playedCard.cardEffect != null)
            {
                playedCard.cardEffect.PlayEffect();
            }
            var npcs = FindObjectsOfType<NPC>();
            foreach (var npc in npcs)
            {
                npc.HitWithCard(playedCard);
            }
        }

        
    }
    public void PlayCard(CardUI cardUI)
    {
        int uiIndex = handSlots.FindIndex(CUI => CUI == cardUI);

        if (uiIndex >= 0 && uiIndex < handSlots.Count)
        {
            if (cardUI.CardData != null)
            {
                var npcs = NPC.FindNPCsInRadius(transform.position, BaseRange * cardUI.CardData.range, -1, new List<NPC>());
                foreach (var npc in npcs) npc.HitWithCard(cardUI.CardData);
                discardPile.Add(cardUI.CardData);
                int CardIndex = hand.FindIndex(C => C == cardUI.CardData);
                if (CardIndex >= 0 && CardIndex < hand.Count)
                {
                    DrawCardToIndex(CardIndex);
                }
                else
                {
                    hand.Remove(cardUI.CardData);
                    cardUI.CardData = null;
                    DrawCard();
                }
            }
        }
    }

}
