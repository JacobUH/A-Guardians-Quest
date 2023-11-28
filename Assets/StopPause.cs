using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopPause : MonoBehaviour
{
    public GameObject pause;
    [SerializeField] private GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        pause.SetActive(false);
        EventSystem.current.SetSelectedGameObject(backButton);
    }
    private void OnDisable()
    {
        pause.SetActive(true);
    }
}
