using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration = 0.07f;
    protected  bool isKnocked;

    [Header("Collision info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatisGround;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Commponents
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    #region SetVelocity
    public void SetZeroVelocity() => rb.velocity = Vector2.zero;
    public void SetVelocity(float x, float y)
    {
        if(isKnocked) return;
        rb.velocity = new Vector2(x, y);
        FlipController(x);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDectected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
    public virtual bool IsWallDectected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatisGround);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
    protected virtual void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {
    }
    public virtual void Damage()
    {
        fx.StartCoroutine(fx.FlashFX()); 
        //这里被攻击了应该要有僵直,不然纯纯垃圾 往后面看会有没有解决办法
        StartCoroutine("HitKnockback");
    }
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        //这里有点问题 背刺的话 方向反了
        //rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        rb.velocity = new Vector2(knockbackDirection.x, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
}
