using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private List<Card> AllCards;

    [SerializeField]
    private InGamePopUpMenu inGameMenu;
    
    private bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseGame()
    {
        if (!isGameOver)
        {
            if (inGameMenu!= null) inGameMenu.LoseGame();
            isGameOver = true;
        }
    }

}
