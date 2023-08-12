using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   public static PlayerManager instance;
   public Player2 player;

   public int currency;
   public int level;

   private void Awake() 
   {
         if(instance!= null)
            Destroy(instance.gameObject);
         else
            instance = this;
   }

   public bool HaveEnoughMoney(int _price)
   {
      if(_price > currency)
      {
         Debug.Log("not enough money");
         return false;
      }

      currency = currency -_price;

      return true;
   }

   public int GetCurrency() => currency;
}
