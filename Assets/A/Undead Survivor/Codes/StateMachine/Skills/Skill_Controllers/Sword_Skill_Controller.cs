using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
   private Animator anim;
   private Rigidbody2D rb;
   private CircleCollider2D cd;
   private Player2 player;

   private bool canRotate = true;
   private bool isReturning;


   [SerializeField] private float freezeTimeDuration;
    private float returnSpeed = 10f;

    [Header("Pierce info")]
   private float pierceAmount;

    [Header("Bounce Info")]
   private float bounceSpeed;
   private bool isBouncing;
   private int bounceAmount;
   private List<Transform> enemyTarget;
   private int targetIndex;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTImer;
    private bool wasStopped;
    private bool isSpinning;

    private float hitTimer;
    private float hitCooldown;

    private float spinDirection;


    private void Awake() {
                anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        player = GetComponent<Player2>();
    }
   public void Start()
    {

    }


    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void SetupSword(Vector2 _dir, Player2 _player, float _returnSpeed)
    {
        player =_player;

        rb.velocity = _dir;
        returnSpeed = _returnSpeed;


        if(pierceAmount <=0)
        anim.SetBool("Rotation",true);

        spinDirection = Mathf.Clamp(rb.velocity.x, -1,1);

        Invoke("DestroyMe", 6f);
    }

    public void SetupBounce(bool _isBounceing, int _bounceAmounts, float _bounceSpeed)
    {
        isBouncing = _isBounceing;
        bounceAmount = _bounceAmounts;
        bounceSpeed= _bounceSpeed;

        enemyTarget = new List<Transform>();
    }

    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDurration,float _hitCooldown)
    {
        isSpinning = _isSpinning;
        maxTravelDistance=_maxTravelDistance;
        spinDuration = _spinDurration;
        hitCooldown = _hitCooldown;
    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
       // rb.isKinematic = false;
        transform.parent = null;
        isReturning =true;


        //sword.skill.setcooldown;

    }


    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchTheSword();
        }

        BounceLogic();
        SpinLOgic();
    }


    private void SpinLOgic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTImer -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(
                    transform.position, 
                    new Vector2(transform.position.x + spinDirection, transform.position.y),
                    1.5f*Time.deltaTime);

                if (spinTImer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }


                hitTimer -= Time.deltaTime;

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy2>() != null)
                             SwordSkillDamage(hit.GetComponent<Enemy2>());
                    }
                }
            }

        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTImer = spinDuration;
    }

    private void BounceLogic()
    {
          if(isBouncing && enemyTarget.Count > 0)
        {

            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed *Time.deltaTime);

            if(Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy2>());

                targetIndex++;
                bounceAmount--;

                if(bounceAmount <=0)
                {
                    isBouncing =false;
                    isReturning = true;
                }

                if(targetIndex >= enemyTarget.Count)
                targetIndex=0;
            }
        }
    }





    private void OnTriggerEnter2D(Collider2D collision) //칼이 부딪히면 멈추게
    {
        if (isReturning)
            return;

        
        if(collision.GetComponent<Enemy2>() !=null)
        {
            Enemy2 enemy = collision.GetComponent<Enemy2>();
            SwordSkillDamage(enemy);
          //  enemy.StartCoroutine("FreezeTimeFor", .5f);
        }

        SetupTargetForBounce(collision);

        StuckInto(collision);

    }

    private void SwordSkillDamage(Enemy2 enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

        player.stats.DoDamage(enemyStats);
        //enemy.DamageImpact();

        if(player.skill.sword.TimeStopUnlocked)
            enemy.FreezeTimeFor(freezeTimeDuration);

        if(player.skill.sword.vulnerableUnlocked)
            enemyStats.MakeVulnerableFor(freezeTimeDuration);
       // enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration); 함수추가해서 위에껄로 대체

        ItemData_Equipment equipedAmultet = Inventory.instance.GetEquipment(EquipmentType.Amulet);//아뮬렛 장비착용확인

        if(equipedAmultet != null)//착용중이면
        equipedAmultet.Effect(enemy.transform);//효과있으면발동
    }

    private void SetupTargetForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy2>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy2>() != null)
                        enemyTarget.Add(hit.transform);
                }
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        if(pierceAmount > 0 && collision.GetComponent<Enemy2>() != null)
        {
            pierceAmount --;
            return;
        }

        if(isSpinning)
        {
            StopWhenSpinning();
            return;
        }
       
        
      
        canRotate = false;
        cd.enabled = false;

       
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if(isBouncing && enemyTarget.Count >0)
        return;


        anim.SetBool("Rotation",false);
        transform.parent = collision.transform;//부딪힌 물체 위치에 고정되게
    }

  
}
