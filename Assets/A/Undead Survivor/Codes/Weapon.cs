using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    /*
   public int id;
   public int prefabId;
   public float damage;
   public int count;
   public float speed;


   float timer;
   Player player;

   void Awake() 
   {
        player = GameManager.instance.player;
   }

    void Start()
    {
       // Init();
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
        return;

         switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);//0,0,1
                break;
            default:
            timer += Time.deltaTime;

                if(timer> speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;

        }


        //테스트
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }

    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if( id == 0)
        Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);//특정 함수 호출들 모든 자식에게 방송하는 함수
    }

    public void Init(ItemData data)
    {
        //basic set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;//플레이어의 자식으로 등록
        transform.localPosition = Vector3.zero;


        //property set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;;
        count = data.baseCount + Character.Count;

        for ( int index = 0; index < GameManager.instance.pool.uiprefab.Length ;  index++)
        {
            if(data.projectile == GameManager.instance.pool.uiprefab[index])
            {
                prefabId = index;
                break;
            }
        }


        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed; //근거리 회전속도
                Batch();
                break;
            default:
               speed = 0.4f * Character.WeaponRate;;// 원거리 연사속도
                break;
        }

        //Hand set
        Hand hand = player.hands[(int)data.itemType];//enum 데이터는 0,1,2,3,4 정수로 가능  근접무기 ,1 원거리무기
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);//특정 함수 호출들 모든 자식에게 방송하는 함수
    }

    void Batch()
    {
        for (int index = 0; index < count; index ++)
        {
            Transform bullet;
            
            if(index < transform.childCount)//자신의 자식 오브젝트 갯수 확인
            {
                bullet = transform.GetChild(index);//이미 자식으로 가지고 있는거부터 그 위치 쓴다.
            }
            else//아니다 갖고있는 갯수를 초과헀다.
            {
                bullet =  GameManager.instance.pool.Get(prefabId).transform;//풀링에서만 가져왔던것들, 이미 있으면 그거부터 가져오고 모자란부분 폴링에서..
                bullet.parent = transform;
            }
            
           
           

            bullet.localPosition = Vector3.zero;//생후 위치 초기화 0,0,0 
            bullet.localRotation = Quaternion.identity;//회전값도 초기화 0,0,0

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.2f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);//-1 is infinity per;

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }




    void Fire()
    {
        if(!player.scanner.nearestTarget)
        return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;//목표위치 - 내 위치 = 크기 포함된 방향
        dir = dir.normalized;//크기를1로 정규화 해서 방향만 구함


        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//목표 방향쪽으로 회전시켜줌
        bullet.GetComponent<Bullet>().Init(damage, count, dir);//-1 is infinity per;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

    */
}
