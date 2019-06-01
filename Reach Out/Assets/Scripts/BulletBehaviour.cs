using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour, IPooledObject
{
    private float bulletSpeed = 10f;
    private Rigidbody rb;
    ObjectPooler instance = ObjectPooler.Instance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Unit")
        {
            if (collision.gameObject.GetComponent<UnitAI>().IsCompanion())
            {
                return;
            }
            MoraleTracker.Instance.DecreaseMorale(10);
            instance.StoreInPool("unit", collision.gameObject);
            instance.StoreInPool("bullet", gameObject);
        }

        if(collision.gameObject.tag == "Wall")
        {
            instance.StoreInPool("bullet", this.gameObject);
        }
    }
}
