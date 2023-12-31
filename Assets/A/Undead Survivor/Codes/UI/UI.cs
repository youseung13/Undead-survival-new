using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    public GameObject[] buttons;
    public GameObject[] screens;

    public UI_SkillToolTip skillToolTip;
    public UI_ItemTooltip itemToolTip;
    public UI_StatTooltip statToolTip;
    public UI_CraftWindow craftWindow;

    private void Awake() 
    {
        SwitchTo(skillTreeUI);//we need this to assign events on skill tree slot befor we assign event on skill script
    }
    // Start is called before the first frame update
    void Start()
    {
       //itemToolTip = GetComponentInChildren<UI_ItemTooltip>();
       //SwitchTo(null);//시작하면 안보이게
       SwitchTo(inGameUI);

       itemToolTip.gameObject.SetActive(false);
       statToolTip.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            SwitchWithKeyTo(characterUI);

        if(Input.GetKeyDown(KeyCode.B))
            SwitchWithKeyTo(craftUI);

        if(Input.GetKeyDown(KeyCode.K))
            SwitchWithKeyTo(skillTreeUI);

        if(Input.GetKeyDown(KeyCode.O))
            SwitchWithKeyTo(optionsUI);
        
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(_menu != null)
            _menu.SetActive(true);
    }

    public void ToggleScreen(int screenIndex)
    {
        bool isActive = screens[screenIndex].activeSelf;

        // First, close all the screens
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        // Then, open the selected screen if it was not already active
        if (!isActive)
        {
            screens[screenIndex].SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }


    public void SwitchWithKeyTo(GameObject _menu)
    {
        if(_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf)
                return;
        }

        SwitchTo(inGameUI);
    }
}
