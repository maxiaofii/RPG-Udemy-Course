using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }
        
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(xInput !=0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
