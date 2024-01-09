using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
	[SerializeField] private List<Task> taskQueue = new List<Task>();
	[SerializeField] private int maxTaskCount = 4;

	[Header("UI References")]
	[SerializeField] private GameObject TaskPrefab;
	[SerializeField] private Text taskQueueText;

	private void Start()
	{
		UpdateTaskQueueUI();
	}

	private void Update()
	{
		UpdateTaskQueueUI();
		ProcessTasks(Time.deltaTime);
	}

	private void UpdateTaskQueueUI()
	{
		string queueText = "Task Queue:\n";
		foreach (Task task in taskQueue)
		{
			queueText += $"{task.TaskName}\n";
		}
		taskQueueText.text = queueText;
	}

	[ContextMenu("yeah")]
	public void AddTask()
	{
		GameObject instance = Instantiate(TaskPrefab, gameObject.transform);
		Task taskInstance = instance.GetComponent<Task>();
		if (taskInstance == null) { return; }

		if (taskQueue.Count < maxTaskCount)
		{
			taskQueue.Add(taskInstance);
			UpdateTaskQueueUI();

			taskInstance.StartTask();
		}
		else
		{
			Debug.LogWarning("Task queue is full. Cannot add more tasks.");
		}
	}

	public void RemoveTask(Task task)
	{
		if (taskQueue.Contains(task))
		{
			taskQueue.Remove(task);
			UpdateTaskQueueUI();
		}
	}

	public void ProcessTasks(float deltaTime)
	{
		for (int i = taskQueue.Count - 1; i >= 0; i--)
		{
			Task task = taskQueue[i];

			task.UpdateTaskTime(deltaTime);

			if (task.IsTaskCompleted())
			{
				taskQueue.RemoveAt(i);
				UpdateTaskQueueUI();
			}
		}
	}
}
