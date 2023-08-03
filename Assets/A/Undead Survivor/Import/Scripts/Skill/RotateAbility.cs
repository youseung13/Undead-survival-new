using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAbility : MonoBehaviour
{
    public GameObject spritePrefab;

    public float duration = 8.0f;

    // A list of the sprites that have been spawned
    private List<GameObject> sprites;

    // A timer for keeping track of how long the ability has been active
    private float timer;

    public bool StartRotate;

    public bool Activate;


    public int objSize = 6;
    public  float circleR;
    float deg;
    public   float objSpeed;

    void Start()
    {
        // Initialize the list of sprites
    

    

    }

    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.R) && Activate != true && GameManager.instance.skillManager.unlockSkill[3].activeInHierarchy == true)
        {
            RotateAttack();
        }

        if(timer>= duration)
        {
              for (int i = 0; i < objSize; i++)
                {
                    Destroy(sprites[i]);
                }
                StartRotate= false;
                Activate = false;
                timer = 0;
        }



        if(StartRotate == true)
        {
            Activate = true;
            timer += Time.deltaTime;
            deg+= Time.deltaTime*objSpeed*2;
            if (deg < 360)
            {
                 for (int i = 0; i < objSize; i++)
                {
                
                    var rad = Mathf.Deg2Rad * (deg+(i*(360/objSize)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    if(sprites[i] != null)
                    {
                    sprites[i].transform.position = transform.position + new Vector3(x, y);
                    sprites[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / objSize))) * -1);
                    }
                }
               
            }
            else
            {
                deg = 0;
            }
        }    
    
        
    }

    void RotateAttack()
    {
        sprites = new List<GameObject>();
        StartRotate =true;
         for (int i = 0; i < objSize; i++)
        {
            // Instantiate the sprite and add it to the list
               GameObject sprite = Instantiate(spritePrefab, transform.position, Quaternion.identity);
            Debug.Log("instan");
             sprites.Add(sprite);
              Debug.Log("add"); 
        }
    }

    // This function is called when the sprite's collider enters a trigger collider
  
}





