using UnityEngine;

public enum TaskId
{
    Grandma,
    President,
    Italy,
    Homework
}

[CreateAssetMenu(fileName = "NewTask", menuName = "Tasks/Task")]
public class Task : ScriptableObject
{
    [SerializeField] private string taskName;
    [SerializeField] private string taskDescription;
    [SerializeField] private float timeDuration;
    [SerializeField] private float currentTime;
    [SerializeField] private TaskId taskId;
    [SerializeField] private int coinAmount;
    [SerializeField] private bool taskAwnser = false;

    public string TaskName => taskName;
    public string Description => taskDescription;
    public float TimeDuration => timeDuration;
    public float CurrentTime
    {
        get => currentTime;
        set => currentTime = Mathf.Clamp(value, 0f, timeDuration);
    }

    public void StartTask()
    {
        currentTime = timeDuration;
    }
    
    public void SubmitAwnser(bool _awnser)
    {
        taskAwnser = _awnser;
        //stop
    }

    public void UpdateTaskTime(float deltaTime)
    {
        currentTime = Mathf.Clamp(currentTime - deltaTime, 0f, timeDuration);
    }

    public bool IsTaskCompleted()
    {
        return currentTime <= 0f;
    }
}
