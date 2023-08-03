using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int level;
     // define variables for the common stats
  [SerializeField]
  public float hp;
  [SerializeField]
  public float maxhp;
  [SerializeField]
  public float speed;

     [SerializeField]
    public float exp;

    public float expToNextLevel;

    public string playerMap;

  public GameObject damageText;

  public GameObject expText;

  public SpriteRenderer rend;
  [HideInInspector]
  public Color hurtColor = Color.red;

  public float hurtDuration = 0.1f;

  private Color originalColor;

  public bool isDead;


    public Vector2 inputVec;

    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;


    bool playerMoving; // 플레이어가 움직이는지 확인

    [HideInInspector]
    public Vector2 lastMove; // 마지막 움직임이 어느 방향이었는지 확인하기 위한 변수

    public string facingDirection;

    public bool isInterrupted;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);//비활성화 되어있는 것들도 가져오기위해 트루
    }

    void Start()
    {
       GameManager.instance.Init();
        if(PlayerPrefs.HasKey("SaveData"))
        {
            GameManager.instance.Load();
        }
        originalColor = spriter.color;
      //  hp = maxhp;
    }

    void OnEnable()
    {
        //GameManager.instance.Init();
        Debug.Log("Onenable");

   

       



        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }
   
    // Update is called once per frame
    void Update()
    {   
        
      
        facingDirection = GetFacingDirection();
//                Debug.Log(facingDirection);
                
                playerMoving = false; // 따로 방향키를 누르지 않으면 플레이어는 움직이지 않음으로 설정 

                // 왼쪽, 오른쪽으로 움직이기
                if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f) { 
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f, 0f));
                playerMoving = true; // 플레이어가 움직이고 있다
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f); // lastMove의 X 값을 Horizontal로 설정
                }

                // 위, 아래로 움직이기
                if (Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f) { 
                transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0f));
                playerMoving = true; // 플레이어가 움직이고 있다
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical")); // lastMove의 Y 값을 Vertical로 설정
                }

                // 에니메이션 MoveX, MoveY 
                anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
                anim.SetBool("PlayerMoving", playerMoving); // 애니메이션 PlayerMoving을 playerMoving과 같도록 설정
                anim.SetFloat("LastMoveX", lastMove.x); // LastMoveX를 lastMove의 X값과 같게 설정
                anim.SetFloat("LastMoveY", lastMove.y); // LastMoveY를 lastMove의 Y값과 같게 설정


             
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Horizontal")))
                {
              
                }
                else
                {
                
                }
    }

    void FixedUpdate() //물리에선 fixed사용
    {
        //1.힘을주기
       // rigid.AddForce(inputVec);

        //2.속도 제어
        //rigid.velocity = inputVec;

        //3. 위치 이동
        if(!GameManager.instance.isLive)
        return;
        
        Vector2 nextVec = inputVec* speed * Time.fixedDeltaTime;//대각선은 더 빠르게 되는거 방지 
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate() 
    {
       // anim.SetFloat("Speed", inputVec.magnitude); //인풋백터의 길이,크기 만 가져옴.

/*
        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }   
*/
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Enemy") || isDead)
        return;

        
        if(!GameManager.instance.isLive)
        return;

     
       // TakeDamage(other.GetComponent<Enemy>().damage);

/*
        GameManager.instance.health  -= Time.deltaTime*10;


        if (GameManager.instance.health<0)
        {
            for ( int index =2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
 */
    }

      public void TakeDamage(float damage)
  {
    // decrease the character's health by the amount of damage taken
    hp -= damage;

    // check if the character is still alive
    if (hp <= 0)
    {
         for ( int index =2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
      // the character has been defeated
      Debug.Log(gameObject.name + " has been defeated!");
    }
  }

    public void AddEXP(int amount)
        {
        exp += amount;

        GameObject exptext = GameManager.instance.pool.Get(4);
            exptext.transform.position = transform.position;
   //   GameObject exptext = Instantiate(expText, this.transform.position, Quaternion.identity);
     // exptext.transform.position = transform.position;
     
        //exptext.transform.parent = transform;
          exptext.transform.GetChild(0).GetComponent<TextMesh>().text = "+"+amount.ToString();
       // Debug.Log("addexp" + exp +" : " + level);

        // check if the player has reached the next level
        if (exp >= expToNextLevel)
        {
        // increase the player's level and reset their EXP to 0
        level++;
        exp = exp - expToNextLevel;


        // update the amount of EXP needed to reach the next level (you can use a formula or set this value manually)
        expToNextLevel =  level * 10;

        // show a message indicating that the player has leveled up
        //Debug.Log("Player has leveled up to level " + level + "!");
        }

        }

  // define a function to show the player's current level and EXP
        public void ShowStats()
        {
        Debug.Log("Player stats:");
        Debug.Log("  Level: " + level);
        Debug.Log("  EXP: " + exp + " / " + expToNextLevel);
        }


 public string GetFacingDirection()
{
    if (lastMove.x > 0f)
    {
        return "right";
    }
    else if (lastMove.x < 0f)
    {
        return "left";
    }
    else if (lastMove.y > 0f)
    {
        return "up";
    }
    else if (lastMove.y < 0f)
    {
        return "down";
    }
    else
    {
        return "none";
    }
}


     public IEnumerator Hurt()
{
  if(!isDead)
  {
     if(hp>0)
  {
    for(int i = 0; i < 3; i++)
    {
        rend.color = hurtColor;
        yield return new WaitForSeconds(hurtDuration);
        rend.color = new Color(255,255,255,1);
        yield return new WaitForSeconds(hurtDuration);
    }
  }
  }
 
}
}
