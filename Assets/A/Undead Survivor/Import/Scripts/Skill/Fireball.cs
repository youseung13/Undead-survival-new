/*
using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Coroutine co_my_coroutine;
    public FindEnemy target;
    // Prefab for the bullet
    public GameObject bulletPrefab;

    // Shoot interval in seconds
    public float shootInterval = 2f;

    Vector3 aim ;


private void Awake() {
    target = GetComponent<FindEnemy>();
}

private void Update() {
    
}
    void Start()
    {
      // co_my_coroutine = StartCoroutine(Shoot());
         Debug.Log("start1");
        // Start the shooting coroutine
        //StartCoroutine(Shoot());
        Debug.Log("start2");
       // StopCoroutine(co_my_coroutine);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // Check if there is a target enemy
            if (target.targetEnemy != null)
            {
                // Calculate the direction to the target enemy
                Vector3 shootDirection = target.targetEnemy.transform.position - transform.position;

                // Normalize the shoot direction
                shootDirection.Normalize();
                bullet.GetComponent<Rigidbody>().velocity = shootDirection * bullet.GetComponent<BulletScript>().speed;
                
            }
            else
            {
                // If there is no target enemy, shoot in a random direction
                float angle = Random.Range(0f, 360f);
                Vector3 shootDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                bullet.GetComponent<Rigidbody>().velocity = shootDirection * bullet.GetComponent<BulletScript>().speed;
            }

            // Instantiate the bullet at the player's position
            

            // Set the bullet's velocity


            // Wait for the shoot interval
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
*/
using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public FindEnemy target;
    public GameObject bulletPrefab;

    public float cooldown = 2f;

     private float timer;

    private void Update() 
    {
        
         timer += Time.deltaTime;
        if(timer >= cooldown &&Input.GetKeyDown(KeyCode.Q) && GameManager.instance.skillManager.unlockSkill[0].activeInHierarchy == true)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
            GameObject bullet = GameManager.instance.pool.Get(5);
            bullet.transform.position = transform.position;
         //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // Check if there is a target enemy
            if (target.targetEnemy != null)
            {
                Debug.Log("yes");
                // Calculate the direction to the target enemy
                Vector2 shootDirection = target.targetEnemy.transform.position - transform.position;

                // Normalize the shoot direction
                shootDirection.Normalize();
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bullet.GetComponent<BulletScript>().speed;
                
            }
            else
            {
                Debug.Log("no");
                // If there is no target enemy, shoot in a random direction
                float angle = Random.Range(0f, 360f);
                Vector2 shootDirection = Quaternion.Euler(0, 0, angle) * Vector2.up;
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bullet.GetComponent<BulletScript>().speed;
            }
    }
}
