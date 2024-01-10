using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCallManager : MonoBehaviour
{

    public AudioSource Player;

    public delegate void Updater(float deltaTime);
    public Updater UpdatePhoneCalls;

    private void Update()
    {
        UpdatePhoneCalls?.Invoke(Time.deltaTime);
    }
}
