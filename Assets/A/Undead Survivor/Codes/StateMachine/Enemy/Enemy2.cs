using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    [SerializeField]protected LayerMask whatIsPlayer;

    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirction;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;
    public float detectionRadius = 5f;
    public float baseRadius;
     public float patrolRange;

    public Vector2 homePos;
    [Header("Move Info")]
    public float moveSpeed;
    public float baseSpeed;
    public float idleTime;
    public float defaulMoveSpeed;

    

    [Header("Attack Info")]
    public float attackdistance;
    public float attackCooldown;
   [HideInInspector] public float lastTimeAttacked;
   public EnemyStateMachine stateMachine {get; private set;}
   public string lastAnimBoolName {get; private set;}
   private Enemy_Skeleton enemy;



   private void OnEnable() {
     homePos = transform.position;
   }

   public virtual void AssignLastAnimName(string _animBoolName)
   {
        lastAnimBoolName = _animBoolName;
   }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
      moveSpeed = moveSpeed * (1 - _slowPercentage);
      anim.speed = anim.speed * ( 1 - _slowPercentage);

      Invoke("ReturnDefaultSpeed", _slowDuration);


    }



    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaulMoveSpeed;
    }


   protected override void Awake() 
   {
        stateMachine = new EnemyStateMachine();
        defaulMoveSpeed = moveSpeed;
        baseRadius = detectionRadius;
        baseSpeed = moveSpeed;
        enemy = GetComponent<Enemy_Skeleton>();
   
        
   }

   protected override void Update() 
   {
        base.Update();

        stateMachine.currentState.Update();
   }

    #region  Counter Attack Window
   public virtual void OpenCounterAttackWindow()
   {
    canBeStunned = true;
    counterImage.SetActive(true);
   }

   public virtual void CloseCounterAttackWindow()
   {
    canBeStunned=false;
    counterImage.SetActive(false);
   }
   #endregion

   
   public virtual void FreezeTime(bool _timeFrozen)
   {
    if(_timeFrozen)
    {
        moveSpeed = 0;
        anim.speed = 0;
    }
    else
    {
        moveSpeed = defaulMoveSpeed;
        anim.speed= 1;
    }
   }

   protected virtual IEnumerator FreezeTimerFor(float _seconds)
   {
    FreezeTime(true);
   
    yield return new WaitForSeconds(_seconds);
    
    FreezeTime(false);

   }
   public virtual void AggroTime(bool _hit)
   {
    if(_hit)
    {
        detectionRadius = detectionRadius*2.5f;
        moveSpeed = moveSpeed * 3;
    }
    else
    {
        detectionRadius = baseRadius;
        moveSpeed = baseSpeed;
       
    }
   }

      protected virtual IEnumerator hitAggro(float _seconds)
   {
    AggroTime(true);
   
    yield return new WaitForSeconds(_seconds);
    
    AggroTime(false);

   }
   
   public virtual bool CanBeStunned()
   {
    if(canBeStunned)
    {
        CloseCounterAttackWindow();
        return true;
    }
    return false;

   }


    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

   public virtual Collider2D IsPlayerDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, whatIsPlayer);
        if (colliders.Length > 0)
        {
            Debug.Log("player is detected");
            // At least one collider (player) is detected within the detectionRadius.
            // You can decide what to do here, such as prioritizing the nearest player or just returning the first one found.
            return colliders[0];
        }

        // No player is detected within the detectionRadius.
        return null;
    }
    
     private void OnDrawGizmosSelected()
    {
        // Draw the detection range circle in the Scene view.
         Gizmos.color = new Color(1f, 0f, 0f, 0.3f); 
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    protected override void OnDrawGizmos() 
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
       
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackdistance * facingDir, transform.position.y));          
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(Vector2.Distance(player.transform.position, transform.position) > detectionRadius )
        {
            StartCoroutine("hitAggro", 4f);
            Debug.Log("aggroget");
        }
    }

    // =>
}
