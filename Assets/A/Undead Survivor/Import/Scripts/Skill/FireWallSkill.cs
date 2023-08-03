using System.Collections;
using UnityEngine;

public class FireWallSkill : MonoBehaviour
{
    // The player game object
    public Player player;

    // The prefab for the magic skill
    public GameObject magicSkillPrefab;

    // The distance between each instance of the magic skill prefab
    public float spreadDistance = 1f;

    public int numInstances;

    // The duration for which each instance of the magic skill prefab will be displayed
    public float duration;

    // Update is called once per frame
    void Update()
    {
        // Check if the player has activated the magic skill
        if (Input.GetKeyDown(KeyCode.W) && GameManager.instance.skillManager.unlockSkill[1].activeInHierarchy == true)
        {
           FireWall();
       
        }
    }

    public void FireWall()
    {
       // SoundManager.instance.Play("Firewall");
        if(player.facingDirection == "up")
            {
                for (int i = 0; i <= numInstances; i++)
                {

                    Vector3 StartPoint = new Vector3(player.transform.position.x + (numInstances/2) -i, 
                    player.transform.position.y+3 ,player.transform.position.z);    
                    GameObject magicSkill = Instantiate(magicSkillPrefab, StartPoint , Quaternion.identity);
                    StartCoroutine(DestroyAfterDelay(magicSkill, duration));
                }
            }
            else if(player.facingDirection == "right")
            {
              for (int i = 0; i <= numInstances; i++)
                {

                    Vector3 StartPoint = new Vector3(player.transform.position.x + 3, 
                    player.transform.position.y+ (numInstances/2) -i ,player.transform.position.z);    
                    GameObject magicSkill = Instantiate(magicSkillPrefab, StartPoint , Quaternion.Euler(0,180,90));
                    StartCoroutine(DestroyAfterDelay(magicSkill, duration));
                }
            }
            else if(player.facingDirection == "down")
            {
              for (int i = 0; i <= numInstances; i++)
                {

                    Vector3 StartPoint = new Vector3(player.transform.position.x + (numInstances/2) -i, 
                    player.transform.position.y -3 ,player.transform.position.z);    
                    GameObject magicSkill = Instantiate(magicSkillPrefab, StartPoint , Quaternion.Euler(0,0,180));
                    StartCoroutine(DestroyAfterDelay(magicSkill, duration));
                }
            }
            else

            for (int i = 0; i <= numInstances; i++)
                {

                    Vector3 StartPoint = new Vector3(player.transform.position.x -3, 
                    player.transform.position.y +(numInstances/2) -i ,player.transform.position.z);    
                    GameObject magicSkill = Instantiate(magicSkillPrefab, StartPoint , Quaternion.Euler(0,0,90));
                    StartCoroutine(DestroyAfterDelay(magicSkill, duration));
                }
    }

    IEnumerator DestroyAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}