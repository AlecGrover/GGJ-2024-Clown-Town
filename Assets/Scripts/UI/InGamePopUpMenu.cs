using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePopUpMenu : MonoBehaviour
{

    private int MENU_CLEAR = 0, MENU_PAUSE = 1, MENU_WIN = 2, MENU_GAME_OVER = 3;
    private int menuType = 0;

    [SerializeField]private GameObject nextButton, restartDayButton, restartDay1Button, mainMenuButton;
    [SerializeField]private GameObject textPause, textWin, textLose, backgroundImage;
    

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

    private void ChangeMenuType(int t){
        if(t == MENU_CLEAR){
            menuType = MENU_CLEAR;
            backgroundImage.SetActive(false);
        }
        else{
            backgroundImage.gameObject.SetActive(true);
            if(t == MENU_PAUSE){
                menuType = MENU_PAUSE;
                mainMenuButton.SetActive(true);
                restartDayButton.SetActive(false);
                restartDay1Button.SetActive(false);
                nextButton.SetActive(false);
                textPause.SetActive(true);
                textWin.SetActive(false);
                textLose.SetActive(false);
            }
            else if(t == MENU_WIN){
                menuType = MENU_WIN;
                mainMenuButton.SetActive(false);
                restartDayButton.SetActive(false);
                restartDay1Button.SetActive(false);
                nextButton.SetActive(true);
                textPause.SetActive(false);
                textWin.SetActive(true);
                textLose.SetActive(false);
            }
            else if(t == MENU_GAME_OVER){
                menuType = MENU_GAME_OVER;
                mainMenuButton.SetActive(true);
                restartDayButton.SetActive(true);
                restartDay1Button.SetActive(true);
                nextButton.SetActive(false);
                textPause.SetActive(false);
                textWin.SetActive(false);
                textLose.SetActive(true);
            }
        }
    }
}