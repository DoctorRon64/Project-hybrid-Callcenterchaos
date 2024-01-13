using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum TaskId
{
    Grandma,
    President,
    Italy,
    Homework
}

public class Task : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private string taskName;
    [SerializeField] private string taskDescription;
    [SerializeField] private TaskId taskId;

    [SerializeField] private Color deselectColor;
    [SerializeField] private Color selectColor;

    [Header("Options")]
    [SerializeField] private List<string> Options = new List<string>();
    [SerializeField] private Text[] optionsText;
    private int OptionCount => Options.Count;
    public int SelectedOption = 0;
    [SerializeField] private float timeDuration;
    [SerializeField] private int coinAmount;
    private float currentTime;

    [Header("References")]
    [SerializeField] private Text taskNameText;
    [SerializeField] private Text taskDescriptionText;
    [SerializeField] private Text timeText;
    [SerializeField] private Image timeDisplay;
    private TaskManager manager;
    [System.NonSerialized] public GameObject associatedGameObject;

    public void SetupTaskManager(TaskManager _manager)
    {
        manager = _manager;
    }

    private void UpdateUI()
    {
        taskNameText.text = taskName;
        taskDescriptionText.text = taskDescription;
        timeText.text = "Time left: " + currentTime.ToString("F1") + " seconds";
        timeDisplay.fillAmount = currentTime / timeDuration;

        for (int i = 0; i < Options.Count; i++)
        {
            optionsText[i].text = Options[i];
        }
    }

    public void StartTask()
    {
        currentTime = timeDuration;
        UpdateUI();
    }

    public bool SubmitAnswer(int answer)
    {
        if (answer < OptionCount) 
        {
            SelectedOption = answer;
            HandleTaskSubmission();
            return true;
        }
        else
        {
            Debug.LogWarning("Invalid answer option: " + answer);
            return false;
        }
    }

    private void HandleTaskSubmission()
    {
		manager.RemoveTask(this);
	}

    public void UpdateTaskTime(float deltaTime)
    {
        currentTime = Mathf.Clamp(currentTime - deltaTime, 0f, timeDuration);
        UpdateUI();
    }

    public bool IsTaskCompleted()
    {
        return currentTime <= 0f;
    }

    public void DeSelect()
    {
        GetComponent<Image>().color = deselectColor;
    }

    public void Select()
    {
        GetComponent<Image>().color = selectColor;
    }
}