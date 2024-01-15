using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingCallAnimationHandler : MonoBehaviour
{
    Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void SetCall(bool _bool)
	{
		animator.SetBool("call", _bool);
	}
}
