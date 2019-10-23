using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public string sender;
	public GameObject target;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(sender == "Player" && other.name.Contains("NPC Log") && target != null)
		{
			other.GetComponent<LogMovement>().setTarget(target);
			other.GetComponent<LogMovement>().hit(10);
			Destroy(gameObject);
		}
		else if(sender == "Log" && other.name.Contains("Player") && target != null)
		{
			other.GetComponent<PlayerMovement>().hit(10);
			Destroy(gameObject);
		}
		else if(sender == "Log" && other.name.Contains("NPC Log") && target != other.gameObject && target != null)
		{
			other.GetComponent<LogMovement>().setTarget(target);
			other.GetComponent<LogMovement>().hit(5);
			Destroy(gameObject);
		}
	}
}
