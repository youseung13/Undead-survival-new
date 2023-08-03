using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int speed;
    public int damage;

    public float dur;
    public bool hit;
    // Start is called before the first frame update

    private void Update() {
        
        dur += Time.deltaTime;
        if(dur >= 3f)
        {
            gameObject.SetActive(false);
            dur = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        if (other.tag == "Enemy" && hit == false && other.GetComponent<Enemy>().isDead != true)
        {
            hit =true;
            // Deal damage to the enemy
          //  other.GetComponent<Enemy>().TakeDamage(damage);
            other.GetComponent<Enemy>().TakeDamage(damage);

            // Destroy the bullet
          //  Destroy(gameObject);
          gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        hit = false;
        dur = 0;
    }
}
