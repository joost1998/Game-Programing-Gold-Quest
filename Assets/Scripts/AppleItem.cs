using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleItem : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name.Contains("Player"))
		{
			other.GetComponent<PlayerMovement>().addAppleItem(gameObject);
			Destroy(gameObject);
		}
	}
}