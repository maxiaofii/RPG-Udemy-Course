using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack info")]
    public Vector2[] attackMovement;

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

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatisGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    #region Commponents
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

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
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.playerStateMachine = new PlayerStateMachine();
        this.idleState = new PlayerIdleState(this, playerStateMachine, "Idle");
        this.moveState = new PlayerMoveState(this, playerStateMachine, "Move");
        this.jumpState = new PlayerJumpState(this, playerStateMachine, "Jump");
        this.airState = new PlayerAirState(this, playerStateMachine, "Jump");
        dashState = new PlayerDashState(this, playerStateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, playerStateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, playerStateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, playerStateMachine, "Attack");
    }   
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerStateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
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
    #region SetVelocity
    public void ZeroVelocity() => rb.velocity = Vector2.zero;
    public void SetVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
        FlipController(x);
    }
    #endregion

    #region Collision
    public bool IsGroundDectected()=>Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
    public bool IsWallDectected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatisGround);
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if(_x>0&& !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
}
