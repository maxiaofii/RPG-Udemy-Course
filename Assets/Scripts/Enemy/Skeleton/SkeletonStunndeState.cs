using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkeletonStunndeState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunndeState(Enemy_Skeleton enemyBase, EnemyStateMachine stateMachine, string _animBoolName) : base(enemyBase, stateMachine, _animBoolName)
    {
        this.enemy = enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink",0,0.1f);
        stateTimer = enemy.stunDuration;
        rb.velocity= new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink",0);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
