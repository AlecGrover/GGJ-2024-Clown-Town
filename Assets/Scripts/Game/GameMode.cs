using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameObject cardDisplayPrefab;
    
    [SerializeField]
    private List<Card> AllCards;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject DealCard()
    {
        GameObject newCard = Instantiate(cardDisplayPrefab);
        CardDisplay newCardDisplay = newCard.GetComponent<CardDisplay>();
        if (newCardDisplay == null || AllCards.Count == 0)
        {
            Destroy(newCard);
        }
        else
        {
            newCardDisplay.cardData = AllCards[Random.Range(0, AllCards.Count)];
        }

        return newCard;
    }
    
    
}
