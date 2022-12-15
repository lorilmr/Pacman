using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour {
    private bool isSuperDot = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Pacman")) {
            GameManager.Instance.EatenDot(isSuperDot);
            Destroy(gameObject);
        }
    }
    public void MakeToSuper() {
        isSuperDot = true;
        transform.localScale = new Vector3(3f,3f,3f);
    }
}
