using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackSstate : EnemyState
{
    Enemy_Skeleton enemy;
    public SkeletonAttackSstate(Enemy_Skeleton enemyBase, EnemyStateMachine stateMachine, string _animBoolName) : base(enemyBase, stateMachine, _animBoolName)
    {
        this.enemy = enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    } 
}
