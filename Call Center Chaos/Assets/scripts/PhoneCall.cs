using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PhoneCall : ScriptableObject
{
    public int button;
    public AudioClip Audio;
    public GameObject task;
    public float ringTime;
    private float timer;
    public bool PickedUp;
    [HideInInspector]public PhoneCallManager manager;

    public PhoneCall(int _button, AudioClip _audio, GameObject _task, float _ringTime, PhoneCallManager _manager)
    {
        button = _button;
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
        if (_button != button) { return; }

        if(!PickedUp && !state) { PickUpCall(); }

        /*if(PickedUp && !state)
        {
            CancelCall();
        }*/
    }

    public void StartCall()
    {
        manager = PhoneCallManager.instance;
        manager.StartRinging();
        SerialConnect.instance.SwitchLed(button, true);
        manager.UpdatePhoneCalls += PhoneUpdate;
        SerialConnect.instance.ButtonEvent += ButtonEvent;
        timer = ringTime;
        PickedUp = false;
    }

    public void PickUpCall()
    {
        Debug.Log($"Phone {button} picked up");
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
        SerialConnect.instance.SwitchLed(button, false);
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
            manager.taskManager.AddTask(task);
        }
        manager.Player.Stop();
        SerialConnect.instance.SwitchLed(button, false);
        EndProcess();
    }

    private void EndProcess()
    {
        manager.UpdatePhoneCalls -= PhoneUpdate;
        SerialConnect.instance.ButtonEvent -= ButtonEvent;
    }
}
