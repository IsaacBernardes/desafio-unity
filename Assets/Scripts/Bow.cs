using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{

    public Transform firePoint;
    public GameObject arrowObject;

    // Update is called once per frame
    void Update()
    {

        Animator animator = gameObject.GetComponent(typeof(Animator)) as Animator;
        float horizontalSpeed = animator.GetFloat("HorizontalSpeed");
        float verticalSpeed = animator.GetFloat("VerticalSpeed");
        
        if (horizontalSpeed > 0) { // RIGHT
            firePoint.position = gameObject.transform.position;
            firePoint.position += new Vector3(0.1f, -0.05f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 270);
        } else if (horizontalSpeed < 0) { // LEFT
            firePoint.position = gameObject.transform.position;
            firePoint.position -= new Vector3(0.1f, +0.05f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 90);
        } else if (verticalSpeed > 0) { // TOP
            firePoint.position = gameObject.transform.position;
            firePoint.position += new Vector3(0, 0.12f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 0);
        } else if (verticalSpeed < 0) { // BOTTOM
            firePoint.position = gameObject.transform.position;
            firePoint.position -= new Vector3(0, 0.15f, 0);
            firePoint.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (Input.GetMouseButtonUp(0)) {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(arrowObject, firePoint.position, firePoint.rotation);
    }
}
