using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceandFIre_Controller : ThunderStrike_Controller
{
    private Vector3 direction;
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    private void Update()
    {
        // Move the skill in the calculated direction
        transform.position += direction * 5 * Time.deltaTime;
    }
}
