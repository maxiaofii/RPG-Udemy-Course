using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuraion;
    public bool isBusy {  get; private set; }
    [Header("Move info")]
    public float movespeed=9;
    public float jumpForce = 12;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDUration;
    public float dashDir { get; private set; }
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    
    #region State
    public PlayerStateMachine playerStateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    #endregion
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.playerStateMachine = new PlayerStateMachine();
        this.idleState = new PlayerIdleState(this, playerStateMachine, "Idle");
        this.moveState = new PlayerMoveState(this, playerStateMachine, "Move");
        this.jumpState = new PlayerJumpState(this, playerStateMachine, "Jump");
        this.airState = new PlayerAirState(this, playerStateMachine, "Jump");
        dashState = new PlayerDashState(this, playerStateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, playerStateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, playerStateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, playerStateMachine, "Attack");
        counterAttack =  new PlayerCounterAttackState(this, playerStateMachine, "CounterAttack");
    }
    protected override void Start()
    {
        base.Start();
        playerStateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        playerStateMachine.currentState.Update();
        CheckForDashInput();
        //在这里检测跳跃可以打断冲刺进入跳跃
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    playerStateMachine.ChangeState(jumpState);
        //}

    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public void AnimationTrigger() => playerStateMachine.currentState.AnimationFinishTrigger();
    void CheckForDashInput()
    {
        //视频实现方法
        //感觉不行，这样不能屮墙了 依托
        //if(IsWallDectected())
        //{
        //    return;
        //}
        if (playerStateMachine.currentState == wallSlideState)
            return;
        dashUsageTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift)&&dashUsageTimer<0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0)
                dashDir = facingDir;
            playerStateMachine.ChangeState(this.dashState);
        }
    }
}
