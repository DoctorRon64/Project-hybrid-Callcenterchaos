using UnityEngine;

public class GameMasterControls : MonoBehaviour
{
	[SerializeField] private TaskManager taskManager;
	[SerializeField] private KeyCode[] keyCodes = new KeyCode[5];
	private int selectedTaskIndex = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) && taskManager.taskQueue.Count > 1)
		{
			SelectPreviousTask();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) && taskManager.taskQueue.Count > 1)
		{
			SelectNextTask();
		}

		// Select AnswerOption with 1, 2, or 3 keys
		HandleKeyInput(keyCodes[2], AnswerOptions.Option1);
		HandleKeyInput(keyCodes[3], AnswerOptions.Option2);
		HandleKeyInput(keyCodes[4], AnswerOptions.Option3);
	}

	private void HandleKeyInput(KeyCode _keyCode, AnswerOptions _option)
	{
		if (Input.GetKeyDown(_keyCode))
		{
			SubmitAnswer(_option);
		}
	}

	private void SelectNextTask()
	{
		if (taskManager.taskQueue.Count > 1)
		{
			selectedTaskIndex = (selectedTaskIndex + 1) % taskManager.taskQueue.Count;
			taskManager.TaskSelectUI(selectedTaskIndex);
		}
		else if (taskManager.taskQueue.Count == 1)
		{
			selectedTaskIndex = 0;
			taskManager.TaskSelectUI(selectedTaskIndex);
		}
		else
		{
			Debug.Log("No tasks left in the queue.");
		}
	}

	private void SelectPreviousTask()
	{
		selectedTaskIndex = (selectedTaskIndex - 1 + taskManager.taskQueue.Count) % taskManager.taskQueue.Count;
		taskManager.TaskSelectUI(selectedTaskIndex);
	}

	private void SubmitAnswer(AnswerOptions answer)
	{
		if (selectedTaskIndex >= 0 && selectedTaskIndex < taskManager.taskQueue.Count)
		{
			Task selectedTask = taskManager.taskQueue[selectedTaskIndex];
			selectedTask.SubmitAnswer(answer);

			taskManager.RemoveTask(selectedTask);

			if (taskManager.taskQueue.Count > 0)
			{
				SelectNextTask();
			}
		}
	}
}
