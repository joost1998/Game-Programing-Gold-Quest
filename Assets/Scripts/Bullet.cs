using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private GameObject target;

	void OnTriggerEnter2D(Collider2D other)
	{
		try { 
			//True if the player hits a Log (Enemy)
			if (target.name.Contains("Player") && other.name.Contains("NPC Log") && !other.isTrigger)
			{
				other.GetComponent<Log>().SetTarget(target);
				other.GetComponent<Log>().Attacking();
				other.GetComponent<Log>().Hit(10);
				Destroy(gameObject);
			}
			//True if a Log hits the Player
			else if (target.name.Contains("Log") && other.name.Contains("Player"))
			{
				other.GetComponent<PlayerMovement>().hit(10);
				Destroy(gameObject);
			}
			//True if a Log hits an other Log
			else if (target.name.Contains("Log") && other.name.Contains("NPC Log") && target != other.gameObject && !other.isTrigger)
			{
				other.GetComponent<Log>().SetTarget(target);
				other.GetComponent<Log>().Attacking();
				other.GetComponent<Log>().Hit(5);
				Destroy(gameObject);
			}
		} catch(MissingReferenceException e) {}
	}

	//Set the target, in this case the sender of the bullet
	public void SetTarget(GameObject target)
	{
		this.target = target;
	}
}
