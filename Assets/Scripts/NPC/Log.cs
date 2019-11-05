using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : AbNPC
{
	public void Update()
	{
		this.stateMachine.RunMonoExecute();
	}

	public void Sitting()
	{
		this.stateMachine.ChangeState(new Sitting(gameObject));
	}

	public void Attacking()
	{
		this.stateMachine.ChangeState(new Attacking(gameObject, target, shootPrefab));
	}
}
