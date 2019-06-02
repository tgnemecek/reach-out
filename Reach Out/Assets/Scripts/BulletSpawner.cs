using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private float fireRate = 0.3f;
    [SerializeField]
    private Transform spawnPoint;
    private ObjectPooler instancer;
    private float counter;
	public GameObject AUDIO_Gun;
    // Start is called before the first frame update
    void Start()
    {
        instancer = ObjectPooler.Instance;
        counter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        counter = counter + Time.deltaTime;
        if(counter >= fireRate)
        {
            counter = 0f;
            instancer.SpawnFromPool("bullet", spawnPoint.transform.position, transform.rotation);
			AUDIO_Gun.GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }
    }

    public void setSpawning(float fireRate, Transform spawnPoint)
    {
        this.fireRate = fireRate;
        this.spawnPoint = spawnPoint;
    }
}
