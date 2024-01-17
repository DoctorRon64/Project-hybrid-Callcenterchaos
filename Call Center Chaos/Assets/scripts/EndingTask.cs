using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTask : Task
{
    public override void HandleTaskSubmission()
    {
        int selectedAnswer = SelectedOption;

        if (selectedAnswer >= 0 && selectedAnswer < Options.Count)
        {
            //do nothing
        }
        else
        {
            Debug.LogWarning("Invalid answer option: " + selectedAnswer);
        }
        manager.RemoveTask(this);
    }
}
