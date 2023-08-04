using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop;//드랍할 아이템 갯수
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList = new List<ItemData>();

   [SerializeField] private GameObject dropPrefab;

   public virtual void GenerateDrop()
{
    for (int i = 0; i < possibleDrop.Length; i++)
    {
        if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            dropList.Add(possibleDrop[i]);
    }

    for (int i = 0; i < possibleItemDrop; i++)
    {
        if (dropList.Count > 0)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count)];

            dropList.Remove(randomItem);

            StartCoroutine(DelayedDropItem(randomItem, 1.5f));
        }
    }
}

   protected void DropItem(ItemData _itemData)
   {
     GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

    // Vector2 randomvelocity = new Vector2(Random.Range(-5,5), Random.Range(12,15));

     newDrop.GetComponent<ItemObject>().SetupItem(_itemData);
   }

   public IEnumerator DelayedDropItem(ItemData item, float delaySeconds)
{
    yield return new WaitForSeconds(delaySeconds);

    DropItem(item);
}
}

