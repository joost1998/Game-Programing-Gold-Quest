using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	//Trigger if collision's touch;
	void OnTriggerEnter2D(Collider2D other)
	{
		//If other is a player
		if(other.gameObject.name.Contains("Player"))
		{
			//Loop though every Enemy;
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Log"))
			{
				//Change state to do nothing but sleep;
				obj.GetComponent<Log>().Sitting();
			}
			//Destroy the apple object;
			Destroy(gameObject);
		}
	}
}