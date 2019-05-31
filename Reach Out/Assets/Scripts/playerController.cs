using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    private int movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var xMov = Input.GetAxis("Horizontal") * movementSpeed;
        var yMov = Input.GetAxis("Vertical") * movementSpeed;
        rb.velocity = new Vector3(xMov, 0, yMov);
    }
}
