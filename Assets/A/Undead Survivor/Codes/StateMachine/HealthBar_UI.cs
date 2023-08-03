using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity;
    private CharacterStats myStats;
    private RectTransform myTransform;
    private Slider slider;

    private void Awake() 
    {
          entity = GetComponentInParent<Entity>();
          myTransform = GetComponent<RectTransform>();
          slider = GetComponentInChildren<Slider>();
          myStats = GetComponentInParent<CharacterStats>();
    }
    private void Start()
    {
        entity.onFlipped += FlipUI;

        myStats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;
    }




    private void FlipUI() => myTransform.Rotate(0,180,0);

    private void OnDisable() 
    {
        entity.onFlipped -= FlipUI;

        myStats.onHealthChanged-= UpdateHealthUI;
    }

}
