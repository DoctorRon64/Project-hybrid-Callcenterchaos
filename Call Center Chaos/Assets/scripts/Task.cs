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
	[SerializeField] private int coinAmount;

	[Header("Time")]
	[SerializeField] private float timeDuration;
	[SerializeField] private float currentTime;

	[Header("Answer")]
	[SerializeField] private bool taskAnswer = false;

	[SerializeField] private Text taskNameText;
	[SerializeField] private Text taskDescriptionText;
	[SerializeField] private Text timeText;
	[SerializeField] private Image timeDisplay;

	private void UpdateUI()
	{
		taskNameText.text = taskName;
		taskDescriptionText.text = taskDescription;
		timeText.text = "Time left: " + currentTime.ToString("F2") + " seconds";
		timeDisplay.fillAmount = currentTime / timeDuration;
	}

	public string TaskName
	{
		get => taskName;
		set
		{
			taskName = value;
			UpdateUI();
		}
	}
	public string Description
	{
		get => taskDescription;
		set
		{
			taskDescription = value;
			UpdateUI();
		}
	}
	public float CurrentTime
	{
		get => currentTime;
		set
		{
			currentTime = Mathf.Clamp(value, 0f, timeDuration);
			UpdateUI();
		}
	}
	public float TimeDuration => timeDuration;
	public void StartTask()
	{
		currentTime = timeDuration;
		UpdateUI();
	}

	public void SubmitAnswer(bool answer)
	{
		taskAnswer = answer;
		// Stop or continue based on your logic
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
}
