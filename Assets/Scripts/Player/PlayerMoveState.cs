using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
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
        player.SetVelocity(xInput*player.movespeed,rb.velocity.y);
        if (xInput==0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
