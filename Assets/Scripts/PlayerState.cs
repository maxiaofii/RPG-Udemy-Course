using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    protected float xInput;
    protected float yInput;
    protected Rigidbody2D rb;
    protected PlayerStateMachine stateMachine;
    protected Player player;
    string aniboolname;
    protected float stateTimer;
    protected bool triggerCalled;
    public PlayerState(Player player, PlayerStateMachine playerStateMachine,  string stateName)
    {
        this.stateMachine = playerStateMachine;
        this.player = player;
        this.aniboolname = stateName;
        rb = player.rb;
    }
    public virtual void Enter()
    {
        player.anim.SetBool(aniboolname, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(aniboolname, false);
    }
    public virtual void Update()
    {
        stateTimer -=Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
        //这里可以实现空中冲刺
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    stateMachine.ChangeState(player.dashState);
        //}
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
