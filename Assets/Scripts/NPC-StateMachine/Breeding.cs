using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breeding : IState
{
	//Enemy GameObject
	GameObject obj;

	//Assign Animator to change bool in the Blend Tree
	Animator animator;

	public ParticleSystem ps;

	//Timer for extra Health
	double countdownHealth;

	public Breeding(GameObject obj)
	{
		this.obj = obj;
		this.animator = obj.GetComponent<Animator>();
		this.ps = obj.GetComponent<ParticleSystem>();
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
		//Cooldown for add Health target
		if (countdownHealth < 0.01)
		{
			//Play Breeding particle effect
			ps.Play();

			//Get the object to add Health
			obj.GetComponent<Log>().addHealth(1);
			countdownHealth = 10.00;
		}
		countdownHealth = countdownHealth - 0.01;
	}

	//Before new state Function
	public void Exit()
	{
		//Set sleeping to false, ready to Fight
		animator.SetBool("wakeUp", true);
	}
}
