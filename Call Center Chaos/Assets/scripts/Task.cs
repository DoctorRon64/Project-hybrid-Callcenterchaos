using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TaskId
{
    AI,
    President,
    Italy,
    Homework
}

public class Task : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] protected string taskName;
    [SerializeField] protected string taskDescription;
    [SerializeField] public TaskId taskId;

    [Header("Options")]
    [SerializeField] protected List<string> Options = new List<string>();
    [SerializeField] protected List<AudioClip> Responses = new List<AudioClip>();
    [SerializeField] public List<PhoneCall> Calls = new List<PhoneCall>();
    [SerializeField] protected List<int> Rewards = new List<int>();
    [SerializeField] public int OnCancelOption;
    protected int OptionCount => Options.Count;
    public int SelectedOption = 0;
    [SerializeField]
    protected float timeDuration
    {
        get
        {
            return TaskTimeHolder.instance.GetTaskDuration(taskId);
        }
    }
    [SerializeField] protected int coinAmount;
    [SerializeField] protected bool endTaskTree = false;
    [SerializeField] protected string finishSceneName = "";
    protected float currentTime;

    [Header("References please dont edit")]
    [SerializeField] protected Text[] optionsText;
    [SerializeField] protected Color deselectColor;
    [SerializeField] protected Color selectColor;
    [SerializeField] protected Text taskNameText;
    [SerializeField] protected Text taskDescriptionText;
    [SerializeField] protected Text timeText;
    [SerializeField] protected Image timeDisplay;
    protected TaskManager manager;
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
        int selectedAnswer = SelectedOption;

        if (selectedAnswer >= 0 && selectedAnswer < Options.Count && !endTaskTree)
        {
            SubmitAnswerToPhoneCall(selectedAnswer);
        }
        else
        {
            Debug.LogWarning("Invalid answer option: " + selectedAnswer);
        }

        if (endTaskTree)
        {
            SceneManager.LoadScene(finishSceneName);
        }

        manager.RemoveTask(this);
    }

    protected virtual void SubmitAnswerToPhoneCall(int answer)
    {
        if (answer < Calls.Count)
        {
            PhoneCall selectedCall = Calls[answer];
            MoneyManager.instance.AddAmount(Rewards[answer]);
            PhoneCallManager.instance.Player.clip = Responses[answer];
            PhoneCallManager.instance.Player.Play();
            if (selectedCall != null)
            {
                PhoneCallManager.instance.AddCallToQueue(selectedCall);
            }
        }
        else
        {
            Debug.LogWarning("Invalid answer option: " + answer);
        }
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