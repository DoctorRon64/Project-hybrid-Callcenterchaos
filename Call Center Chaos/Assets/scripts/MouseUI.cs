using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUI : MonoBehaviour
{
    public RectTransform UIObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        MoveObjectToMouse();
    }

    void MoveObjectToMouse()
    {
        UIObject.position = Input.mousePosition;
    }
}
