using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
   [SerializeField] private float xLimit = 960;
   [SerializeField] private float yLimit = 540;

   [SerializeField] private float xOffst = 150;
   [SerializeField] private float yOffst = 100;

    public virtual void AdjustPosition()
    {
        
       Vector2 mousePosition = Input.mousePosition;

       float newXOffset = 0;
       float newYOffset = 0;

       if(mousePosition.x > xLimit)
            newXOffset = -xOffst;
        else
            newXOffset = xOffst;
        
        if(mousePosition.y > yLimit)
            newYOffset = -yOffst;
        else
            newYOffset = yOffst;

       transform.position = new Vector2(mousePosition.x + newXOffset, mousePosition.y + newYOffset); 
    }

    public void AdjustFontSize(TextMeshProUGUI _Text)
    {
        if(_Text.text.Length > 12)
            _Text.fontSize = _Text.fontSize * .8f;
    }
}
