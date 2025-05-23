using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int  comboCounter;
    private float lastTimeAttacked;
    private float comboWindow =2;
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 ||Time.time>= lastTimeAttacked + comboWindow)    
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);
        #region Choose attack direction
        float attackDir = player.facingDir;
        if(xInput != 0)
            attackDir = xInput;
        #endregion
        player.SetVelocity(player.attackMovement[comboCounter].x* attackDir, player.attackMovement[comboCounter].y);
        stateTimer = 0.2f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.ZeroVelocity();
        }
        if(triggerCalled && !player.isBusy)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
