using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskImage;

    private SkillManager skills;

    // Start is called before the first frame update
    void Start()
    {
        if(playerStats != null)
            playerStats.onHealthChanged +=  UpdateHealthUI;

       skills = SkillManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldownOf(dashImage);
        if(Input.GetKeyDown(KeyCode.Q))
            SetCooldownOf(parryImage);

        if(Input.GetKeyDown(KeyCode.P))
            SetCooldownOf(crystalImage);
        
        if(Input.GetKeyDown(KeyCode.T))
            SetCooldownOf(swordImage);

        if(Input.GetKeyDown(KeyCode.F5))
            SetCooldownOf(blackholeImage);   

        if(Input.GetKeyDown(KeyCode.Alpha1))
            SetCooldownOf(flaskImage);   

        CheckCooldownOf(dashImage, skills.dash.cooldown);
        CheckCooldownOf(parryImage, skills.parry.cooldown);
        CheckCooldownOf(crystalImage, skills.crystal.cooldown);
        CheckCooldownOf(swordImage, skills.sword.cooldown);
        CheckCooldownOf(blackholeImage, skills.blackhole.cooldown);
        CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);
    }

        private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }

    private void SetCooldownOf(Image _image)
    {
        if(_image.fillAmount <=0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if(_image.fillAmount >= 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }

}
