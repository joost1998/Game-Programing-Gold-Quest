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
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
		ItemList = new List<GameObject>();
		Cursor.visible = false;
	}

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
		Aim();
		Shoot();
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
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
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pz.z = 0;
		crosshair.transform.position = pz;
	}

	void Shoot()
	{
		Vector2 shootingDirection = crosshair.transform.localPosition;
		shootingDirection.Normalize();

		if(Input.GetMouseButtonDown(0))
		{
			GameObject bullet = Instantiate(shootImage, transform.position, Quaternion.identity);
			bullet.GetComponent<Bullet>().SetTarget(gameObject);
			bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * 6.0f;
			bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
			Destroy(bullet, 2.0f);
		}
	}

	public void hit(int damage)
	{
		health -= damage;
		checkDestroy();
	}

	public void checkDestroy()
	{
		if (health <= 0)
		{
			Debug.Log("Player dead");
		}
	}

	public void addAppleItem(GameObject apple)
	{
		ItemList.Add(apple);
		Invoke("clearItemList", 5);
	}

	private void clearItemList()
	{
		ItemList.Clear();
	}

	public bool hasApple()
	{
		return ItemList.Count >= 1;
	}
}
