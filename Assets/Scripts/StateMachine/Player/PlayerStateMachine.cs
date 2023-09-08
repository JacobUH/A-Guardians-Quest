using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Transform mainCameraTransform;
    public PlayerTargetManager playerTargetManager;

    public override void Start()
    {
        base.Start();
        playerTargetManager = GetComponent<PlayerTargetManager>();
        SwitchState(new PlayerFreeLookState(this));
    }
}
