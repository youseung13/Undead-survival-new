using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_CraftSlot : UI_ItemSlot
{
    private void Update() 
    {
        UpdateSlot(item);
    }
   public override void OnPointerDown(PointerEventData eventData)
   {
        ItemData_Equipment craftdata = item.data as ItemData_Equipment;

        Inventory.instance.CanCraft(craftdata, craftdata.craftingMaterials);

   }
}
