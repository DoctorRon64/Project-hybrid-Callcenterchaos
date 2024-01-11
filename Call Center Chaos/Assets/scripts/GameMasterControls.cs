using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterControls : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private KeyCode[] keyCodes = new KeyCode[5];
    private Task SelectedTask;
    private int SelectedTaskIndex = 0;

    private void Update()
    {
        if (Input.anyKeyDown && taskManager.taskQueue != null)
        {
            TaskSelector();
            TaskOptions();
        }
    }

    private void TaskSelector()
    {
        HandleKeyInput(keyCodes[0], "Left Task", -1);
        HandleKeyInput(keyCodes[1], "Right Task", 1);
    }

    private void HandleKeyInput(KeyCode keyCode, string debugMessage, int delta)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log(debugMessage);
            taskManager?.TaskSelectUI(SelectedTaskIndex);
            ChangeSelectedTask(delta);
        }
    }

    private void ChangeSelectedTask(int delta)
    {
        SelectedTaskIndex += delta;
        SelectedTaskIndex = Mathf.Clamp(SelectedTaskIndex, 0, taskManager.taskQueue.Count - 1);
        SelectedTask = taskManager?.taskQueue[SelectedTaskIndex];
    }
    private void TaskOptions()
    {
        HandleOptionInput(keyCodes[2], AwnserOptions.Option1);
        HandleOptionInput(keyCodes[3], AwnserOptions.Option2);
        HandleOptionInput(keyCodes[4], AwnserOptions.Option3);
    }

    private void HandleOptionInput(KeyCode keyCode, AwnserOptions option)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log("Option " + option);
            taskManager?.taskQueue[SelectedTaskIndex].SubmitAnswer(option);
        }
    }
}
