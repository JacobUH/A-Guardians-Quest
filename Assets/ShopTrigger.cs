using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shop;
    public GameObject pauseMenu;
    

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.X))
        {
            shop.SetActive(true);
            pauseMenu.SetActive(false);
            GameManager.Instance.SetInMenuBool(true);
        }
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Escape))
        {
            shop.SetActive(false);
            pauseMenu.SetActive(true);
            GameManager.Instance.SetInMenuBool(false);
        }

    }


}
