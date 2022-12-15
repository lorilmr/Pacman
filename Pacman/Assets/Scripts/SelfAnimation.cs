using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAnimation : MonoBehaviour {
	public enum Anim_Direction {
		None=-1,
		Right=0,
		Left,
		Up,
		Down,
	}
	public Sprite[] SpritesArr;
	public float AnimSpeed;
	public int TotalFrame;

	private SpriteRenderer SpriteRender;
	private int CurrentFrame=0;
	private int StartFrame;
	private int EndFrame;
	private Anim_Direction currentDirection=Anim_Direction.None;
	// Use this for initialization
	void Start () {
		SpriteRender = GetComponent<SpriteRenderer>();
		ChangeDirection(Anim_Direction.Right);
		StartCoroutine(PlayAnimation());
	}
	IEnumerator PlayAnimation() {
		while (true) {
			yield return new WaitForSeconds(AnimSpeed);
			SpriteRender.sprite = SpritesArr[CurrentFrame++];
			if (CurrentFrame>=EndFrame) {
				CurrentFrame = StartFrame;
			}
		}
		
	}
	public void ChangeDirection(Anim_Direction anim_Direction) {
        if (currentDirection == anim_Direction)
        {
            return;
        }
        else
        {
            currentDirection = anim_Direction;
            CurrentFrame = StartFrame = (int)anim_Direction * TotalFrame;
			EndFrame = StartFrame + TotalFrame;
        }
    }
	// Update is called once per frame
	//void Update() {
	//	if (Input.GetKey(KeyCode.D))
	//	{
	//	ChangeDirection(SelfAnimation.Anim_Direction.Right);
	//	}
	//	else if (Input.GetKey(KeyCode.A))
	//	{
	//	ChangeDirection(SelfAnimation.Anim_Direction.Left);
	//	}
	//	else if (Input.GetKey(KeyCode.W))
	//	{
	//	ChangeDirection(SelfAnimation.Anim_Direction.Up);
	//	}
	//	else if (Input.GetKey(KeyCode.S))
	//	{
	//		ChangeDirection(SelfAnimation.Anim_Direction.Down);
	//	}
	//}
}
