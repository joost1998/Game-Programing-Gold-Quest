using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

	private int health;

	public GameObject crosshair;

	public GameObject player;
	public GameObject shootImage;

	public IList<GameObject> ItemList;

    void Start()
    {
		//Set the variables 
		animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
		ItemList = new List<GameObject>();
		Cursor.visible = false;
		health = 100;
	}

    void Update()
    {
		//Function to move character
        UpdateAnimationAndMove();
		//Function to move shooting images to the cursor position
		Aim();
		//Function to shoot
		Shoot();
    }

    void UpdateAnimationAndMove()
    {
		//Get data to see wich direction is moving
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if (change != Vector3.zero)
        {
			//Move position of character
            MoveCharacter();
			//Set the paramaters in the bled tree to use the right animations
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

	void Aim()
	{
		//Move the shoot image to cursor position
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pz.z = 0;
		crosshair.transform.position = pz;
	}

	void Shoot()
	{
		//Get the cursor position
		Vector2 shootingDirection = crosshair.transform.localPosition;
		shootingDirection.Normalize();

		//Check if the left mouse is pressed
		if(Input.GetMouseButtonDown(0))
		{
			//Calculates the direction and speed of the bullet and shoot
			GameObject bullet = Instantiate(shootImage, transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().SetTarget(gameObject);
			bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * 6.0f;
			bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
			//Destroy's the bullet after a few seconds
			Destroy(bullet, 2.0f);
		}
	}

	//Remove the amount of dame from health
	public void hit(int damage)
	{
		health -= damage;
		checkDestroy();
	}

	//Check if a player has no health, game over
	public void checkDestroy()
	{
		if (health <= 0)
		{
			Debug.Log("Player dead");
			gameObject.SetActive(false);
		}
	}

	//Add a apple
	public void addAppleItem(GameObject apple)
	{
		ItemList.Add(apple);
		//Player is a god for 5 seconds
		Invoke("clearItemList", 5);
	}

	//Clear the ItemList
	private void clearItemList()
	{
		ItemList.Clear();
	}

	//Simple return true,false
	public bool hasApple()
	{
		return ItemList.Count >= 1;
	}
}
