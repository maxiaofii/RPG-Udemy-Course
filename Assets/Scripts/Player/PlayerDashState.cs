using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, string stateName) : base(player, playerStateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.clone.CreateClone(player.transform);
        stateTimer = player.dashDUration;

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        //这里是防止在wallslide中冲刺，但是wallslide时shift还是可能出现残影，所以感觉
        //还是去dashcheck中检查比较好
        if (!player.IsGroundDectected() && player.IsWallDectected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
