using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBullet : MonoBehaviour
{

    public int speed;
    public int damage;

    [SerializeField]
    private int hitcount;

    [SerializeField]
    private int maxhit;

    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
       if(hitcount >= maxhit)
       {
        Destroy(gameObject);
      
        return;
       }
        // Check if the other collider belongs to an enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            hitcount++;
            // Apply damage to the enemy
            enemy.TakeDamage(damage);
        }
    }
}
