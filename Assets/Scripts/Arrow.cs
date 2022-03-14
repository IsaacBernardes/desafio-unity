using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed = 1f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {   

        if (other.name == "Enemy(Clone)") 
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.Die();
            }
            Destroy(gameObject);

        } 
        else if (other.name == "PlayerObject") 
        {
            // Do Nothing...
        }
        else {
            Destroy(gameObject);
        }
    }

}
