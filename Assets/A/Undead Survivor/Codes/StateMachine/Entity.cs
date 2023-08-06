using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class Entity : MonoBehaviour
{
  public GameObject player;
    public Transform attackCheck;
    public float attackCheckRadius;

    #region  Components
    public Animator anim { get; private set;}
    public Rigidbody2D rb { get; private set;}
    public EntityFX fx {get; private set;}
    public SpriteRenderer sr {get; private set;}
    public CharacterStats stats {get; private set;}
    public CapsuleCollider2D cd {get; private set;}
    #endregion


  [Header("Knockback Info")]
  [SerializeField] protected Vector2 knockbackDirection;
   [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;


    public System.Action onFlipped;
 

    protected virtual void Awake()
  {

  }

  protected virtual void Start()
  {
    sr = GetComponentInChildren<SpriteRenderer>();
    fx = GetComponent<EntityFX>();
    anim = GetComponentInChildren<Animator>();
    rb = GetComponent<Rigidbody2D>();
    player = GameObject.Find("Player");
    stats = GetComponent<CharacterStats>();
    cd = GetComponent<CapsuleCollider2D>();

  }

  protected virtual void Update()
  {
    
  }

  public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
  {


  }

  protected virtual void ReturnDefaultSpeed()
  {
    anim.speed = 1;
  }


  public virtual void DamageImpact() => StartCoroutine("HitKnockback");

  #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        //transform.Rotate(0, 180, 0);
    Vector3 newScale = transform.localScale;
    newScale.x *= -1;
    transform.localScale = newScale;
  
        
        


        if(onFlipped != null)
        onFlipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion

    public void SetZeroVelocity()
    {
      if(isKnocked)
      return;

        rb.velocity = new Vector2(0,0);

    } 
   public void SetVelocity(float _xVelocity, float _yVelocity)
   {
    if(isKnocked)
    return;


    rb.velocity = new Vector2(_xVelocity, _yVelocity);
     FlipController(_xVelocity);
   }

   protected virtual void OnDrawGizmos() 
   {
    Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
   }


  protected virtual IEnumerator HitKnockback()
  {
    isKnocked = true;

    rb.velocity = new Vector2(knockbackDirection.x * -facingDir, (player.transform.position.y- transform.position.y)*-1*2f);

    yield return new WaitForSeconds(knockbackDuration);

    isKnocked = false;
    rb.velocity = Vector2.zero;
  }

  public virtual void Die()
  {

  }

  /*
      IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 까지 딜레이
       // yield return new WaitForSeconds(2f);
       Vector3 playerPos = GameManager.instance.player.transform.position;
       Vector3 dirVec = transform.position - playerPos;
       rigid.AddForce(dirVec.normalized, ForceMode2D.Impulse);//즉발적인 힘
    }
  */


}
