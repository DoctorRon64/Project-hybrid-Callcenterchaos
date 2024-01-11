using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public enum TaskId
{
	Grandma,
	President,
	Italy,
	Homework
}
public enum AwnserOptions
{
    Option1,
    Option2,
    Option3,
}

public class Task : MonoBehaviour
{
	[Header("Values")]
	[SerializeField] private string taskName;
	[SerializeField] private string taskDescription;
	[SerializeField] private TaskId taskId;
    [SerializeField] private Task parentTask;

	[SerializeField] private Color deselectColor;
	[SerializeField] private Color SelectColor;

    [Header("Options")]
    [SerializeField] private string[] Options = new string[3];
    public AwnserOptions awnserOption { get; private set; }
	[SerializeField] private float timeDuration;
	private float currentTime;
	[SerializeField] private int coinAmount;

    [Header("References")]
    [System.NonSerialized] public GameObject associatedGameObject;
    [SerializeField] private Text taskNameText;
	[SerializeField] private Text taskDescriptionText;
	[SerializeField] private Text timeText;
	[SerializeField] private Image timeDisplay;
	[SerializeField] private Text OptionText1;
	[SerializeField] private Text OptionText2;
	[SerializeField] private Text OptionText3;

    private void UpdateUI()
	{
		taskNameText.text = taskName;
		taskDescriptionText.text = taskDescription;
		timeText.text = "Time left: " + currentTime.ToString("F1") + " seconds";
		timeDisplay.fillAmount = currentTime / timeDuration;

		OptionText1.text = Options[0];
		OptionText2.text = Options[1];
		OptionText3.text = Options[2];
	}
	public void StartTask()
	{
		currentTime = timeDuration;
		UpdateUI();
	}

    public void SubmitAnswer(AwnserOptions _answer)
    {
        awnserOption = _answer;
		currentTime = 0f;
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
		GetComponent<Image>().color = SelectColor;
    }
}
