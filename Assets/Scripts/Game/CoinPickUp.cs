using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private AudioSource pickupEffect;
    [SerializeField] GameObject controller;

    void Start(){
        pickupEffect = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Coin"){
            Destroy(collision.gameObject);    
            controller.GetComponent<MenuHandler>().SetMoney(1);
            pickupEffect.Play();
        }

    }
}
