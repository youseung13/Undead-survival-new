using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd=> GetComponent<CircleCollider2D>();
    private Player2 player;

    private float crystalExistTimer;

    private bool canExplode;
    private bool canMove;
    private float moveSpeed;

    private bool canGrow;
    private float growSpeed =5;

    private Transform RandomTargetinRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    public void SetUpCrystal(float _crystalDuration,bool _canExplode, bool _canMove, float _moveSpeed, Transform _RandomTargetinRadius,Player2 _player)
    {
        player = _player;
        crystalExistTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        RandomTargetinRadius = _RandomTargetinRadius;
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager.instance.blackhole.GetBlackholeRadius();
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,whatIsEnemy);

        if(colliders.Length>0)//없을떄 오류나지 않게
        RandomTargetinRadius = colliders[Random.Range(0,colliders.Length)].transform;//타겟을 랜덤한 적으로 지정
    }

    private void Update() 
    {
        crystalExistTimer -= Time.deltaTime;

        if(crystalExistTimer <0)
        {
            FinishCrystal();
        }

        if(canMove)
        {
            if(RandomTargetinRadius == null)
            return;
            transform.position = Vector2.MoveTowards(transform.position, RandomTargetinRadius.position, moveSpeed*Time.deltaTime);

            if(Vector2.Distance(transform.position, RandomTargetinRadius.position) < 1)
            {
                 FinishCrystal();
                canMove = false;

            }

        }

        if(canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3,3), growSpeed*Time.deltaTime);
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy2>() != null)
            {
                player.stats.DoMagicalDamage(hit.GetComponent<CharacterStats>());// 크리스탈 딜링

                ItemData_Equipment equipedAmultet = Inventory.instance.GetEquipment(EquipmentType.Amulet);//아뮬렛 장비착용확인

                if(equipedAmultet != null)//착용중이면
                 equipedAmultet.Effect(hit.transform);//효과있으면발동
            }

        }
    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            canGrow =true;
            anim.SetTrigger("Explode");

        }
        else
            SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);
}
