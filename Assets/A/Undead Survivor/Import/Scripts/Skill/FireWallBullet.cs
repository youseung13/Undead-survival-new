using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallBullet : MonoBehaviour
{
    public int damagePerSecond; // The damage dealt by the firewall per second
    public float damageInterval ; // The interval at which the damage is applied, in seconds

    public float duration = 5f;

   // private float StayTime = 0.0f; // The elapsed time since the last damage was applied

    bool enterhit;

   // private float timer = 0f;
     private Collider2D hitCollider;
  

/*
    void OnTriggerEnter2D(Collider2D other)
    {
         // Check if the collider belongs to an enemy
        NewEnemy enemy = other.GetComponent<NewEnemy>();
        if (enemy != null && enemy.isDead != true && enterhit != true)
        {
            enterhit = true;
           
                enemy.TakeDamage(damagePerSecond*3);
         

            // Show a visual effect to indicate the damage being dealt
            // TODO: Play a particle effect or display a damage text above the enemy's head
        }
    }
void OnTriggerEnter2D(Collider2D other)
{
    // Check if the collider belongs to an enemy
    NewEnemy enemy = other.GetComponent<NewEnemy>();
    if(enterhit != true)
    {
          enterhit = true;
        if (enemy != null && enemy.isDead != true )
        {
        
        // Check if the collider of the enemy is touching the collider of the skill prefab
            enemy.TakeDamage(damagePerSecond*3);
             Debug.Log("first give Dam" + damagePerSecond*3);

        }
    }
    
}

    void OnTriggerStay2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        NewEnemy enemy = other.GetComponent<NewEnemy>();
        if (enemy != null && enemy.isDead != true)
        {
             if (StayTime >= damageInterval)
             {
           
                enemy.TakeDamage(damagePerSecond);
                Debug.Log("Firewall give Dam" + damagePerSecond);
                 StayTime = 0.0f;
             }

            // Show a visual effect to indicate the damage being dealt
            // TODO: Play a particle effect or display a damage text above the enemy's head
        }
    }
*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            hitCollider = other;
            StartCoroutine(DealDamageOverTime());
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            hitCollider.GetComponent<Enemy>().TakeDamage(damagePerSecond);
            Debug.Log("damage total :" + damagePerSecond);
            elapsedTime += damageInterval;
            yield return new WaitForSeconds(damageInterval);
        }
    }

    
}
