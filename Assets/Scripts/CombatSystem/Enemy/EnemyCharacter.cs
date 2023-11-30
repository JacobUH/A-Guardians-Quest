using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private GameObject player;

    private void LateUpdate()
    {
        if (healthUI != null)
        {
            if (player == null) player = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(player.transform.position, transform.position) > healthDisplayDistance)
            {
                if (healthUI.activeSelf) healthUI.SetActive(false);
            }
            else
            {
                if (!healthUI.activeSelf) healthUI.SetActive(true);
            }
        }
    }
}
