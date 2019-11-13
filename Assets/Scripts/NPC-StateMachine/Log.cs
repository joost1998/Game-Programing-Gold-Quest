using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : AbNPC
{
	public void Awake()
	{
		//Set Health of the Log;
		this.Health = 100;

		//Set the state to Sitting (Sleeping);
		Sitting();
	}

	//Run the execute function in the state class to function like a update function;
	private void Update()
	{
		this.stateMachine.RunMonoExecute();
	}

	//Set the state to Sitting (Sleeping);
	public void Sitting()
	{
		this.stateMachine.ChangeState(new Sitting(gameObject));
	}

	//Set the state to Attacking enemy;
	public void Attacking()
	{
		this.stateMachine.ChangeState(new Attacking(gameObject, target, shootPrefab));
	}
}
