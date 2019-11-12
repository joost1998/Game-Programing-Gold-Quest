using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : AbNPC
{
	public void Awake()
	{
		this.animator = GetComponent<Animator>();

		this.Health = 100;

		Sitting();
	}

	private void Update()
	{
		this.stateMachine.RunMonoExecute();
	}

	public void Sitting()
	{
		this.stateMachine.ChangeState(new Sitting(gameObject, animator));
	}

	public void Attacking()
	{
		this.stateMachine.ChangeState(new Attacking(gameObject, target, shootPrefab));
	}
}
