using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitAI : MonoBehaviour
{
    private Color[] colors = {Color.blue, Color.clear, Color.cyan, Color.green, Color.red};
    private bool isCompanion = false;
    private float speed = 6f;
    private Rigidbody rb;
    private Transform spawner;
    private Transform CompanionPlace;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCompanion)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.position = Vector3.Lerp(rb.position, CompanionPlace.position, 0.2f);
            if(Vector3.Distance(rb.position, CompanionPlace.position) < 0.01 && gameObject.GetComponent<BulletSpawner>() == null)
            {
                BulletSpawner bs = gameObject.AddComponent<BulletSpawner>();
                bs.setSpawning(0.3f, spawner);
            }
        }

    }

    public void setAsCompanion()
    {

    }
}
