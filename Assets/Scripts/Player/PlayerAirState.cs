using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Air");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.IsGroundDectected()&& rb.velocity.y < 0.1f)
             stateMachine.ChangeState(player.idleState);
        if(xInput != 0)
            player.SetVelocity(xInput*player.movespeed*0.8f, rb.velocity.y);
        if(player.IsWallDectected())
            stateMachine.ChangeState(player.wallSlideState);
    }
}
