using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rig;
    public GameObject playerCamera;
    public Animator animator;
    public GameObject enemySpawnZone;
    private float horizontalSpeed;
    private float verticalSpeed;
    private bool attacking;
    private bool dash;
    private float dashCooldown = .2f;
    private SpriteRenderer playerSprite;
    private bool died = false;
    private float diedCooldown = .3f;


    // Start is called before the first frame update
    void Start() {
        Physics2D.gravity = Vector2.zero;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {

        //playerCamera.transform.position = new Vector3(rig.position.x, rig.position.y, -10);

        if (Input.GetKeyDown(KeyCode.W) && verticalSpeed < (1 * speed)) {
            verticalSpeed += 1 * speed;
        } if (Input.GetKeyDown(KeyCode.A) && horizontalSpeed > (-1 * speed)) {
            horizontalSpeed += -1 * speed;
        } if (Input.GetKeyDown(KeyCode.S) && verticalSpeed > (-1 * speed)) {
            verticalSpeed += -1 * speed;
        } if (Input.GetKeyDown(KeyCode.D) && horizontalSpeed < (1 * speed)) {
            horizontalSpeed += 1 * speed;
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            verticalSpeed -= 1 * speed;
        } if (Input.GetKeyUp(KeyCode.A)) {
            horizontalSpeed -= -1 * speed;
        } if (Input.GetKeyUp(KeyCode.S)) {
            verticalSpeed -= -1 * speed;
        } if (Input.GetKeyUp(KeyCode.D)) {
            horizontalSpeed -= 1 * speed;
        }

        if (Input.GetMouseButtonDown(0)) {
            attacking = true;
        } if (Input.GetMouseButtonUp(0)) {
            attacking = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            dash = true;
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("HorizontalSpeed", horizontalSpeed);
        animator.SetFloat("VerticalSpeed", verticalSpeed);
        animator.SetBool("Attacking", attacking);
        rig.velocity = new Vector2(horizontalSpeed, verticalSpeed);

        if (horizontalSpeed < 0) {
            playerSprite.flipX = true;
        } else if (horizontalSpeed > 0) {
            playerSprite.flipX = false;
        }

        if (dash) {
            rig.velocity = new Vector2(horizontalSpeed * 3, verticalSpeed * 3);
            dashCooldown -= Time.deltaTime;
        }

        if (dashCooldown < 0) {
            dash = false;
            dashCooldown = .2f;
        }

        if (died) {
            diedCooldown -= Time.deltaTime;
        }

        if (diedCooldown < 0) {
            enemySpawnZone.GetComponent<EnemySpawnScript>().Restart();
            died = false;
            diedCooldown = .3f;
            gameObject.transform.position = new Vector2(0f, 0f);
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void Die() {
        died = true;
        playerSprite.color = new Color(1f, 0.5f, 0.5f, 1f);
    }
}
