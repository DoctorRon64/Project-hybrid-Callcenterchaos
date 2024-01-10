using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCall
{
    public int button;
    public AudioClip Audio;
    private PhoneCallManager manager;
    public TaskData task;
    public float ringTime;
    public bool PickedUp;

    public PhoneCall(int _button, AudioClip _audio, TaskData _task, float _ringTime, PhoneCallManager _manager)
    {
        button = _button;
        Audio = _audio;
        task = _task;
        ringTime = _ringTime;
        manager = _manager;

        manager.UpdatePhoneCalls += Update;
        SerialConnect.instance.ButtonEvent += ButtonEvent;


    }

    private void Update(float deltaTime)
    {
        ringTime -= deltaTime;
        if (ringTime < 0 && !PickedUp)
        {
            CancelCall();
        }
        if(ringTime < 0 && PickedUp)
        {
            EndCall();
        }
    }

    private void ButtonEvent(int _button, bool state)
    {
        if (_button != button) { return; }

        if(!PickedUp && state) { PickUpCall(); }
    }

    private void PickUpCall()
    {
        PickedUp = true;
        ringTime = Audio.length + 1;
        manager.Player.clip = Audio;
        manager.Player.Play();
    }

    private void CancelCall()
    {

    }

    private void EndCall()
    {

    }
}

public struct TaskData
{
    public string name;
    public string description;
    public TaskId taskId;
    public int coinAmount;
    public int duration;
}
