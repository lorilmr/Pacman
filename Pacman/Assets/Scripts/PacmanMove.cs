using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour {
	public float MoveSpeed;

	private Rigidbody2D rigidbody2D;
	private SelfAnimation animator;
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<SelfAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.isStartGame) {
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 destination = rigidbody2D.position + Vector2.right * MoveSpeed;
                rigidbody2D.MovePosition(destination);
                animator.ChangeDirection(SelfAnimation.Anim_Direction.Right);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Vector2 destination = rigidbody2D.position + Vector2.left * MoveSpeed;
                rigidbody2D.MovePosition(destination);
                animator.ChangeDirection(SelfAnimation.Anim_Direction.Left);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                Vector2 destination = rigidbody2D.position + Vector2.up * MoveSpeed;
                rigidbody2D.MovePosition(destination);
                animator.ChangeDirection(SelfAnimation.Anim_Direction.Up);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Vector2 destination = rigidbody2D.position + Vector2.down * MoveSpeed;
                rigidbody2D.MovePosition(destination);
                animator.ChangeDirection(SelfAnimation.Anim_Direction.Down);
            }
        }
        
    }
}
