using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public GameObject[] lockSkill;
    public GameObject[] unlockSkill;
    //public GameObject uiNotice;


    enum Skill { Skill1, Skill2, Skill3, Skill4 }
    Skill[] skills;
    WaitForSecondsRealtime wait;//타임스케쥴 영향없이 현실시간 반영

    void Awake()
        {
            skills = (Skill[])System.Enum.GetValues(typeof(Skill));
            wait = new WaitForSecondsRealtime(5);
           

            if (!PlayerPrefs.HasKey("SkillData")) {
                Init();
            }
        }

     void Init()
        {
            PlayerPrefs.SetInt("SkillData", 1);

            foreach (Skill skill in skills) {
                PlayerPrefs.SetInt(skill.ToString(), 0);
            }
        }
    // Start is called before the first frame update
    void Start()
    {
        UnlockSkill();
    }

    void UnlockSkill()
    {
        for (int index = 0; index < lockSkill.Length; index++)
        {
            string skillName = skills[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(skillName) == 1;
            lockSkill[index].SetActive(!isUnlock);
            unlockSkill[index].SetActive(isUnlock);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach ( Skill skill in skills)
        {
            
            CheckAchive(skill);
        }
    }

    void CheckAchive(Skill skill)
    {
        bool isAchive = false;

        switch (skill)
        {
            case Skill.Skill1:
                if(GameManager.instance.gameTime >= 5 && unlockSkill[0].activeInHierarchy == false)
                isAchive = true;
                break;
            case Skill.Skill2:
                if(GameManager.instance.gameTime >= 10 && unlockSkill[1].activeInHierarchy == false)
                isAchive = true;
                break;
            case Skill.Skill3:
                if(GameManager.instance.gameTime >= 15 && unlockSkill[2].activeInHierarchy == false)
                isAchive = true;
                break;
            case Skill.Skill4:
                if(GameManager.instance.gameTime >= 20 && unlockSkill[3].activeInHierarchy == false)
                isAchive = true;
                break;
        }


        if (isAchive && PlayerPrefs.GetInt(skill.ToString()) == 0)  //해금 안되고 조건 충족시켰을떄
        {
            PlayerPrefs.SetInt(skill.ToString(), 1);
       
        }

        UnlockSkill();


    }
}
