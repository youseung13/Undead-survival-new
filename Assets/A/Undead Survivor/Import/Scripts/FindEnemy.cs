/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    // List to store detected enemies
    private List<GameObject> detectedEnemies;

    private bool isdetect;
    // Target enemy
    public GameObject targetEnemy;
    // Shoot interval in seconds

private void Awake() {
     detectedEnemies = new List<GameObject>();
   
}
    void Start()
    {
    
    }

    void Update()
    {
         if (detectedEnemies.Count > 0 && isdetect == false)
        {
              StartCoroutine(DetectEnemies());
              isdetect = true;
        }
        // Check if there is a target enemy
        if (targetEnemy != null)
        {
            // Rotate towards the target enemy
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetEnemy.transform.position - transform.position), Time.deltaTime * 5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            return;
        }
        
        // Check if the collider belongs to an enemy
        if (other.tag == "Enemy")
        {
            // Add the enemy to the list of detected enemies
            detectedEnemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
         if(other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            return;
        }
        // Check if the collider belongs to an enemy
        if (other.tag == "Enemy")
        {
            // Remove the enemy from the list of detected enemies
            detectedEnemies.Remove(other.gameObject);
        }
    }

    IEnumerator DetectEnemies()
    {
        while (true)
        {
            // Find the closest enemy
            targetEnemy = FindClosestEnemy();

            // Wait for 1 second before checking for new enemies again
            yield return new WaitForSeconds(1f);
        }
    }

    GameObject FindClosestEnemy()
    {
        // Set the initial minimum distance to a large number
        float minDistance = float.MaxValue;

        // Set the initial closest enemy to null
        GameObject closestEnemy = null;

        // Loop through all detected enemies
        foreach (GameObject enemy in detectedEnemies)
        {
            // Calculate the distance to the enemy
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // Check if the enemy is closer than the current closest enemy
            if (distance < minDistance)
            {
                // Set the new minimum distance and closest enemy
                minDistance = distance;
                closestEnemy = enemy;
            }
        }
       // Debug.Log(closestEnemy.name + closestEnemy.transform.position);

        // Return the closest enemy
        return closestEnemy;
    }

}


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    // Target enemy
    public GameObject targetEnemy;
    public GameObject targetOutline;
    // Detection radius
    public float detectionRadius = 5f;
    // Raycast layer mask
    public LayerMask enemyLayer;
    // Detect interval in seconds
    public float detectInterval = 1f;
    private bool isdetect;

    bool isRunning = true;

    void Start()
    {
    }

   void Update()
    {
        if (!isdetect)
        {
            StartCoroutine(DetectEnemies());
            isdetect = true;
        }
       if(targetEnemy)
        {
          ////  targetOutline.SetActive(true);
          ////  targetOutline.transform.position = targetEnemy.transform.position;
         ////   targetOutline.transform.rotation = targetEnemy.transform.rotation;
          //  targetOutline.transform.parent = targetEnemy.transform;
           // targetOutline.transform.localPosition = new Vector3(0,0.5f,0);
        } //above of head
        else
        {
     ////       targetOutline.SetActive(false);
        }
    }

    IEnumerator DetectEnemies()
    {
        while (isRunning)
        {
            // Find all enemies within detection radius
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
            if (hitColliders.Length > 0)
            {
                // Assign closest enemy to target enemy
                targetEnemy = hitColliders[0].gameObject;
                float closestDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
                for (int i = 1; i < hitColliders.Length; i++)
                {
                    float distance = Vector3.Distance(transform.position, hitColliders[i].transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetEnemy = hitColliders[i].gameObject;
                    }
                }
            }
            else
            {
                targetEnemy = null;
            }
            // Wait for detectInterval seconds before checking for new enemies again
            yield return new WaitForSeconds(detectInterval);
        }
    }
}