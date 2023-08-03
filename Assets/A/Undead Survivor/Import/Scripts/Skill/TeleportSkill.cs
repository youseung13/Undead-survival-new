using UnityEngine;

public class TeleportSkill : MonoBehaviour
{
    Player pl;
    Rigidbody2D rb;
    public float range = 3f;
    public float cooldown = 2f;
    public GameObject teleportEffect;
    private Vector2 facingDirection;
    private float nextTeleportTime;

    private float timer;
    private Vector2[] diagonalDirections = { new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1) };

private void Awake() {
    pl=GetComponent<Player>();
    rb=GetComponent<Rigidbody2D>();
}
    void Update()
    {
        timer +=Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T) && timer > nextTeleportTime)
        {
            // Get the player's current position
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = new Vector2();
            // Check the last move direction to determine facing direction
            //float pl.lastMove.x = Input.GetAxis("Horizontal");
          //  float pl.lastMove.y = Input.GetAxis("Vertical");
            if (pl.lastMove.x > 0 && pl.lastMove.y == 0)
            {
                newPosition =new Vector2(currentPosition.x+range,currentPosition.y);
            }
            else if (pl.lastMove.x == 0 && pl.lastMove.y == 0)
            {
                newPosition =new Vector2(currentPosition.x,currentPosition.y-range);
            }
            else if (pl.lastMove.x < 0 && pl.lastMove.y == 0)
            {
                newPosition =new Vector2(currentPosition.x-range,currentPosition.y);
            }
            else if (pl.lastMove.x == 0 && pl.lastMove.y > 0)
            {
                 newPosition =new Vector2(currentPosition.x,currentPosition.y+range);
            }
            else if (pl.lastMove.x == 0 && pl.lastMove.y < 0)
            {
                 newPosition =new Vector2(currentPosition.x,currentPosition.y-range);
            }

            
            else if (pl.lastMove.x > 0 && pl.lastMove.y > 0)
            {
                if(pl.lastMove.x >pl.lastMove.y)
                {
                    newPosition =new Vector2(currentPosition.x+range,currentPosition.y);
                }
                else
                    newPosition =new Vector2(currentPosition.x,currentPosition.y+range);
            }
            else if (pl.lastMove.x < 0 && pl.lastMove.y > 0)
            {
              if(Mathf.Abs(pl.lastMove.x) >Mathf.Abs(pl.lastMove.y))
                {
                    newPosition =new Vector2(currentPosition.x-range,currentPosition.y);
                }
                else
                    newPosition =new Vector2(currentPosition.x,currentPosition.y+range);
            }
            else if (pl.lastMove.x > 0 && pl.lastMove.y < 0)
            {
               if(Mathf.Abs(pl.lastMove.x) >Mathf.Abs(pl.lastMove.y))
                {
                    newPosition =new Vector2(currentPosition.x+range,currentPosition.y);
                }
                else
                    newPosition =new Vector2(currentPosition.x,currentPosition.y-range);
            }
            else if (pl.lastMove.x < 0 && pl.lastMove.y < 0)
            {
              if(Mathf.Abs(pl.lastMove.x) >Mathf.Abs(pl.lastMove.y))
                {
                    newPosition =new Vector2(currentPosition.x-range,currentPosition.y);
                }
                else
                    newPosition =new Vector2(currentPosition.x,currentPosition.y-range);
            }
            

            // Calculate the new position
          //  Vector2 newPosition = currentPosition + (facingDirection * range);
            
            // Check if there is an obstacle at the new position
            RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.tag != "Ground")
            {
                // If there is an obstacle, restrict the movement range
                newPosition = newPosition - (facingDirection * (hit.distance - 0.1f));

            }

            // Teleport the player to the new position
            transform.position = newPosition;
            //transform.Translate(newPosition-(Vector2)(transform.position));
           

            // Spawn the teleport effect
         //   Instantiate(teleportEffect, newPosition, Quaternion.identity);

            // Set the next teleport time
            nextTeleportTime = Time.time + cooldown;

            Debug.Log("dire" + facingDirection + "position" + newPosition);
        }
    }
}
