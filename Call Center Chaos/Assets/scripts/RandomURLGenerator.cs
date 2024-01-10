using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomURLGenerator : MonoBehaviour
{
    public List<string> urls = new List<string>();
    private UnityEngine.UI.Text URLText; 

    private void Awake()
    {
        URLText = GetComponent<UnityEngine.UI.Text>();
        RandomizeURL();
    }

    [ContextMenu("random")]
    private void RandomizeURL()
    {
        int randomURL = Random.Range(0, urls.Count);
        URLText.text = urls[randomURL];
    }
}
