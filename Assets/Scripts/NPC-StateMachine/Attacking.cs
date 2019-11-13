using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : IState
{
	//Create GameObjects;
	GameObject obj, target, shootPrefab;

	//Use a double to make a cooldown foor shooting;
	double countdownShoot;

	public Attacking(GameObject obj, GameObject target, GameObject shootPrefab)
	{
		this.obj = obj;
		this.target = target;
		this.shootPrefab = shootPrefab;
	}

	//Start Function
	public void Enter()
	{
		obj.GetComponent<Animator>().SetBool("wakeUp", true);
	}

	//Update Function
	public void Execute()
	{
		Walk();

		//Cooldown for shooting target
		if(countdownShoot < 0.01)
		{
			Shoot();
			countdownShoot = 4.00;
		}
		countdownShoot = countdownShoot - 0.01;
	}

	//Before new state Function
	public void Exit()
	{
		obj.GetComponent<Animator>().SetBool("wakeUp", false);
	}

	private void Shoot()
	{
		//A try for if target is destroyd, change state
		try
		{
			//Calculating the shooting direction and shoot
			Vector2 shootingDirection = (target.transform.position - obj.transform.position).normalized * 7F;
			GameObject bullet = Log.Instantiate(shootPrefab, obj.transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().SetTarget(obj);
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingDirection.x, shootingDirection.y);
			bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
			Log.Destroy(bullet, 2.0f);
		} catch(MissingReferenceException e)
		{
			//Change the state to the previous state
			obj.GetComponent<Log>().stateMachine.SwitchToPreviousState();
		}
	}

	private void Walk()
	{
		if (target != null)
		{
			//Walk to the Target
			if (Vector3.Distance(target.transform.position, obj.transform.position) <= 10 && Vector3.Distance(target.transform.position, obj.transform.position) > 4)
			{
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.transform.position, 2 * Time.deltaTime);
			}
			else
			{
				//If target is close: keep moving
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(target.transform.position.x + 2, target.transform.position.x + -2), 2 * Time.deltaTime);
			}
		}
	}
}
