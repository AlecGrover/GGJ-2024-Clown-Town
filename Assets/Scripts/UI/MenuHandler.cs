using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    private int TITLE_SCREEN = 0, MAIN_SCREEN = 1, TUTORIAL_SCREEN = 2, SHOP_SCREEN = 3;

    [SerializeField] private GameObject shopScreen, audioBG, smallPopUp;
    
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
        shopScreen.SetActive(true);
        smallPopUp.SetActive(false);
        shopScreen.GetComponent<AudioSource>().Play();
        audioBG.GetComponent<AudioSource>().Stop();
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void ReturnToGame(){
        shopScreen.SetActive(false);
        shopScreen.GetComponent<AudioSource>().Stop();
        audioBG.GetComponent<AudioSource>().Play();
    }

    public void RestartFromDay1(){

    }

    public void RestartDay(){

    }

    public void BuyCard(int i){

    }
}
