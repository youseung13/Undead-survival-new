
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Armor,
    Weapon,
    Glove,
    Shoes,
    RIng,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
   public EquipmentType equipmentType;


    [Header("Unique effect")]
    public float itemCooldown;
   public ItemEffect[] itemEffects;


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

   [Header("Craft requirements")]
   public List<InventoryItem> craftingMaterials;

   private int DescriptionLength;

   public void Effect(Transform _enemyposition)
   {
    
    foreach(var item in itemEffects)
    {
        item.ExecuteEffect(_enemyposition);
    
    }
   }



   public void AddModifiers()
   {
       PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.health.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);

   }

   public void RemoveModifiers()
   {
       PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();


        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.health.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
   }


    public override string GetDescription()
    {

        sb.Length = 0;
        DescriptionLength = 0;

        AddItemDescription(strength, "Strength");
        AddItemDescription(agility, "agility");
        AddItemDescription(intelligence, "intelligence");
        AddItemDescription(vitality, "vitality");

        AddItemDescription(damage, "damage");
        AddItemDescription(critChance, "critChance");
        AddItemDescription(critPower, "critPower");

        AddItemDescription(health, "health");
        AddItemDescription(evasion, "evasion");
        AddItemDescription(armor, "armor");
        AddItemDescription(magicResistance, "magicResistance");

        AddItemDescription(fireDamage, "fireDamage");
        AddItemDescription(iceDamage, "iceDamage");
        AddItemDescription(lightingDamage, "lightingDamage");

        for (int i = 0; i < itemEffects.Length; i++)
        {
            if(itemEffects[i].effectDescription.Length >0)
            {
                sb.AppendLine();
                sb.AppendLine("Unique : " + itemEffects[i].effectDescription);
                DescriptionLength++;
            }
        }

        if(DescriptionLength <5)
        {
            for (int i = 0; i < 5-DescriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

      return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if(_value != 0)
        {
            if(sb.Length>0)
            sb.AppendLine();

            if(_value>0)
            sb.Append("+ " + _value + " " + _name);

            DescriptionLength++;
        }
    }
}
