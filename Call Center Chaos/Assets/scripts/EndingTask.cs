using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTask : Task
{
	protected override void SubmitAnswerToPhoneCall(int answer)
	{
		if (answer < Calls.Count)
		{
			MoneyManager.instance.AddAmount(Rewards[answer]);
			PhoneCallManager.instance.Player.clip = Responses[answer];
			PhoneCallManager.instance.Player.Play();
		}
		else
		{
			Debug.LogWarning("Invalid answer option: " + answer);
		}
	}
}
