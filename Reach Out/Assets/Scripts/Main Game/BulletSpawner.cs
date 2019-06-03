using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BulletSpawner : MonoBehaviour
{
    private float fireRate = 0.3f;
    [SerializeField]
    private Transform spawnPoint;
    private ObjectPooler instancer;
    private float counter;
    private StudioEventEmitter gunShot;
    // Start is called before the first frame update
    void Start()
    {
        instancer = ObjectPooler.Instance;
        counter = 0;
        gunShot = GameObject.Find("AUDIO_Gun").GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        counter = counter + Time.deltaTime;
        if(counter >= fireRate)
        {
            counter = 0f;
            instancer.SpawnFromPool("bullet", spawnPoint.transform.position, transform.rotation);
            gunShot.Play();
        }
    }

    public void setSpawning(float fireRate, Transform spawnPoint)
    {
        this.fireRate = fireRate;
        this.spawnPoint = spawnPoint;
    }
}
