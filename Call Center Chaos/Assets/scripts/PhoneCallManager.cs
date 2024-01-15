using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhoneCallManager : MonoBehaviour
{
    public static PhoneCallManager instance;

    [HideInInspector] public TaskManager taskManager;
    public AudioSource Player;
    public AudioClip Ringtone;

    public delegate void Updater(float deltaTime);
    public Updater UpdatePhoneCalls;

    private List<TaskId> removeable = new List<TaskId>();
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
            StartCoroutine(StartNextCall());
        }
    }

    public void AddCallToQueue(PhoneCall call)
    {
        phoneCallQueue.Enqueue(call);

    }

    private IEnumerator StartNextCall()
    {
        if (phoneCallQueue.Count > 0)
        {
            PhoneCall newCall = phoneCallQueue.Dequeue();
            while (removeable.Contains(newCall.task.GetComponent<Task>().taskId))
            {
                newCall = phoneCallQueue.Dequeue();
            }
            yield return new WaitForSeconds(3);
            currentCall.StartCall();
            Debug.Log($"Starting call; {phoneCallQueue.Count} left in queue");
        }
        else
        {
            
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

    public void RemoveAllOfTag(TaskId tag)
    {
        removeable.Add(tag);
    }
}
