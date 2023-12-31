using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;

    private void OnValidate() {
        gameObject.name = "Equipment slot - " + slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.data == null)
        return;

        Inventory.instance.UnequipItem(item.data as ItemData_Equipment);//장비 해제하고
        Inventory.instance.AddItem(item.data as ItemData_Equipment);//인벤에 추가하고

        ui.itemToolTip.hideToolTip();

        
        CleanUpSlot();//장비슬롯 비우고
    }
}
