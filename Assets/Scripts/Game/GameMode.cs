using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private List<Card> AllCards;
    
    private InGamePopUpMenu inGameMenu;
    private MenuHandler menuHandler;

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
        inGameMenu = FindObjectOfType<InGamePopUpMenu>();
        menuHandler = FindObjectOfType<MenuHandler>();
        if (secondsText != null) secondsText.text = Mathf.FloorToInt(secondsPerDay).ToString();
        GamePersistence persistence = FindObjectOfType<GamePersistence>();
        if (persistence != null) persistence.Load();
        StartGame();
    }

    private void Awake()
    {
        
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

    public void GetPersistenceData(out int DayNumber, out int Gold)
    {
        if (menuHandler != null)
        {
            DayNumber = menuHandler.GetDay();
            Gold = menuHandler.GetMoney();
        }
        else
        {
            DayNumber = 0;
            Gold = 0;
        }
    }

    public void SetPersistenceData(int DayNumber, int Gold)
    {
        if (menuHandler != null)
        {
            menuHandler.SetDay(DayNumber);
            menuHandler.SetMoney(Gold);
            Debug.Log("Day: " + DayNumber + " Gold: " + Gold);
        }
        Debug.Log("Attempted to save Day: " + DayNumber + " Gold: " + Gold + "");
    }

}
