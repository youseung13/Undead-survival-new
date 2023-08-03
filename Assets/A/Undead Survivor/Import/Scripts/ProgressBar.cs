/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    #if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj =Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    #endif

    public PlayerController playerExp;

    public float minimum;
    public float maximum;
    public float current;
    public Image fill;
    public Slider slider;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        current = playerExp.exp;
        maximum = playerExp.expToNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
       
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset/maximumOffset;
        slider.value = fillAmount;

        fill.color = color;
    }
}

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public PlayerController playerHealth;
    public Image fillImage;
    private Slider slider;
    
    
    private void Awake() {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value <= slider.minValue)
        {
            fillImage.enabled=false;
        }
        if(slider.value> slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled=true;
        }
        float fillValue = playerHealth.exp / playerHealth.expToNextLevel;
        slider.value = fillValue;
    }
}

