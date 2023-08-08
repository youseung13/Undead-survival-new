using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();

    
    private void OnTriggerEnter2D(Collider2D collision) 
    {

    if(collision.GetComponent<Player2>() != null)
        {
            Debug.Log(myItemObject.GetComponent<ItemData>().itemName + " pick up");
            myItemObject.PickupItem();

        }
    }
}
