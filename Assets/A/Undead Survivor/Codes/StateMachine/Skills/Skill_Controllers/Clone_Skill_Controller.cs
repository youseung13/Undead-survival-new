using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private Player2 player;
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed;

    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius =.8f;
    private Transform closetEnemy;
    private int facingDir=1;

    private bool canDuplicateClone;
    private float chanceToDuplicate;

    private void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer <0)
        {
            sr.color = new Color(1,1,1,sr.color.a - (Time.deltaTime*colorLoosingSpeed));

            if(sr.color.a <=0)
            Destroy(gameObject);
        }

    }
  public void SetupClone
  (Transform _newTransform, float _cloneduration, bool _canAttack, Vector3 _offset, Transform _closestEnemy,bool _canDuplicate,float _cahanceToDuplicate,Player2 _player)
  {
    if(_canAttack)
    anim.SetInteger("AttackNumber", Random.Range(1,3));


    player = _player;
    transform.position = _newTransform.position + _offset;
    cloneTimer = _cloneduration;

    closetEnemy = _closestEnemy;
    canDuplicateClone = _canDuplicate;
    chanceToDuplicate = _cahanceToDuplicate;
    FaceClosetTarget();
  
  }

    private void AnimationTrigger()
   {
        cloneTimer = -.1f;
   }

   private void AttackTrigger()
   {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy2>() != null)
            {
                player.stats.DoDamage(hit.GetComponent<CharacterStats>());//클론 딜링

                if(canDuplicateClone)
                {
                    if(Random.Range(0,100) < chanceToDuplicate)
                    {
                        SkillManager.instance.clone.CreateClone(hit.transform,new Vector3(.8f * facingDir,0));
                    }
                }
            }

        }
   }

   private void FaceClosetTarget()
   {
        if(closetEnemy != null)
        {
            if(transform.position.x > closetEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0,180,0);
            }

        }
   }
}
