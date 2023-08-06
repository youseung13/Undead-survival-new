using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player2 player => GetComponentInParent<Player2>();
   private void AnimationTrigger()
   {
    player.AnimationTrigger();
   }

   private void AttackTrigger()
   {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy2>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if(_target != null)
                player.stats.DoDamage(_target);

                //inventory get weapon call item Effect
                 ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);
              
                  if(weaponData != null)
                    weaponData.Effect(_target.transform);
            }

        }
   }



     private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
