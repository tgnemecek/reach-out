using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementSpeed = SystemConstants.PLAYER_SPEED;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMov = Input.GetAxis("Horizontal") * movementSpeed;
        //float yMov = Input.GetAxis("Vertical") * movementSpeed;
        //rb.velocity = new Vector3(xMov, 0, yMov);
        rb.velocity = new Vector3(xMov, 0, 0);
        if (rb.position.x > SystemConstants.SCREEN_RIGHT)
        {
            Vector3 newPostion = rb.position;
            newPostion.x = 6;
            rb.position = newPostion;
        }
        else if (rb.position.x < SystemConstants.SCREEN_LEFT)
        {
            Vector3 newPostion = rb.position;
            newPostion.x = -6;
            rb.position = newPostion;
        }
        if (rb.position.z > SystemConstants.SCREEN_TOP)
        {
            Vector3 newPostion = rb.position;
            newPostion.z = 4.6f;
            rb.position = newPostion;
        }
        else if(rb.position.z < SystemConstants.SCREEN_BOTTOM)
        {
            Vector3 newPostion = rb.position;
            newPostion.z = -4.6f;
            rb.position = newPostion;
        }
    }
}
