using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUI : MonoBehaviour
{
    [SerializeField] public RectTransform UIObject;

    Vector3 targetPosition;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        MoveObjectToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
        }
    }

    void MoveObjectToMouse()
    {
        //UIObject.position = Input.mousePosition;
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        transform.position = targetPosition;
    }
}
