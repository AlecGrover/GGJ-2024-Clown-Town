using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    private int TITLE_SCREEN = 0, MAIN_SCREEN = 1, TUTORIAL_SCREEN = 2, SHOP_SCREEN = 3;

    private bool isShopping = false;

    [SerializeField] private int coins = 0;

    [SerializeField] private GameObject shopScreen, audioBG, smallPopUp, coinText, shopCoinText,goodsound,badsound;

    [SerializeField] private GameObject[] textShop = new GameObject[6];
    
    [SerializeField] private int[] prices = new int[6];
    
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
        ResetShop();
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
        if(prices[i-1] <= coins && prices[i-1] != 0){
            coins -= prices[i-1];
            //Add card
            //Remove Card from shop screen
            prices[i-1] = 0;
            textShop[i-1].GetComponent<TextMeshProUGUI>().text = "X";
            goodsound.GetComponent<AudioSource>().Play();
        }
        else{
            badsound.GetComponent<AudioSource>().Play();
        }

    }

    public void SetMoney(int i){
        coins+=i;
        coinText.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        shopCoinText.GetComponent<TextMeshProUGUI>().text = coins.ToString();
    }

    public int GetMoney(){
        return coins;
    }

    private void ResetShop(){
        for(int i = 0; i < 6; i++){

            if(i < 3){
                prices[i] = Random.Range(6,10);
                //Add uncommon card to shop
            }
            else if (i < 5){
                prices[i] = Random.Range(10,15);
                //Add uncommon card to shop
            }
            else{
                prices[i] = Random.Range(15,20);
                //Add uncommon card to shop
            }

            textShop[i].GetComponent<TextMeshProUGUI>().text = prices[i].ToString();
        }
    }
        
}
