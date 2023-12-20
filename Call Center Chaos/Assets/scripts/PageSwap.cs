using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSwap : MonoBehaviour
{
    [SerializeField] private List<GameObject> pages = new List<GameObject>();

    public void turnPageOn(int _index)
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
        pages[_index].gameObject.SetActive(true);
    }

}
