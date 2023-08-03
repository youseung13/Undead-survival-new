using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnemy : MonoBehaviour
{
    public float attackRange; // the distance at which the enemy will start attacking the player
    public float explosionRange; // the distance at which the enemy will self-destruct
    public GameObject explosionPrefab; // the prefab for the explosion effect

    private Transform player; // reference to the player's transform
    private bool attacking; // flag to track whether the enemy is currently attacking the player


    private void Start() 
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() 
    {
        if (Vector3.Distance(transform.position, player.position) < attackRange)
             {
                attacking = true;
             }

        if (attacking) 
        {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, player.position) < explosionRange) 
        {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        }
    }


}