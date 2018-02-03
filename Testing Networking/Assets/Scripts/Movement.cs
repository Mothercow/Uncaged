using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class Movement : NetworkBehaviour 
{
	public float speed;
	public Boundary boundary;
	float moveHorizontal, moveVertical;

	void Update()
	{
		if (!isLocalPlayer) 
		{
			Destroy (this);
			return;
		}
	}

	void FixedUpdate () 
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		if (moveVertical == 0)
		{
			moveHorizontal = Input.GetAxis ("Horizontal");
		}

		if (moveHorizontal == 0)
		{
			moveVertical = Input.GetAxis ("Vertical");
		}

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		rb.velocity = movement * speed;
			
		rb.position = new Vector2 
		(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
		);
	}
}
