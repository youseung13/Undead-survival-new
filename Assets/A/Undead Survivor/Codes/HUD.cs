using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Eenmy, Time, Health}

    public InfoType type;
    Text myText;
    Slider mySlider;


    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }


    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.player.exp;
                float maxExp = GameManager.instance.player.expToNextLevel;//nextExp[Mathf.Min(GameManager.instance.level,GameManager.instance.nextExp.Length-1)];
                mySlider.value = curExp/maxExp;
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.player.hp;
                float maxHealth = GameManager.instance.player.maxhp;
                mySlider.value = curHealth/maxHealth;

                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Eenmy:
                myText.text = string.Format("{0:F0}", GameManager.instance.numberOfenemy.Count);
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.player.level);// 
                break; 
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min =  Mathf.FloorToInt(remainTime / 60);//몫
                int sec = Mathf.FloorToInt(remainTime % 60);//나머지
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec );// 
                break;
        }
    }

}