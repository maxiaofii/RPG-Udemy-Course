using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
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
        if (Input.GetKey(KeyCode.Space)){
            stateMachine.ChangeState(player.wallJump);
            return;
        }
        if (xInput!=0 && player.facingDir !=xInput)
            stateMachine.ChangeState(player.idleState);
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        if (player.IsGroundDectected())
            stateMachine.ChangeState(player.idleState);
    }
}
