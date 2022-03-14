using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rig;
    public GameObject playerCamera;
    private float horizontalSpeed;
    private float verticalSpeed;


    // Start is called before the first frame update
    void Start() {
        Physics2D.gravity = Vector2.zero;
    }

    // Update is called once per frame
    void Update() {

        playerCamera.transform.position = new Vector3(rig.position.x, rig.position.y, -10);

        if (Input.GetKeyDown(KeyCode.W)) {
            verticalSpeed += 1 * speed;
        } if (Input.GetKeyDown(KeyCode.A)) {
            horizontalSpeed += -1 * speed;
        } if (Input.GetKeyDown(KeyCode.S)) {
            verticalSpeed += -1 * speed;
        } if (Input.GetKeyDown(KeyCode.D)) {
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
    }

    void FixedUpdate()
    {
        // anim.setFloat("HorizontalSpeed", horizontalSpeed);
        // anim.setFloat("VerticalSpeed", verticalSpeed);
        rig.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }
}
