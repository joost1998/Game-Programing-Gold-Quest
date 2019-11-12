using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : IState
{
	GameObject obj;

	Animator animator;

	public Sitting(GameObject obj, Animator animator)
	{
		this.obj = obj;
		this.animator = animator;
	}

	public void Enter()
	{
		//animator.SetBool("wakeUp", false);
		obj.GetComponent<Animator>().SetBool("wakeUp", false);
	}

	public void Execute()
	{
		
	}

	public void Exit()
	{
		obj.GetComponent<Animator>().SetBool("wakeUp", true);
	}
}
