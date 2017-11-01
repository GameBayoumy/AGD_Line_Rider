using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : SpawnableGameObject {

    public AudioClip pickupSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Add score

            SoundManager.PlaySFXRandomized(pickupSound, "SFX");
            GameObject CurrencyHandler = GameObject.Find("CurrencyHandler");
            Currency currency = CurrencyHandler.GetComponent<Currency>();
            int newCurrency = currency.CurrencyPoints += 1;
            PlayerPrefs.SetInt("Player Currency",newCurrency);
            gameObject.SetActive(false);
        }
    }
}
