using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPText : MonoBehaviour
{
    float dur ;
    // Start is called before the first frame update
     private void Start() {
        //Destroy(gameObject, 1f);
       //  float ran = Random.Range(-0.5f, 0.5f);
       // transform.localPosition += new Vector3(ran, 2f, 0);
       // transform.localPosition += new Vector3(0, 5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        dur += Time.deltaTime;

        if(dur>= 1.2f)
        {
            gameObject.SetActive(false);
            dur =0;
        }
    }
}
