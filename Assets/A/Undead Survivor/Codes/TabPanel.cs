using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPanel : MonoBehaviour
{
    public List<TabButtons> tabButtons;
    public List<GameObject> contensPanels;

    int selected = 0;

    private void Start()
    {
        ClickTab(selected);
    }


    public void ClickTab(int id)
    {
        for(int i = 0; i<contensPanels.Count; i++)
        {
            if(i == id)
            {
                contensPanels[i].SetActive(true);
                tabButtons[i].Selected();
            }
            else
            {
                contensPanels[i].SetActive(false);
                tabButtons[i].DeSelected();
            }
        }
        
    }
}
