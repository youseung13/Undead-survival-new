using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
   public static float Speed//함수 아닌 속성
   {
    get { return GameManager.instance.playerId == 0 ? 1.1f : 1f;}
   }

   public static float WeaponSpeed//함수 아닌 속성
   {
    get { return GameManager.instance.playerId == 1 ? 1.1f : 1f;}
   }

   public static float WeaponRate//함수 아닌 속성
   {
    get { return GameManager.instance.playerId == 1 ? 0.9f : 1f;}
   }

   public static float Damage//함수 아닌 속성
   {
    get { return GameManager.instance.playerId == 2 ? 1.2f : 1f;}
   }

   public static int Count//함수 아닌 속성
   {
    get { return GameManager.instance.playerId == 3 ? 1 : 0;}
   }
}
