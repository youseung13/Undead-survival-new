using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : MonoBehaviour
{
   public GameObject bulletPrefab;

    [SerializeField]
    private int numBullets ;

    [SerializeField]
    private float angle ;


private void Awake() {
   
}
    void Start()
    {
    
    }

    void Update()
    {
       //Vector2 facingDirection = GameManager.instance.player.transform.up;
        
        if(Input.GetKeyDown(KeyCode.E) && GameManager.instance.skillManager.unlockSkill[2].activeInHierarchy == true)
        {
            Shoot();
        }       
    }   

      public void Shoot()
    {
     float facingRotation = Mathf.Atan2
     (GameManager.instance.player.lastMove.y,
     GameManager.instance.player.lastMove.x) * Mathf.Rad2Deg+270;
      
     float startRotation = facingRotation + angle /2f;
      float angleIncrease = angle /((float)numBullets -1f);

      for (int i = 0; i < numBullets; i++)
      {
     float tempRot = startRotation - angleIncrease*i;
          GameObject bullet = GameManager.instance.pool.Get(5);
          bullet.transform.position = transform.position;
        //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        Vector2 shootDirection = Quaternion.Euler(0f, 0f, tempRot) * Vector2.up;
      // bullet.transform.Rotate(Vector3.forward, transform.eulerAngles.z - angle / 2 + angleIncrease * i);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //  bullet.transform.up = transform.up.normalized;
          rb.velocity = shootDirection * bullet.GetComponent<BulletScript>().speed;
      }
    }

    


}


