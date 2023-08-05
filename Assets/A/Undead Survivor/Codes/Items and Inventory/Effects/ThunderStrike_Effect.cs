using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder strike effect", menuName = "Data/Item Effect/Thunder strike")]
public class ThunderStrike_Effect : ItemEffect
{
    [SerializeField] GameObject thunderStrikePrefab;
    public override void ExecuteEffect(Transform _enemyposition)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab, _enemyposition.position, Quaternion.identity);


        Destroy(newThunderStrike, 1f);
        //todo L set up new thunder strike
    }
}
