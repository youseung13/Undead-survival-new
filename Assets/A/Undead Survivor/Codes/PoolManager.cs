using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   // 프리팹보관할 변수
   public GameObject[] uiprefab;
   public GameObject[] enemyprefab;


   // 풀 담당을 하는 리스트 변수와1:1관계
   List<GameObject>[] uipools;
    List<GameObject>[] enemypools;
   void Awake()
   {
    uipools = new List<GameObject>[uiprefab.Length];//변수 길이만큼 리스트 배열들 초기화

    for (int index = 0;index < uipools.Length; index++)
    {
        uipools[index] = new List<GameObject>();//배열안에 리스트들 초기화
    }
    Debug.Log(uipools.Length);

    enemypools = new List<GameObject>[enemyprefab.Length];//변수 길이만큼 리스트 배열들 초기화

    for (int index = 0;index < enemypools.Length; index++)
    {
        enemypools[index] = new List<GameObject>();//배열안에 리스트들 초기화
    }
    Debug.Log(uipools.Length);
   }


   public GameObject Get(int index)
   {
    GameObject select = null;//지역번수는 초기화 해야함

    //선택한 풀의 놀고 있는(비활성) 오브젝트에 접근

    foreach (GameObject item in uipools[index])
    {
        if(!item.activeSelf)
        {
            //비활성화된것 처리
            //발견하면 select 변수에 할당
            select = item;
            select.SetActive(true);
            break;//반복끝내기
        }
    }

    //못찾았으면
    if(!select)
    {
        //새롭게 생성해서 select 변수에 할당
        select = Instantiate(uiprefab[index], transform);//생성
        uipools[index].Add(select);//풀에 등록

    }


    return select;
   }

   public GameObject Get2(int index)
   {
    GameObject select = null;//지역번수는 초기화 해야함

    //선택한 풀의 놀고 있는(비활성) 오브젝트에 접근

    foreach (GameObject item in enemypools[index])
    {
        if(!item.activeSelf)
        {
            //비활성화된것 처리
            //발견하면 select 변수에 할당
            select = item;
            select.SetActive(true);
            break;//반복끝내기
        }
    }

    //못찾았으면
    if(!select)
    {
        //새롭게 생성해서 select 변수에 할당
        select = Instantiate(enemyprefab[index], transform);//생성
        enemypools[index].Add(select);//풀에 등록

    }


    return select;
   }
 
}
