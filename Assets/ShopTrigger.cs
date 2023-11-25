using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shop;
    private bool insideArea = false;
    private bool insideShop = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) insideArea = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) insideArea = false;
    }

    private void Update()
    {
        if (insideArea)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                GameManager.Instance.SetInMenuBool(true);
                insideShop = true;
                shop.SetActive(insideShop);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                insideShop = false;
                shop.SetActive(insideShop);
                GameManager.Instance.SetInMenuBool(insideShop);
            }
        }
        if (!insideArea)
        {
            insideShop = false;
            GameManager.Instance.SetInMenuBool(insideShop);
            shop.SetActive(insideShop);
            
        }
    }
}
