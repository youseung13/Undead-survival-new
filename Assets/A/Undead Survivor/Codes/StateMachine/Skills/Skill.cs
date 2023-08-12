using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
   public float cooldown;
   protected float cooldownTimer;

   protected Player2 player;

   protected virtual void Start() 
   {
        player = PlayerManager.instance.player;
   }


    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }


    public virtual bool CanUseSkill()
    {
        if(cooldownTimer <0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }

        Debug.Log("Skill is on cooldown");
        return false;
    }

    public virtual void UseSkill()
    {
        //do somw skill specific things
    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 15);

        float closetDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Enemy2>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                if(distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;
                    closestEnemy =  hit.transform;

                }
              
            }
        }

        return closestEnemy;
    }
   
}
