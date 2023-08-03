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
}
