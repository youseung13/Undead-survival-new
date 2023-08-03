using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyxx : Characterxx
{
    
    [SerializeField]
    private int giveexp;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    public PlayerController player;
    bool isLive ;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;
    // Start is called before the first frame update
    void Awake()
    {
       rigid = GetComponent<Rigidbody2D>(); 
       spriter = GetComponent<SpriteRenderer>(); 
       anim = GetComponent<Animator>();
      // player = GetComponent<PlayerController>();
    }

    void FixedUpdate() //물리쓰니까 픽스드
    {
        if(!isLive)//죽었으면 아래 실행 x
        return;


        Vector2 dirVec = target.position - rigid.position; //위치 차이로 방향값 위치차이의 정규화
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity =Vector2.zero;//물리가 이동에 영향끼치지 않도록 0으로
    }

    void LateUpdate() 
    {
        if(!isLive)//죽었으면 아래 실행 x
        return;
        spriter.flipX = target.position.x < rigid.position.x;
    }


    void OnEnable()
    {
       // target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        hp = maxhp;
    }


    public void Init(SpawnData data)
    {
      // anim.runtimeAnimatorController = animCon[data.spriteType];
     //  speed = data.speed;
      // maxhp = data.health;
       //hp = data.health;
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("Takedamage in eney");
        // Reduce the enemy's health by the damage dealt
        hp -= damage;
          GameObject damtext = Instantiate(damageText, transform.position, Quaternion.identity);
          damtext.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
       //damtext.SetDamageText(damage);

        // Check if the enemy's health has reached 0
        if (hp <= 0)
        {
            player.AddEXP(giveexp);
            // Destroy the enemy
            gameObject.SetActive(false);
            GameObject damtext2 = Instantiate(damageText, transform.position, Quaternion.identity);
          damtext2.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
            
          
           
        }
    }
}
