using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    Enemy_Skeleton enemy;
    private int moveDir;
    public SkeletonBattleState(Enemy_Skeleton enemyBase, EnemyStateMachine stateMachine, string _animBoolName ) : base(enemyBase, stateMachine, _animBoolName)
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
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                    //return;
                }
            }
        }
        else
        {
            //����о����ã�ֱ�ӸĹ���������ȽϺ�
            // || Vector2.Distance(player.transform.position, enemy.transform.position) > 10
            if (stateTimer < 0 )
            {
                stateMachine.ChangeState(enemy.idleState);
                //return;
            }
        }
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;
        //������ת��ֱ�ӹ����Ĺؼ� ��Ϊ������Flip ���ǰ��ֱ��return ��û�к�ֱ�ӹ����߼�
        enemy.SetVelocity(enemy.moveSpeed * moveDir*1.3f,rb.velocity.y);
    }
    private bool CanAttack()
    {
        if (Time.time>=enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
