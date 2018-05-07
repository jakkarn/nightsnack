using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 2f;

    private new Rigidbody2D rigidbody;

	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(moveX, moveY).normalized * movementSpeed;
        rigidbody.velocity = move;
	}
}
