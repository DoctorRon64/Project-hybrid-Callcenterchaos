using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTimeHolder : MonoBehaviour
{
    public static TaskTimeHolder instance;

    public float presidentTime;
    public float AITime;
    public float italyTime;
    public float homeworkTime;
    void Start()
    {

    }

    public float GetTaskDuration(TaskId taskId) {
        switch (taskId)
        {
            case TaskId.AI: return AITime;
            case TaskId.President: return presidentTime;
            case TaskId.Italy: return italyTime;
            default: return homeworkTime;
        }
    } }
