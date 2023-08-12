using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Entity
{
    [Header("Attack detaisl")]
    public float[] attackMovement;
    public float counterAttackDuration = .2f;
    public bool isBusy {get ; private set;}
    [Header("Move Info")]
    public float moveSpeed = 5;
    public float dashSpeed;
    public float dashDuration;
    public float swordReturnImpact;
    public bool canMove;
    private float defaultMoveSpeed;

    public SkillManager skill  {get; private set;}
    public GameObject sword {get; private set;}


    public Vector2 inputVec;
    public bool playerMoving;

    public Vector2 moveDirection;
    public Vector2 lastMove;
    public string facingDirection;
    public bool isattack;
    public int dashRange;
    public GameObject ghost;


    #region  States
    public PlayerStateMachine stateMachine {get; private set;}

  public PlayerIdleState idleState  {get; private set;}
  public PlayerMoveState moveState  {get; private set;}

  public PlayerDashState dashState { get; private set;}
  public PlayerPrimaryAttackState primaryAttack { get; private set;}
  public PlayerCounterAttackState counterAttack { get; private set;}

  public PlayerAimSwordState aimSword {get; private set;}
  public PlayerCatchSwordState catchsword {get; private set;}
  public PlayerBlackholeState blackHole {get; private set;}
  public PlayerDeadState deadState {get; private set;}

  #endregion


  protected override void Awake()
   {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        dashState = new PlayerDashState(this, stateMachine, "Dash");


        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimSword =new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchsword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackHole = new PlayerBlackholeState(this, stateMachine, "Idle");
        deadState = new PlayerDeadState(this, stateMachine, "Die");


   }


   protected override void Start() 
   {
    base.Start();
    
    skill = SkillManager.instance;

    stateMachine.Initialize(idleState);

    defaultMoveSpeed = moveSpeed;
    
   }

   protected override void Update()
   {    
    base.Update();

    stateMachine.currentState.Update();

    //Facingdir();
    CheckForInput();


    if(Input.GetKeyDown(KeyCode.P) && skill.crystal.crystalUnlocked)
        skill.crystal.CanUseSkill();

    if(Input.GetKeyDown(KeyCode.Alpha1))
    {
        Inventory.instance.UseFlask();
        Debug.Log("use flask");
    }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Access the inventory dictionary from the Inventory instance
            Dictionary<int, InventoryItem> inventoryDictionary = Inventory.instance.inventoryDictionary;

            // Print the contents of the inventoryDictionary
            foreach (KeyValuePair<int, InventoryItem> kvp in inventoryDictionary)
            {
                int key = kvp.Key;
                InventoryItem value = kvp.Value;

                // Access the stackSize and data properties of the InventoryItem
                int stack = value.stackSize;
                ItemData itemData = value.data;
                ItemType _type = value.data.itemType;
                string _name = value.data.itemName;
                float _chance = value.data.dropChance;


                // Print the values to the console
                Debug.Log("Key: " + key + ", ItemData: " + itemData + ", stacksize:" + stack+ ", type:" + _type +", name: "+ _name +", dropchance:" +_chance);
            }
        }

    
   }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
      moveSpeed = moveSpeed * ( 1  - _slowPercentage);
      anim.speed = anim.speed * (1 - _slowPercentage);

      Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        
    }

 

   public void AssignNewSword(GameObject _newSword)
   {
    sword = _newSword;
   }

   public void CatchTheSword()
   {
    stateMachine.ChangeState(catchsword);
      Destroy(sword);
   }

   public IEnumerator BusyFor(float _seconds)
   {
    isBusy = true;

    yield return new WaitForSeconds(_seconds);

    isBusy=false;

   }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForInput()
   {

        if(skill.dash.dashUnlocked == false)
        return;
    
        if(Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
        
            stateMachine.ChangeState(dashState);
        }


      //  if(Input.GetKeyDown(KeyCode.V) && isattack != true)
       // stateMachine.ChangeState(primaryAttack);
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

public void Facingdir()
{
       
        if( moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMove = moveDirection;
        }

             moveDirection =new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
                  // 에니메이션 MoveX, MoveY 
     anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
     anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
     anim.SetBool("PlayerMoving", playerMoving); // 애니메이션 PlayerMoving을 playerMoving과 같도록 설정
     anim.SetFloat("LastMoveX", lastMove.x); // LastMoveX를 lastMove의 X값과 같게 설정
     anim.SetFloat("LastMoveY", lastMove.y); // LastMoveY를 lastMove의 Y값과 같게 설정
}


    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

}
