using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TutorialCall : PhoneCall
{
    [SerializeField] private PhoneCall nextCall;
    protected override void CancelCall()
    {
        Debug.Log("Current call cancelled");
        manager.Player.Stop();
        SerialConnect.instance.SwitchLed(false);
        manager.currentCall = null;
        if (nextCall != null)
        {
            PhoneCallManager.instance.AddCallToQueue(nextCall);
        }
        EndProcess();
    }

    protected override void EndCall()
    {
        Debug.Log("Current call ended");
        manager.Player.Stop();
        SerialConnect.instance.SwitchLed(false);
        manager.currentCall = null;
        if (nextCall != null)
        {
            PhoneCallManager.instance.AddCallToQueue(nextCall);
        }
        EndProcess();
    }
}
