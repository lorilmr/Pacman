using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float MoveSpeed;
    //public Transform pathParent;

    private Vector3[] allPoints;
    private int currentPoint;
    private SelfAnimation animator;
    private int childPathIdx;
    private SpriteRenderer model;
    private Vector3 BornPosition;

    private int BuffSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        BornPosition = transform.localPosition;
        animator = GetComponent<SelfAnimation>();
        model = GetComponent<SpriteRenderer>();
        InitOnePath();
        //allPoints = new Vector3[pathParent.childCount];
        //for (int i = 0; i < allPoints.Length; i++) {
        //    allPoints[i] = pathParent.GetChild(i).position;
        //}
        //currentPoint = 0;
    }
    private void InitOnePath() {
        allPoints = PathManager.Instance.GetPath(out childPathIdx);
        allPoints[0].x = transform.position.x;
        currentPoint = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isStartGame) {
            transform.position = Vector3.MoveTowards(transform.position, allPoints[currentPoint],Time.deltaTime*MoveSpeed*BuffSpeed) ;
            if (transform.position == allPoints[currentPoint]) {
                Vector3 prev = allPoints[currentPoint];
                currentPoint++;
                if (currentPoint >= allPoints.Length) {
                    Debug.Log("当前路径巡逻结束");
                    PathManager.Instance.BackPath(childPathIdx);
                    InitOnePath();
                }
                Vector3 next = allPoints[currentPoint];
                CalculateDrection(prev,next);
            } 
        }
  
    }
    private void CalculateDrection(Vector3 prev, Vector3 next) {
        float xx = next.x - prev.x;
        float yy= next.y - prev.y;
        if (Mathf.Abs(xx)>Mathf.Abs(yy)) {
            animator.ChangeDirection(xx > 0 ? SelfAnimation.Anim_Direction.Right : SelfAnimation.Anim_Direction.Left);
        }
        else
        {
            animator.ChangeDirection(yy > 0 ? SelfAnimation.Anim_Direction.Up : SelfAnimation.Anim_Direction.Down);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Pacman"))
        {
            if (BuffSpeed == 1) {
                GameManager.Instance.GameOver(false);
            }
            else
            {
                transform.localPosition= BornPosition;
                PathManager.Instance.BackPath(childPathIdx);
                InitOnePath();
            }

        }
    }
    public void DebuffAdded() {
        Color color = model.color;
        color.a = 0.4f;
        model.color = color;
        BuffSpeed = 0;
    }
    public void DebuffRemove()
    {
        Color color = model.color;
        color.a = 1f;
        model.color = color;
        BuffSpeed = 1;
    }
}
