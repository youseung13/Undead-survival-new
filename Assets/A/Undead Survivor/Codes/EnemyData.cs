using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType { Melee, Range, Magic, Boss, Etc}

    [Header("# Enemy basic Stat")]
    public EnemyType enemytype;

    public float hp;
    public float maxhp;
    public float speed;
    public int giveexp;

    [Header("# Enemy battle Stat")]
    public float damage;
    public float atkCooltime ;
    public float atkrange;
    public float followrange;

    [Header("# Drop Table")]
    public GameObject[] dropItems;
}
