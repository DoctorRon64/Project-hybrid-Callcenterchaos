using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSwap : MonoBehaviour
{
    [SerializeField] private List<GameObject> pages = new List<GameObject>();
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private Color notPressed;
    [SerializeField] private Color pressed;

    public void turnPageOn(int _index)
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
        pages[_index].gameObject.SetActive(true);
    
        foreach( var button in buttons)
        {
            button.image.color = pressed;
        }
        buttons[_index].image.color = notPressed;
    }
}
