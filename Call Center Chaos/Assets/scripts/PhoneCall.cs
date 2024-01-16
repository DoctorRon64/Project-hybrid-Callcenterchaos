using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PhoneCall : ScriptableObject
{
    public AudioClip Audio;
    public GameObject task;
    public float ringTime;
    private float timer;
    [HideInInspector]public bool PickedUp;
    [HideInInspector]public PhoneCallManager manager;

    public PhoneCall(AudioClip _audio, GameObject _task, float _ringTime, PhoneCallManager _manager)
    {
        Audio = _audio;
        task = _task;
        ringTime = _ringTime;
        manager = _manager;
    }

    public void PhoneUpdate(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0 && !PickedUp)
        {
            CancelCall();
        }
        if(timer <= 0 && PickedUp)
        {
            EndCall();
        }
    }

    private void ButtonEvent(int _button, bool state)
    {
        if(!PickedUp && !state) { PickUpCall(); }

        if(PickedUp && state && timer < Audio.length - 0.5f)
        {
            CancelCall();
        }
    }

    public void StartCall()
    {
        manager = PhoneCallManager.instance;
        manager.StartRinging();
        SerialConnect.instance.SwitchLed(true);
        manager.UpdatePhoneCalls += PhoneUpdate;
        SerialConnect.instance.ButtonEvent += ButtonEvent;
        timer = ringTime;
        PickedUp = false;
    }

    public void PickUpCall()
    {
        Debug.Log($"Phone picked up");
        manager.StopRinging();
        PickedUp = true;
        timer = Audio.length + 1;
        manager.Player.clip = Audio;
        manager.Player.Play();
    }

    private void CancelCall()
    {
        Debug.Log("Current call cancelled");
        manager.Player.Stop();
        SerialConnect.instance.SwitchLed(false);
        manager.currentCall = null;
        EndProcess();

    }
    //Ja ik ben Sven ik plaats deze comment ik ben dOM
    //Haha jazeker ook ben ik zo lang als mensen naar me kijken vragen ze waar de ingang is
    //2+2=3 haha ik ben zo slim
    private void EndCall()
    {
        Debug.Log("Current call ended");
        if(task != null)
        {
            Debug.Log("Adding new task from call");
            manager.taskManager.AddTask(task);
        }
        manager.Player.Stop();
        SerialConnect.instance.SwitchLed(false);
        EndProcess();
    }

    private void EndProcess()
    {
        manager.UpdatePhoneCalls -= PhoneUpdate;
        manager.currentCall = null;
        SerialConnect.instance.ButtonEvent -= ButtonEvent;
    }
}
