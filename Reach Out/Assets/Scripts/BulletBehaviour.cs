using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour, IPooledObject
{
    private float bulletSpeed = 10f;
    private Rigidbody rb;
	  private GameObject AUDIO_Hit;
    ObjectPooler instance = ObjectPooler.Instance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
		    AUDIO_Hit = GameObject.Find("AUDIO_Hit");

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
            UnitAI ai = collision.gameObject.GetComponent<UnitAI>();
            if (ai.IsCompanion() || ai.gameObject.transform.position.z < SystemConstants.SCREEN_BOTTOM + 1)
            {
                instance.StoreInPool("bullet", gameObject);
                return;
            }
            CompanionAlocator.RemoveCompanion(CompanionAlocator.CheckForCompanion(ai.GetColor()));
            MoraleTracker.Instance.DecreaseMorale(SystemConstants.DESTRUCTION_PENALTY);
            instance.StoreInPool("unit", collision.gameObject);
            instance.StoreInPool("bullet", gameObject);
            AUDIO_Hit.GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }

        if(collision.gameObject.tag == "Wall")
        {
            instance.StoreInPool("bullet", this.gameObject);
        }
    }
}
