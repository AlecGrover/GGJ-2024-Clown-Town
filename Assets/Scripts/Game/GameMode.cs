using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private List<Card> AllCards;

    [SerializeField]
    private InGamePopUpMenu inGameMenu;

    [SerializeField][Range(15, 600)]
    private float secondsPerDay = 180f;
    [SerializeField]
    private TextMeshProUGUI secondsText;
    
    private float secondsElapsed = 0f;
    
    [SerializeField]
    private CardHolder player;
    
    private bool isGameOver = false;
    private bool isGameStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (secondsText != null) secondsText.text = Mathf.FloorToInt(secondsPerDay).ToString();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isGameStarted) return;
        secondsElapsed += Time.fixedDeltaTime;
        if (secondsText != null) secondsText.text = Mathf.FloorToInt(Mathf.Max(secondsPerDay - secondsElapsed, 0)).ToString();
        if (secondsElapsed >= secondsPerDay)
        {
            isGameStarted = false;
            isGameOver = true;
            if (inGameMenu != null)
            {
                inGameMenu.WinGame();
            }
        }
    }

    public void LoseGame()
    {
        if (!isGameOver)
        {
            if (inGameMenu!= null) inGameMenu.LoseGame();
            isGameOver = true;
            isGameStarted = false;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

}
