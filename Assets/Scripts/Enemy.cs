using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   

    public GameObject target;
    public float speed = 0f;
    private Rigidbody2D rig;
    private Animator animator;
    private SpriteRenderer rend;
    private bool died = false;
    
    public void Start() {
        rig = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate() {

        if(died == true) {
            AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
            if (currentAnimation.IsName("die") && currentAnimation.normalizedTime > 0.9) {
                Destroy(gameObject);
            }
        } else {
            FollowPlayer();
        }
    }
    public void FollowPlayer() {
        if (target != null && speed != 0f && died == false) {

            float horizontalSpeed = 0f;
            float verticalSpeed = 0f;

            if (target.transform.position.x > gameObject.transform.position.x + 0.1f) {
                horizontalSpeed += 1 * speed;
            } else if (target.transform.position.x < gameObject.transform.position.x - 0.1f) {
                horizontalSpeed -= 1 * speed;
            }

            if (target.transform.position.y > gameObject.transform.position.y + 0.1f) {
                verticalSpeed += 1 * speed;
            } else if (target.transform.position.y < gameObject.transform.position.y - 0.1f) {
                verticalSpeed -= 1 * speed;
            }

            rig.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            animator.SetFloat("HorizontalSpeed", horizontalSpeed);
            animator.SetFloat("VerticalSpeed", verticalSpeed);

            if (horizontalSpeed < 0) {
                rend.flipX = true;
            } else if (horizontalSpeed > 0) {
                rend.flipX = false;
            }
        }
    }

    public void Die()
    {   
        rend.color = new Color(1f, 0.5f, 0.5f, 1f);
        BoxCollider2D childCollider = gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<BoxCollider2D>();
        childCollider.isTrigger = true;
        animator.SetBool("Died", true);
        rig.velocity = new Vector2(0f, 0f);
        died = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (died == false) {
            if (other.name == "PlayerObject") 
            {
                PlayerScript player = other.GetComponent<PlayerScript>();
                if (player != null) {
                    player.Die();
                }
            
                // Destroy(gameObject);

            }
        }
        
    }

}
