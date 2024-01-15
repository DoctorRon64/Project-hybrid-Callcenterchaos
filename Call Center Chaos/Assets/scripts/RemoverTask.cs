using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverTask : Task
{
    [SerializeField] private List<List<TaskId>> IdsToRemove = new List<List<TaskId>>();

    protected override void SubmitAnswerToPhoneCall(int answer)
    {
        if (answer < Calls.Count)
        {
            PhoneCall selectedCall = Calls[answer];
            MoneyManager.instance.AddAmount(Rewards[answer]);
            foreach (TaskId id in IdsToRemove[answer])
            {
                PhoneCallManager.instance.RemoveAllOfTag(id);
            }
            selectedCall.StartCall();
        }

        else
        {
            Debug.LogWarning("Invalid answer option: " + answer);
        }
    }
}
