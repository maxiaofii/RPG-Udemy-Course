using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform player;
    public SkeletonGroundState(Enemy_Skeleton enemyBase, EnemyStateMachine stateMachine, string _animBoolName) : base(enemyBase, stateMachine, _animBoolName)
    {
        this.enemy = enemyBase;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();   
        //�����þ����ⱳ��  ���Ǹо��ǲ��� ��������ȥ���ǰ�󷽸���
        if(enemy.IsPlayerDetected() || Vector2.Distance(player.transform.position,enemy.transform.position)<2)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
