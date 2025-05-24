using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string aniBoolName;
    protected float stateTimer;
    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string _animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBase = enemyBase;
        this.aniBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(aniBoolName, true);
    }
    public virtual void Exit() 
    {
        enemyBase.anim.SetBool(aniBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationFinishTrigger()
    { 
        triggerCalled = true;
    }
}
