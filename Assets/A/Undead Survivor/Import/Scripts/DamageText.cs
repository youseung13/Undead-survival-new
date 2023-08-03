using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    float duration;
   // public Text text;

    /*
    public float lifetime = 0.8f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 iniPos;
     private Vector3 targetPos;
     private float timer;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float dist = Random.Range(minDist,maxDist);
        targetPos = iniPos + (Quaternion.Euler(0,0,direction)*new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime/ 2f;

        if(timer > lifetime)
        { Destroy(gameObject);}
        else if (timer > fraction)text.color = Color.Lerp(text.color, Color.clear, (timer - fraction)/ (lifetime-fraction));

        transform.localPosition = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer /lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer /lifetime));
        
    }

*/
   // public void SetDamageText(int damage)
   // {
  //     text.text = damage.ToString();
   // }
  




    private void Start() {
       
        
        //float ran = Random.Range(-0.5f, 0.5f);
       // transform.localPosition += new Vector3(ran, 2f, 0);
        
         }

    private void Update() 
    {
         duration += Time.deltaTime;
         if(duration>1.2f)
         {
           gameObject.SetActive(false);
            duration = 0;
         }
    }
}
