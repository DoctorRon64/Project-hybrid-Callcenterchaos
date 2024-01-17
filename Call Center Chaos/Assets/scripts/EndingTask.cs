using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTask : Task
{
    [Header("set scene name to go to")]
    [SerializeField] protected string finishSceneName = "";

    public override void HandleTaskSubmission()
    {
        int selectedAnswer = SelectedOption;

        if (selectedAnswer >= 0 && selectedAnswer < Options.Count)
        {
            SceneManager.LoadScene(finishSceneName);
        }
        else
        {
            Debug.LogWarning("Invalid answer option: " + selectedAnswer);
        }
        manager.RemoveTask(this);
    }
}
