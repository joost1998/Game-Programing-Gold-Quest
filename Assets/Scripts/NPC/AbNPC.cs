/*
 *	Used Bron To Create This State Machine: https://www.youtube.com/watch?v=D6hAftj3AgM
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbNPC : MonoBehaviour
{
	[NonSerialized]
	public StateMachineBehaviour stateMachine = new StateMachineBehaviour();

	[NonSerialized]
	public Animator animator;

	[NonSerialized]
	public GameObject target;

	[NonSerialized]
	public int Health;

	private void Start()
	{
		this.animator = GetComponent<Animator>();

		this.Health = 100;
	}

	public void hit(int damage)
	{
		//State = LogState.Attacking;
		this.Health -= damage;
		checkHealth();
	}

	public void checkHealth()
	{
		if (this.Health <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void SetTarget(GameObject target)
	{
		this.target = target;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		SetTarget(collision.GetComponent<Bullet>().sender);
	}
}
