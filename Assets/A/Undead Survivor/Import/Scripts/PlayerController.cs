using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : Characterxx {

        [SerializeField]
        public float exp;



        public float expToNextLevel;
        
        [SerializeField]
        public int level;

        private Animator anim; // Animator를 불러오기 위한 변수

        bool playerMoving; // 플레이어가 움직이는지 확인

        [HideInInspector]
        public Vector2 lastMove; // 마지막 움직임이 어느 방향이었는지 확인하기 위한 변수

       public  string facingDirection;

        public bool isInterrupted;
        

	// Use this for initialization
	public override void Start () {
        base.Start();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
        void Update () 
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


        public void AddEXP(int amount)
        {
        exp += amount;
      GameObject exptext = Instantiate(expText, this.transform.position, Quaternion.identity);
     // exptext.transform.position = transform.position;
     
        //exptext.transform.parent = transform;
          exptext.transform.GetChild(0).GetComponent<TextMesh>().text = "+"+amount.ToString();
       // Debug.Log("addexp" + exp +" : " + level);

        // check if the player has reached the next level
        if (exp >= expToNextLevel)
        {
        // increase the player's level and reset their EXP to 0
        level++;
        exp = 0;


        // update the amount of EXP needed to reach the next level (you can use a formula or set this value manually)
        expToNextLevel = level * 10;

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

public void OnTriggerEnter2D(Collider2D other)
{
    /*
    var item = other.GetComponent<GroundItem>();
    if(item)
    {
        inventory.AddItem(new Item(item.item), 1);
        Destroy(other.gameObject);
    }
    */

}

private void OnApplicationQuit() 
{
   // inventory.Container.Items = new InventorySlot[28];// 게임종료하면 초기화되게
}

}