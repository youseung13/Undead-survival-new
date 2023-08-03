using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabButtons : MonoBehaviour
{
    Image background;
    public Sprite idleImg;
    public Sprite selectedImg;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void Selected()
    {
        background.sprite = selectedImg;
    }

    public void DeSelected()
    {
        background.sprite = idleImg;
    }

}
