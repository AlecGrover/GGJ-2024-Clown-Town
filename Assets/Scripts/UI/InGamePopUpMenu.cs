using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InGamePopUpMenu : MonoBehaviour
{

    private int MENU_CLEAR = 0, MENU_PAUSE = 1, MENU_WIN = 2, MENU_GAME_OVER = 3;
    private int menuType = 0;

    [SerializeField]private int MAX_DAY_TIMER = 9000;
    [SerializeField]private int dayTimer = 0;

    [SerializeField]private GameObject nextButton, mainMenuButton, resumeButton;
    [SerializeField]private GameObject textPause, textWin, textLose, backgroundImage;
    
    void Start(){
        dayTimer = MAX_DAY_TIMER;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (menuType == MENU_CLEAR){
                ChangeMenuType(MENU_PAUSE);
            }
            else if (menuType == MENU_PAUSE){
                ChangeMenuType(MENU_CLEAR);
            }
        }
    }

    void FixedUpdate(){
        dayTimer--;
        if(dayTimer < 0){
            dayTimer = MAX_DAY_TIMER;
            //WinGame();
        }
    }

    public void ResumeGame(){
        ChangeMenuType(MENU_CLEAR);
    }

    public void LoseGame(){
        ChangeMenuType(MENU_GAME_OVER);
    }

    public void WinGame()
    {
        MenuHandler menu = FindObjectOfType<MenuHandler>();
        if (menu != null)
        {
            menu.SetDay(menu.GetDay() + 1);
        }
        ChangeMenuType(MENU_WIN);
    }

    private void ChangeMenuType(int t){
        if(t == MENU_CLEAR){
            menuType = MENU_CLEAR;
            Time.timeScale = 1;
            backgroundImage.SetActive(false);
        }
        else{
            Time.timeScale = 0;
            backgroundImage.gameObject.SetActive(true);
            if(t == MENU_PAUSE){
                menuType = MENU_PAUSE;
                mainMenuButton.SetActive(true);
                resumeButton.SetActive(true);
                nextButton.SetActive(false);
                textPause.SetActive(true);
                textWin.SetActive(false);
                textLose.SetActive(false);
            }
            else if(t == MENU_WIN){
                menuType = MENU_WIN;
                mainMenuButton.SetActive(false);
                resumeButton.SetActive(false);
                nextButton.SetActive(true);
                textPause.SetActive(false);
                textWin.SetActive(true);
                textLose.SetActive(false);
            }
            else if(t == MENU_GAME_OVER){
                menuType = MENU_GAME_OVER;
                mainMenuButton.SetActive(true);
                resumeButton.SetActive(false);
                nextButton.SetActive(false);
                textPause.SetActive(false);
                textWin.SetActive(false);
                textLose.SetActive(true);
            }
        }
    }

    public int GetDayNumber()
    {
        MenuHandler menu = FindObjectOfType<MenuHandler>();
        if (menu != null)
        {
            return menu.GetDay();
        }
        else
        {
            return -1;
        }
    }
}
