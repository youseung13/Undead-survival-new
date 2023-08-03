using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : Characterxx
{

    enum monster_type
    {
        Melee,
        Range
    }

    public int spawnPointIndex;
    public int prefabIndex;
    [SerializeField]
    private int giveexp;
    Animator animator;

    public Transform player;

    public PlayerController playerCon;

    public float followrange;

    public Vector2 home;

    public float atkCooltime =4;
    public float atkDelay;

    public float atkrange;

    public Vector2 facingDirection;

    public bool isBack;
 
    // Start is called before the first frame update
    public override void Start()
    {   
    
        animator = GetComponent<Animator>();
//        player = GameObject.FindGameObjectWithTag("Player").transform;
        home =transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Vector2.Distance(player.position, transform.position) > followrange + atkrange)
        {
            isBack = true;
        }
        else
        {
            isBack = false;
        }
        FacingPlayer();
     
        if(atkDelay>=0)
        atkDelay -=Time.deltaTime;
    }

    public void Init(SpawnData data)
    {
   //    speed = data.speed;
   //    maxhp = data.health;
   //    hp = data.health;
    }

    public void FacingPlayer()
    {  

        if(isBack)
        {
             facingDirection = home - (Vector2)transform.position;
             facingDirection.Normalize();
             animator.SetFloat("MoveX", facingDirection.x);
             animator.SetFloat("MoveY", facingDirection.y);
        
        }
        else
        {
        facingDirection = player.position - transform.position;
         facingDirection.Normalize();
        animator.SetFloat("MoveX", facingDirection.x);
        animator.SetFloat("MoveY", facingDirection.y);
        }

    }


    public override void TakeDamage(int damage)
    {
        if(!isDead)
        {
       // Debug.Log("Takedamage in eney");
        // Reduce the enemy's health by the damage dealt
        hp -= damage;
        StartCoroutine(Hurt());
        SoundManager.instance.Play("hit4");
          GameObject damtext = Instantiate(damageText, transform.position, Quaternion.identity);
          damtext.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
          
       //damtext.SetDamageText(damage);

        // Check if the enemy's health has reached 0
        if (hp <= 0)
        {
             playerCon.AddEXP(giveexp);
           isDead =true;
            animator.SetTrigger("IsDead");
            // Destroy the enemy
            GameObject damtext2 = Instantiate(damageText, transform.position, Quaternion.identity);
          damtext2.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        }

        }
    }


    public void Die()
    {
        gameObject.SetActive(false);
    }

     void OnEnable()
    {
       // target = GameManager.instance.player.GetComponent<Rigidbody2D>();
      //  isLive = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled=true;
        isDead=false;
        hp = maxhp;
    }

    private void OnDisable() {
        gameObject.GetComponent<CapsuleCollider2D>().enabled=false;
    }
}
