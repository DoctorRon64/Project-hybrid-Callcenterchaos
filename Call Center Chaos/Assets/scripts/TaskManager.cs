using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    public List<Task> taskQueue = new List<Task>();
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
        taskInstance.SetupTaskManager(this);
        taskInstance.StartTask();
        taskQueue.Add(taskInstance);

        if (taskQueue.Count > maxTaskCount)
        {
            RemoveOldestTask();
        }
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
			if (taskQueue.Count == 1)
			{
				taskQueue.Clear();
			}
			else
			{
				taskQueue.Remove(task);
			}
			DestroyImmediate(task.associatedGameObject);
		}
	}


	public void TaskSelectUI(int index)
    {
        if (index >= 0 && index < taskQueue.Count)
        {
            foreach (var task in taskQueue)
            {
                if (task != taskQueue[index])
                {
                    task.DeSelect();
                }
            }
            taskQueue[index].Select();
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