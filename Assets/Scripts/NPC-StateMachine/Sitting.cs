using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : IState
{
	//Enemy GameObject
	GameObject obj;

	//Assign Animator to change bool in the Blend Tree
	Animator animator;

	public Sitting(GameObject obj)
	{
		this.obj = obj;
		this.animator = obj.GetComponent<Animator>();
	}

	//Start Function
	public void Enter()
	{
		//Set sleeping to true, animation loop
		animator.SetBool("wakeUp", false);
	}

	//Update Function
	public void Execute()
	{
		//No Code needed - Sleep animations will automaticaly called.
	}

	//Before new state Function
	public void Exit()
	{
		//Set sleeping to false, ready to Fight
		animator.SetBool("wakeUp", true);
	}
}
