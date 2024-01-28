using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    public static GamePersistence instance;

    public int DayCount;
    public int GoldCount;
    public List<Character> ActiveNPCs;
    public List<Card> Deck;

    public bool bHoldingSaveData = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Save()
    {
        var player = FindObjectOfType<CardHolder>();
        if (player != null)
        {
            Deck = player.GetAllOwnedCards();
        }
        var gameMode = FindObjectOfType<GameMode>();
        if (gameMode != null)
        {
            gameMode.GetPersistenceData(out DayCount, out GoldCount, out ActiveNPCs);
        }
        bHoldingSaveData = true;
    }

    public void Load()
    {
        if (!bHoldingSaveData) return;
        var player = FindObjectOfType<CardHolder>();
        if (player != null)
        {
            player.SetDeck(Deck);
        }
        var gameMode = FindObjectOfType<GameMode>();
        if (gameMode != null)
        {
            gameMode.SetPersistenceData(DayCount, GoldCount);
        }
    }

}
