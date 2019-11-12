using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name.Contains("Player"))
		{
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Log"))
			{
				obj.GetComponent<Log>().Sitting();
			}
			Destroy(gameObject);
		}
	}
}