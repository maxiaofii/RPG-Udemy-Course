using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }
        if(!player.IsGroundDectected())
            stateMachine.ChangeState(player.airState);
        if (Input.GetKeyDown(KeyCode.Space)&& player.IsGroundDectected())
            stateMachine.ChangeState(player.jumpState);
        //if (player.IsGroundDectected())
        //    stateMachine.ChangeState(player.idleState);
    }
}
