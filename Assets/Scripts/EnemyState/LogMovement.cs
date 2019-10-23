using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMovement : MonoBehaviour
{
	//Private local vars
	public GameObject target;
	private float chaseRadius;
	private float attackRadius;
	private int health;
	private LogState State;
	private float timerSpeed = 0.5F;

	//Public vars for Unity
	public GameObject shootImage;

	private void Awake()
	{
		InvokeRepeating("Shoot", timerSpeed, timerSpeed);
	}

	//MonoBehaviour standard function - Sets and create vars
	void Start()
    {
		//target = GameObject.FindWithTag("Player");
		chaseRadius = 10;
		attackRadius = 1;
		health = 40;
		State = LogState.Sitting;
    }

	//MonoBehaviour standard function - state switch - call the right functions for that state
    void Update()
    {
		switch(State)
		{
			case LogState.Attacking:
				Attacking();
				break;
		}
    }

	//STATE: ATTACKING - Walk towards the attacker
	void Attacking()
	{
		if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius && Vector3.Distance(target.transform.position, transform.position) > attackRadius)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2 * Time.deltaTime);
		}
	}

	//STATE: ATTACKING - Targets the damager and shoot at it
	void Shoot()
	{
		if (State == LogState.Attacking)
		{
			Vector2 shootingDirection = (target.transform.position - transform.position).normalized * 7F;
			GameObject bullet = Instantiate(shootImage, transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().sender = "Log";
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingDirection.x, shootingDirection.y);
			bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
			Destroy(bullet, 2.0f);
		}
	}

	//NO-STATE: Called by an extern Script
	public void hit(int damage)
	{
		State = LogState.Attacking;
		health -= damage;
		//Call the health check function
		checkHealth();
	}

	//Checking if the log has health, otherwise destroy Object
	public void checkHealth()
	{
		if (health <= 0)
		{
			//Destroys this object
			Destroy(gameObject);
		}
	}
}
