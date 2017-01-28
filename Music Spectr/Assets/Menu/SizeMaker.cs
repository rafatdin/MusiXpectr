using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SizeMaker : MonoBehaviour {

    public bool horizontal;
    RectTransform content;
    List<RectTransform> sizeOfButtons;
	// Use this for initialization
	void Start () {
	  //  buttons = new GameObject [50];
        sizeOfButtons = new List<RectTransform>();
        content = GetComponent<RectTransform>();
        MakeSize();
	}

    void MakeSize()
    {
        GetComponentsInChildren<RectTransform>(false, sizeOfButtons);
        if (horizontal)
        {            
            content.sizeDelta = new Vector2(0, 92.55f);
            for (int i = 1; i < sizeOfButtons.Count; i += 2)
            {
                Debug.Log(i + " = " + sizeOfButtons[i].rect.width);
                content.sizeDelta = new Vector2(15f + content.rect.width, content.rect.height);
            }
        }
        else
        {
            content.sizeDelta = new Vector2(100.75f, 30);
            for (int i = 1; i < sizeOfButtons.Count; i += 2)
            {
                Debug.Log(i + " = " + sizeOfButtons[i].rect.width);
                content.sizeDelta = new Vector2(content.rect.width, 75f + content.rect.height);
            }   
        }
    }
}
