using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }
}
