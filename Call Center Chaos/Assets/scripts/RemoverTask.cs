using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverTask : Task
{
    [SerializeField] private List<TaskId> RemoveOption0 = new List<TaskId>();
    [SerializeField] private List<TaskId> RemoveOption1 = new List<TaskId>();
    [SerializeField] private List<TaskId> RemoveOption2 = new List<TaskId>();

    protected override void SubmitAnswerToPhoneCall(int answer)
    {
        if (answer < Calls.Count)
        {
            PhoneCall selectedCall = Calls[answer];
            MoneyManager.instance.AddAmount(Rewards[answer]);
            List<TaskId> toRemove;
            switch (answer)
            {
                case 0: toRemove = RemoveOption0; break;
                case 1: toRemove = RemoveOption1; break;
                default: toRemove = RemoveOption2; break;
            }
            foreach (TaskId id in toRemove)
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
