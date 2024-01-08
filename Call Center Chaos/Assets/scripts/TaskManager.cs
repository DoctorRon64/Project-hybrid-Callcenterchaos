using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> taskList = new List<Task>();
    [SerializeField] private LimitedQueue<Task> limitedQueue = new LimitedQueue<Task>(5);

    private void Awake()
    {
        foreach (Task task in taskList)
        {
            limitedQueue.Enqueue(task);
        }

        Debug.Log("Current count: " + limitedQueue.Count);

        limitedQueue.Enqueue(taskList[2]);

        Debug.Log("Current count: " + limitedQueue.Count);
    }
}

