using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> taskQueue = new List<Task>();
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] private int maxTaskCount = 4;

    [Header("UI References")]
    [SerializeField] private GameObject taskPrefab;

    private void Update()
    {
        ProcessTasks(Time.deltaTime);
    }

    [ContextMenu("AddTaskExample")]
    public void AddTaskExample()
    {
        AddTask("ExampleTask", "This is an example task.", 10, TaskId.Grandma, 10);
    }

    public void AddTask(string _taskName, string _taskDescription, float _timeDuration, TaskId _taskId, int _coinAmount)
    {
        GameObject instance = Instantiate(taskPrefab, transform);
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