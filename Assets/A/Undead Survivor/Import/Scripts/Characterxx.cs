using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characterxx : MonoBehaviour
{
  // define variables for the common stats
  [SerializeField]
  public float hp;
  [SerializeField]
  public float maxhp;
  [SerializeField]
  public float speed;


  public GameObject damageText;

  public GameObject expText;

  public SpriteRenderer rend;
  [HideInInspector]
  public Color hurtColor = Color.red;

  public float hurtDuration = 0.1f;

  private Color originalColor;

  public bool isDead;

  private void Awake() {
    rend = GetComponent<SpriteRenderer>();
  }

  public virtual void Start() {
    originalColor = rend.color;
    hp = maxhp;
  }

  // define a function to handle attacks
  public virtual void TakeDamage(int damage)
  {
    // decrease the character's health by the amount of damage taken
    hp -= damage;

    // check if the character is still alive
    if (hp <= 0)
    {
      // the character has been defeated
      Debug.Log(gameObject.name + " has been defeated!");
    }
  }

  public IEnumerator Hurt()
{
  if(!isDead)
  {
     if(hp>0)
  {
    for(int i = 0; i < 3; i++)
    {
        rend.color = hurtColor;
        yield return new WaitForSeconds(hurtDuration);
        rend.color = new Color(255,255,255,1);
        yield return new WaitForSeconds(hurtDuration);
    }
  }
  }
 
}


  
}
