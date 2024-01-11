using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCallManager : MonoBehaviour
{
    public static PhoneCallManager instance;

    [HideInInspector] public TaskManager taskManager;
    public AudioSource Player;
    public AudioClip Ringtone;

    public delegate void Updater(float deltaTime);
    public Updater UpdatePhoneCalls;

    [SerializeField] private List<PhoneCall> initialPhoneCalls = new();
    [SerializeField] private Queue<PhoneCall> phoneCallQueue = new();
    public PhoneCall currentCall;

    private void Start()
    {
        if (initialPhoneCalls.Count > 0)
        {
            foreach (PhoneCall phoneCall in initialPhoneCalls) { phoneCallQueue.Enqueue(phoneCall); }
        }
        instance = this;
        taskManager = FindObjectOfType<TaskManager>();
    }

    private void Update()
    {
        UpdatePhoneCalls?.Invoke(Time.deltaTime);

        if (currentCall == null)
        {
            StartNextCall();
        }
    }

    public void AddCallToQueue(PhoneCall call)
    {
        phoneCallQueue.Enqueue(call);

    }

    private void StartNextCall()
    {
        if (phoneCallQueue.Count > 0)
        {
            currentCall = phoneCallQueue.Dequeue();
            currentCall.StartCall();
            Debug.Log($"Starting call; {phoneCallQueue.Count} left in queue");
        }
        else
        {
            //score screen??
            return;
        }
    }

    public void StartRinging()
    {
        if (Ringtone != null)
        {
            Player.clip = Ringtone;
            Player.Play();
        }
    }

    public void StopRinging()
    {
        if (Player.clip == Ringtone)
        {
            Player.Stop();
        }
    }

    [ContextMenu("Debug Pickup")]
    public void DebugPickup()
    {
        if (currentCall != null)
        {
            currentCall.PickUpCall();
        }
    }
}
