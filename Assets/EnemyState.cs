using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected bool triggerCalled;
    private string aniBoolName;
    protected float stateTimer;
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string _animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.aniBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        enemy.anim.SetBool(aniBoolName, true);
    }
    public virtual void Exit() 
    {
        enemy.anim.SetBool(aniBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
}
