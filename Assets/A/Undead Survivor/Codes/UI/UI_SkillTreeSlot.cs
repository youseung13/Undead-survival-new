using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;
    public bool unlocked;

    [SerializeField] private int skillPrice;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSKillColor;


    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;

     private Image skillImage;

     private void Awake() 
     {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSLot());
     }

    private void OnValidate() 
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Start() 
    {
        ui = GetComponentInParent<UI>();
        skillImage = GetComponent<Image>();

        

        skillImage.color = lockedSKillColor;
    }

    public void UnlockSkillSLot()
    {
        if(PlayerManager.instance.HaveEnoughMoney(skillPrice) ==false)
        return;


        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if(shouldBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Can not unlock");
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if(shouldBeLocked[i].unlocked == true)
            {
                Debug.Log("can not unlock check should be lock");
                return;
            }
        }

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       ui.skillToolTip.ShowToolTip(skillDescription, skillName);

       Vector2 mousePosition = Input.mousePosition;

       float xOffset = 0;
       float yOffset = 0;

       if(mousePosition.x > 600)
            xOffset = -150;
        else
            xOffset = 150;
        
        if(mousePosition.y > 320)
            yOffset = -150;
        else
            yOffset = 150;

        ui.skillToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset); 
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
       ui.skillToolTip.HideToolTip();
    }

}
