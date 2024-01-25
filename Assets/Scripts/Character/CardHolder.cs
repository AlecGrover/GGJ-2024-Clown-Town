using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    
    // Cards
    [SerializeField] private List<Card> hand;
    [SerializeField] private List<Card> deck;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayCard();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void PlayCard()
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

}
