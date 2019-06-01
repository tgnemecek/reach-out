using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private float fireRate = 1f;
    private ObjectPooler instancer;
    private float counter;
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
        if (counter >= fireRate)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-6f, 6f);
            counter = 0f;
            instancer.SpawnFromPool("unit", spawnPosition, transform.rotation);
        }
    }
}
