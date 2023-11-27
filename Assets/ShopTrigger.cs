using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shop;
    public GameObject pauseMenu;
    private bool inMenu =false;
    

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.X))
        {
            shop.SetActive(true);
            pauseMenu.SetActive(false);
            GameManager.Instance.SetInMenuBool(true);
            inMenu = true;
            Time.timeScale = 0.0f;
        }
    }
    private void Update()
    {
        if (inMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            shop.SetActive(false);
            pauseMenu.SetActive(true);
            inMenu = false;
            GameManager.Instance.SetInMenuBool(false);
            Time.timeScale = 1.0f;
        }
    }


}
