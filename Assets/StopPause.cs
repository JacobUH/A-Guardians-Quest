using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPause : MonoBehaviour
{
    public GameObject pause;
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
    }
    private void OnDisable()
    {
        pause.SetActive(true);
    }
}
