using System.Collections;
using UnityEngine;

public class GameMasterControls : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private KeyCode[] keyCodes = new KeyCode[5];
    private int selectedTaskIndex = 0;

    private void Update()
    {
        int taskQueueCount = taskManager.taskQueue.Count;

        if (Input.anyKeyDown && taskQueueCount > 0)
        {
            if (taskQueueCount == 1)
            {
                SelectSingleTask();
            }
            else
            {
                TaskSelector();
                TaskOptions();
            }
        }
    }

    private void SelectSingleTask()
    {
        taskManager?.TaskSelectUI(0);
    }

    private void TaskSelector()
    {
        HandleKeyInput(keyCodes[0], -1);
        HandleKeyInput(keyCodes[1], 1);
    }

    private void HandleKeyInput(KeyCode keyCode, int delta)
    {
        if (Input.GetKeyDown(keyCode))
        {
            taskManager?.TaskSelectUI(selectedTaskIndex);
            ChangeSelectedTask(delta);
        }
    }

    private void ChangeSelectedTask(int delta)
    {
        selectedTaskIndex = (selectedTaskIndex + delta + taskManager.taskQueue.Count) % taskManager.taskQueue.Count;
        taskManager?.TaskSelectUI(selectedTaskIndex);
    }

    private void TaskOptions()
    {
        HandleOptionInput(keyCodes[2], AnswerOptions.Option1);
        HandleOptionInput(keyCodes[3], AnswerOptions.Option2);
        HandleOptionInput(keyCodes[4], AnswerOptions.Option3);
    }

    private void HandleOptionInput(KeyCode keyCode, AnswerOptions option)
    {
        if (Input.GetKeyDown(keyCode))
        {
            taskManager?.taskQueue[selectedTaskIndex].SubmitAnswer(option);
        }
    }
}
