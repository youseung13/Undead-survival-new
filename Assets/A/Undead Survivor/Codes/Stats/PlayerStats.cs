using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player2 player;
 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player2>();
    }


    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override void Die()
    {
        base.Die();

        player.Die();
    }

    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);//아머 착용여부 가져오고

        if(currentArmor != null)//비어있지않고 입고있으면
        {


            currentArmor.Effect(player.transform);//가지고있는 효과 실행
        }

    }
}
