using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Freeze enemies Effect", menuName = "Data/Item Effect/Freeze enemies")]
public class FreezeEnemies_Effect : ItemEffect
{
   [SerializeField] private float duration;


    public override void ExecuteEffect(Transform _transform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if(playerStats.currentHealth > playerStats.GetMaxHealthValue() *.1f)
            return;

        if(!Inventory.instance.CanUseArmor())//쿨다운 체크하고 사용불가능이면 발동x
            return;


       Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach(var hit in colliders)
        {
            hit.GetComponent<Enemy2>()?.FreezeTimeFor(duration);// ?앞에께 참이면 뒤에 실행 아니면 x


        }
    }
}
