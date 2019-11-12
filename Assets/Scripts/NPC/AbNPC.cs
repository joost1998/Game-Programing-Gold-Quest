﻿/*
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

	public GameObject shootPrefab;

	public void Hit(int damage)
	{
		this.Health -= damage;
		CheckHealth();
	}

	public void CheckHealth()
	{
		if (this.Health <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void SetTarget(GameObject target)
	{
		this.target = target;
	}
}
