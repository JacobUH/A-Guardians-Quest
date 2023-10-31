using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public int videoIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VideoManager.Instance.PlayVideo(videoIndex);
        }
        transform.parent.gameObject.SetActive(false);
    }
}
