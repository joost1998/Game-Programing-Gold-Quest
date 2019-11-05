using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private GameObject target;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other != null)
		{
			if (target.name == "Player" && other.name.Contains("NPC Log") && target != null)
			{
				other.GetComponent<Log>().SetTarget(target);
				other.GetComponent<Log>().Hit(10);
				other.GetComponent<Log>().Attacking();
				Destroy(gameObject);
			}
			else if (target.name.Contains("Log") && other.name.Contains("Player") && target != null)
			{
				other.GetComponent<PlayerMovement>().hit(10);
				Destroy(gameObject);
			}
			else if (target.name.Contains("Log") && other.name.Contains("NPC Log") && target != other.gameObject && target != null)
			{
				other.GetComponent<Log>().SetTarget(target);
				other.GetComponent<Log>().Hit(5);
				other.GetComponent<Log>().Attacking();
				Destroy(gameObject);
			}
		}
	}

	public void SetTarget(GameObject target)
	{
		this.target = target;
	}
}
