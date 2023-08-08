using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

   [SerializeField] private ItemData itemData;


    private void SetupVisuals()
    {
        if (itemData == null)
            return;


        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item object - " + itemData.itemName;
    }

    void Update() 
    {
    
    }

    public void SetupItem(ItemData _itemData)
    {
        itemData = _itemData;
        
        SetupVisuals();
    }


    public void PickupItem()
    {
        if(Inventory.instance.CanAddItem() == false && itemData.itemType == ItemType.Equipment)
        {
           // rb.velocity = new Vector2(0,7);
            return;
        }

        Debug.Log(itemData.itemName + " pick u!p!");

        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D collision) 
    {

    if(collision.GetComponent<Player2>() != null)
        {
            PickupItem();
        }
    }

    
}
