using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : AbNPC
{
	public void Update()
	{
		this.stateMachine.RunMonoExecute();
	}

	void Sitting()
	{

	}

	void Attacking()
	{
		if (target != null)
		{
			if (Vector3.Distance(target.transform.position, transform.position) <= 10 && Vector3.Distance(target.transform.position, transform.position) > 4)
			{
				transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 2 * Time.deltaTime);
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x + 2, target.transform.position.x + -2), 2 * Time.deltaTime);
			}
		}
	}
}
