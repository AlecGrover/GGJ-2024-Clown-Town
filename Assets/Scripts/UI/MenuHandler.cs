using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    private int TITLE_SCREEN = 0, MAIN_SCREEN = 1, TUTORIAL_SCREEN = 2, SHOP_SCREEN = 3;
    
    public void GoToMainMenu(){
        SceneManager.LoadScene(TITLE_SCREEN);
    }

    public void GoToMainScene(){
        SceneManager.LoadScene(MAIN_SCREEN);
    }

    public void GoToTutorial(){
        SceneManager.LoadScene(TUTORIAL_SCREEN);
    }

    public void GoToShop(){
        SceneManager.LoadScene(SHOP_SCREEN);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void RestartFromDay1(){

    }

    public void RestartDay(){

    }
}
