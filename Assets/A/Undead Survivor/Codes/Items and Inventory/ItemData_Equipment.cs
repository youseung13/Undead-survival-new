
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
   public EquipmentType equipmentType;

   public int strength;
   public int agility;
   public int intelligence;
   public int vitality;

   public int damage;
   public int critChance;
   public int critPower;

   public int health;
   public int armor;
   public int evasion;
   public int magicResistance;

   public int fireDamage;
   public int iceDamage;
   public int lightingDamage;



   public void AddModifiers()
   {
       //PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

       //playerstats.strength.AddModifier(strength);
   }

   public void RemoveModifiers()
   {
       //PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>()


   }

  
}
