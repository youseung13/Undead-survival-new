using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public float speed;
    public float hp;
    public float maxhp;

    public float damage;
    
   
    public int giveexp;
   // public RuntimeanimController[] animCon;
    public Rigidbody2D target;

    public bool isDead;

    public GameObject damageText;

    public GameObject expText;

    public SpriteRenderer spriter;
  [HideInInspector]
  public Color hurtColor = Color.red;

  public float hurtDuration = 0.1f;

 private Color originalColor;

    public float atkCooltime ;
    public float atkDelay ;

    public float atkrange;

    public Vector2 facingDirection;

    public bool isBack;

   public Vector2 home;

   public float followrange;


    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    //SpriteRenderer spriter;

    WaitForFixedUpdate wait;
   
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        atkDelay = atkCooltime;
    }

    void Start()
    {
        //anim = GetComponent<anim>();
       // player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }
 
    void FixedUpdate() 
    {
       
        /*
        if(!GameManager.instance.isLive)
        return;

        if(!isLive || anim.GetCurrentanimStateInfo(0).IsName("Hit"))//죽었거나 맞고있는 상태면 플레이어한테 안감
        return;

        Vector2 dirVec = target.position -rigid.position; //방향이나옴
        Vector2 nextVec = dirVec.normalized*speed*Time.fixedDeltaTime;//목적지로 갈 양
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;//부딪혀서 생기는 물리 속도 0으로 제어
        */

         if(Vector2.Distance(GameManager.instance.player.transform.position, transform.position) > followrange + atkrange)
        {
            isBack = true;
        }
        else
        {
            isBack = false;
        }
        FacingPlayer();
     
        if(atkDelay>0)
        atkDelay -=Time.deltaTime;
    }

    void LateUpdate()
    {
        if(!GameManager.instance.isLive)
        return;

        if(isDead)
        return;
        
       // spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isDead = false;
        coll.enabled = true;
        rigid.simulated = true;//물리적 비활성화
        spriter.sortingOrder = 2;
        anim.SetBool("IsDead", false);
        Init(data);


        //gameObject.GetComponent<BoxCollider2D>().enabled=true;
   

    }

    public void Init(EnemyData data)//스폰데이터 몬스터에 넣어주기위해
    {
     // anim.runtimeanimController =  animCon[data.sprtieType];
      
      
      hp = data.hp;
      maxhp = data.hp;
      speed = data.speed;
      giveexp = data.giveexp;
      damage = data.damage;
      atkCooltime = data.atkCooltime;
      atkrange = data.atkrange;
      followrange = data.followrange;
    }

   void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Bullet") || isDead)
        return;

        if(!other.CompareTag("Player"))
        {
            rigid.velocity =Vector3.zero;
        }
        
        TakeDamage(other.GetComponent<Bullet>().damage);
      //  StartCoroutine(KnockBack());

/*
        if(hp > 0)
        {
            //살아있다, 피격 효과
           // anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            //죽음
            isDead = true;
            coll.enabled = false;
            rigid.simulated = false;//물리적 비활성화
            spriter.sortingOrder = 1;
            anim.SetBool("IsDead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();

            if(GameManager.instance.isLive)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            
        }
*/

    }

    public void FacingPlayer()
    {  

        if(isBack)
        {
             facingDirection = home - (Vector2)transform.position;
             facingDirection.Normalize();
             anim.SetFloat("MoveX", facingDirection.x);
             anim.SetFloat("MoveY", facingDirection.y);
        
        }
        else
        {
        facingDirection = GameManager.instance.player.transform.position - transform.position;
         facingDirection.Normalize();
        anim.SetFloat("MoveX", facingDirection.x);
        anim.SetFloat("MoveY", facingDirection.y);
        }

    }


    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
       // Debug.Log("Takedamage in eney");
        // Reduce the enemy's health by the damage dealt
        hp -= damage;
        StartCoroutine(Hurt());
       // SoundManager.instance.Play("hit4");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

             GameObject DamageText = GameManager.instance.pool.Get(3);
               DamageText.transform.position = transform.position;
          //GameObject damtext = Instantiate(damageText, transform.position, Quaternion.identity);
          DamageText.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
       
       //damtext.SetDamageText(damage);

        // Check if the enemy's health has reached 0
        if (hp <= 0)
        {
             isDead = true;
            anim.SetTrigger("IsDead");
            GameManager.instance.numberOfenemy.Remove(this.gameObject);
           // anim.SetBool("IsDead", true);
            GameManager.instance.kill++;
          //  GameManager.instance.GetExp();
            GameManager.instance.player.AddEXP(giveexp);
           //isDead =true;
            // Destroy the enemy
             GameObject DamageText2 = GameManager.instance.pool.Get(3);
              DamageText2.transform.position = transform.position;
        //  GameObject damtext2 = Instantiate(damageText, transform.position, Quaternion.identity);
          DamageText2.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
           
          //  GameObject damtext2 = Instantiate(damageText, transform.position, Quaternion.identity);
         // damtext2.transform.GetComponent<TextMesh>().text = damage.ToString();

            if(GameManager.instance.isLive)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }

        }
    }


    public void Die()
    {
       
        coll.enabled = false;
        rigid.simulated = false;//물리적 비활성화
        spriter.sortingOrder = 1;
        gameObject.SetActive(false);
    }

 

    private void OnDisable() {
        gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
    }



    IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 까지 딜레이
       // yield return new WaitForSeconds(2f);
       Vector3 playerPos = GameManager.instance.player.transform.position;
       Vector3 dirVec = transform.position - playerPos;
       rigid.AddForce(dirVec.normalized, ForceMode2D.Impulse);//즉발적인 힘
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator Hurt()
    {
    if(!isDead)
    {
        if(hp>0)
    {
        for(int i = 0; i < 3; i++)
        {
            spriter.color = hurtColor;
            yield return new WaitForSeconds(hurtDuration);
            spriter.color = new Color(255,255,255,1);
            yield return new WaitForSeconds(hurtDuration);
        }
    }
    }

    }
}
