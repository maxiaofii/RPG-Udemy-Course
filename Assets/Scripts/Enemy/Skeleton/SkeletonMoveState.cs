using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy_Skeleton enemy, EnemyStateMachine stateMachine, string _animBoolName) : base(enemy, stateMachine, _animBoolName)
    {
        this.enemy = enemy;
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
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rb.velocity.y);
        if(enemy.IsWallDectected()||!enemy.IsGroundDectected())
        {
            enemy.SetVelocity(0, enemy.rb.velocity.y);
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        
    }
}
