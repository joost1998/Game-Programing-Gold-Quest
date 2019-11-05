using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMovement : MonoBehaviour
{
	//Private local vars
	private ParticleSystem ps;
	private float chaseRadius;
	private float attackRadius;
	private int health;
	private LogState State;
	private float timerSpeed;
	private Animation an;

	//Public vars for Unity
	public GameObject target;
	public GameObject shootImage;
	public AnimationClip clip;

	private void Awake()
	{
		timerSpeed = 2.5F;
		InvokeRepeating("Shoot", timerSpeed, timerSpeed);
		InvokeRepeating("Attacking", 6f, 6f);
	}

	//MonoBehaviour standard function - Sets and create vars
	void Start()
    {
		//target = GameObject.FindWithTag("Player");
		chaseRadius = 10;
		attackRadius = 4;
		health = 40;
		State = LogState.Sitting;

		ps = GetComponent<ParticleSystem>();
		an = GetComponent<Animation>();

		State = LogState.Sitting;
	}

	//MonoBehaviour standard function - state switch - call the right functions for that state
    void Update()
    {
		switch(State)
		{
			case LogState.Sitting:
				Sitting();
				break;

			case LogState.Attacking:
				Walking();
				break;

			case LogState.Loving:
				Loving();
				break;
		}
    }

	void Sitting()
	{
		//an.clip = clip;
		//an.Play();
	}

	//STATE: ATTACKING - Walk towards the attacker
	void Walking()
	{
		if (target != null) {
			if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius && Vector3.Distance(target.transform.position, transform.position) > attackRadius)
			{
				transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2 * Time.deltaTime);
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x + 2, target.transform.position.x + -2), 2 * Time.deltaTime);
			}
		}
	}
 
	//STATE: ATTACKING - Targets the damager and shoot at it
	void Shoot()
	{
		if (State == LogState.Attacking && target != null)
		{
			if(target.name.Contains("Player"))
			{
				if (target.GetComponent<PlayerMovement>().hasApple())
				{
					State = LogState.Loving;
					return;
				}
			}
			Vector2 shootingDirection = (target.transform.position - transform.position).normalized * 7F;
			GameObject bullet = Instantiate(shootImage, transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().sender = "Log";
			bullet.GetComponent<Bullet>().target = gameObject;
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingDirection.x, shootingDirection.y);
			bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
			Destroy(bullet, 2.0f);
		}
	}

	//STATE: LOVING - Targets other logs because player has a apple
	public void Loving()
	{
		
	}

	//NO-STATE: Called by an extern Script
	public void hit(int damage)
	{
		State = LogState.Attacking;
		health -= damage;
		//ps.Play();
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

	public void setTarget(GameObject target)
	{
		State = LogState.Attacking;
		this.target = target;
	}

	//Getting 
	Transform GetClosestEnemy(List<Transform> enemies, Transform fromThis)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = fromThis.position;
		foreach (Transform potentialTarget in enemies)
		{
			Vector3 directionToTarget = potentialTarget.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if (dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}
		//closestEnemy = GetClosestEnemy(yourscript.yourtransformlist, this.transform);
		return bestTarget;
	}
}
