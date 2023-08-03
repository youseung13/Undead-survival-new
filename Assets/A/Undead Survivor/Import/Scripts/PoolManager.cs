using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerxx : MonoBehaviour
{
   //프리팹들을 보관할 변수 
   public GameObject[] prefabs;

   //풀 담당하는 리스트들
    List<GameObject>[] pools;


    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];//프리팹의 갯수만큼 리스트 배열 초기화


        for(int index = 0; index < pools.Length; index ++)
        {
            pools[index] = new List<GameObject>();
        }
    }


    public GameObject Get(int index)
    {
        GameObject select = null;

        //선택한 풀의 놀고있는 오브젝트에 접근

            //발견하면 select 변수에 할당

        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)//풀안에 놀고있는 item있으면
            {

            select = item;
            select.SetActive(true);//찾아서 할당해서 활성화
            break;//끝내
            }
        }           
        if(select == null)   //놀고있는게 없이 다쓰고있으면 
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);//새롭게 만든것도 풀에 넣어주기
            //새롭게 생성해서 select에 넣기
        }

        return select;
    }
}
