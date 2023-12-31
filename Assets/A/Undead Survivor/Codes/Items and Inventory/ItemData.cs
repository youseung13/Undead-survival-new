using System.Text;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int ID= 0;
    [Range(0,100)]
    public float dropChance;
   
    protected StringBuilder sb = new StringBuilder();
    
    public virtual string GetDescription()
    {
        return "";
    }
}
/*
[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
   
    public enum ItemType { Melee, Range, Glove, Shoe, Heal}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;

    public int num;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")] 
    public GameObject projectile;
    public Sprite hand;
}
*/