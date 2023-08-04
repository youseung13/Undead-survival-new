using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI  itemText;


    public InventoryItem item;
    // Start is called before the first frame update
    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem ;

        itemImage.color = Color.white;//슬롯은 투명해서 아이템 생기면 색깔 원래대로 하얗게
        
        if(item != null)
        {
            itemImage.sprite = item.data.icon;

            if(item.stackSize >1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else{
                itemText.text = "";//갯수없는 템일떄 공백으로
            }
        }
    }


    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;

        itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)//슬롯 클릭할 떄 실행
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }


        if(item.data.itemType == ItemType.Equipment)
      Inventory.instance.EquipItem(item.data);
    }
}
