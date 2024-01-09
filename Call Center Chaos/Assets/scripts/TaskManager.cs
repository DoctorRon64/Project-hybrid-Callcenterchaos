using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
	[SerializeField] private List<Task> taskQueue = new List<Task>();
	[SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
	[SerializeField] private int maxTaskCount = 4;

	[Header("UI References")]
	[SerializeField] private GameObject TaskPrefab;
	[SerializeField] private Text taskQueueText;

	private void Update()
	{
		ProcessTasks(Time.deltaTime);
	}

	[ContextMenu("AddTaskExample")]
	public void AddTaskExample()
	{
		AddTask("ExampleTask", "This is an example task.", 10, TaskId.Grandma, 10);
	}

	public void AddTask(string _taskName, string _taskDescription, float _timeduration, TaskId _taskId, int _coinAmount)
	{
		GameObject instance = Instantiate(TaskPrefab, gameObject.transform);
		gameObjects.Add(instance);

		Task taskInstance = instance.GetComponent<Task>();
		if (taskInstance == null) { return; }
		
		taskInstance.TaskName = _taskName;
		taskInstance.Description = _taskDescription;
		taskInstance.ID = _taskId;
		taskInstance.TimeDuration = _timeduration;
		taskInstance.CoinAmount = _coinAmount;

		if (taskQueue.Count < maxTaskCount)
		{
			taskQueue.Add(taskInstance);
			taskInstance.StartTask();
		}
		else
		{
			Task lastTask = taskQueue[0];
			taskQueue.RemoveAt(0);
			gameObjects.Remove(lastTask.gameObject);
			Destroy(lastTask.gameObject);

			taskQueue.Add(taskInstance);

			taskInstance.StartTask();
		}
	}

	public void RemoveTask(Task task)
	{
		if (taskQueue.Contains(task))
		{
			taskQueue.Remove(task);
			gameObjects.Remove(task.gameObject);
			Destroy(task.gameObject);
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
				gameObjects.Remove(task.gameObject);
				Destroy(task.gameObject);
			}
		}
	}
}
