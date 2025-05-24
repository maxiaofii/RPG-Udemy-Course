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
        //�����Ƿ�ֹ��wallslide�г�̣�����wallslideʱshift���ǿ��ܳ��ֲ�Ӱ�����Ըо�
        //����ȥdashcheck�м��ȽϺ�
        if (!player.IsGroundDectected() && player.IsWallDectected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
