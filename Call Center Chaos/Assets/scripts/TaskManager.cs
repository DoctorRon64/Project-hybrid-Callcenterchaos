using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public List<Task> taskQueue = new List<Task>();
    [SerializeField] private int maxTaskCount = 4;

    [Header("UI References")]
    [SerializeField] private GameObject taskPrefab;

    private void Awake()
    {
        taskQueue.Clear();
    }

    private void Update()
    {
        if (taskQueue != null)
        {
            ProcessTasks(Time.deltaTime);
        }
    }

    [ContextMenu("AddTaskExample")]
    public void AddTaskExample()
    {
        AddTask(taskPrefab);
    }

    public void AddTask(GameObject taskObj)
    {
        GameObject instance = Instantiate(taskObj, transform);
        Task taskInstance = instance.GetComponent<Task>();

        if (taskInstance == null)
        {
            Debug.LogError("Task component not found on the instantiated object.");
            Destroy(instance);
            return;
        }

        taskInstance.associatedGameObject = instance;

        if (taskQueue.Count >= maxTaskCount)
        {
            RemoveOldestTask();
        }

        taskQueue.Add(taskInstance);
        taskInstance.StartTask();
    }

    private void RemoveOldestTask()
    {
        Task oldestTask = taskQueue[0];
        taskQueue.RemoveAt(0);
        Destroy(oldestTask.associatedGameObject);
    }

    public void RemoveTask(Task task)
    {
        if (taskQueue.Contains(task))
        {
            taskQueue.Remove(task);
            Destroy(task.associatedGameObject);
        }
    }

    public void TaskSelectUI(int _index)
    {
        foreach(var task in taskQueue)
        {
            if (task != taskQueue[_index])
            {
                task.DeSelect();
            }
        }
        taskQueue[_index].Select();
    }

    private void ProcessTasks(float deltaTime)
    {
        for (int i = taskQueue.Count - 1; i >= 0; i--)
        {
            Task task = taskQueue[i];

            task.UpdateTaskTime(deltaTime);

            if (task.IsTaskCompleted())
            {
                RemoveTask(task);
            }
        }
    }
}