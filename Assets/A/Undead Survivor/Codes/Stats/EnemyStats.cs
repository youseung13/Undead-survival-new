using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy2 enemy;
   protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy2>();
    }


    public override void TakeDamage(int _damage)
        {
            base.TakeDamage(_damage);

        }

    protected override void Die()
    {
        base.Die();

        enemy.Die();
    }
        
}

