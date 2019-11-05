using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : IState
{
	GameObject obj, target, shootPrefab;

	public Attacking(GameObject obj, GameObject target, GameObject shootPrefab)
	{
		this.obj = obj;
		this.target = target;
		this.shootPrefab = shootPrefab;
	}

	public void Enter()
	{
		
	}

	public void Execute()
	{
		Shoot();
		Walk();
	}

	public void Exit()
	{
		
	}

	private void Shoot()
	{
		Vector2 shootingDirection = (target.transform.position - obj.transform.position).normalized * 7F;
		GameObject bullet = Log.Instantiate(shootPrefab, obj.transform.position, Quaternion.identity);
		bullet.GetComponent<Bullet>().SetTarget(obj);
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingDirection.x, shootingDirection.y);
		bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
		Log.Destroy(bullet, 2.0f);
	}

	private void Walk()
	{
		if (target != null)
		{
			if (Vector3.Distance(target.transform.position, obj.transform.position) <= 10 && Vector3.Distance(target.transform.position, obj.transform.position) > 4)
			{
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.transform.position, 2 * Time.deltaTime);
			}
			else
			{
				obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(target.transform.position.x + 2, target.transform.position.x + -2), 2 * Time.deltaTime);
			}
		}
	}
}
