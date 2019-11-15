using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbNPC : MonoBehaviour
{
	[NonSerialized]
	public StateMachineBehaviour stateMachine = new StateMachineBehaviour();

	[NonSerialized]
	public GameObject target;

	[NonSerialized]
	public int Health;

	public GameObject shootPrefab;

	//Remove the damage from the health
	public void Hit(int damage)
	{
		this.Health -= damage;
		CheckHealth();
	}

	//Add health function
	public void addHealth(int health)
	{
		this.Health += health;
	}

	//Check if a Log have enough health
	private void CheckHealth()
	{
		if (this.Health <= 0)
		{
			Destroy(gameObject);
		}
	}

	//Set the target of the Log
	public void SetTarget(GameObject target)
	{
		this.target = target;
	}
}
