using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitAI : MonoBehaviour, IPooledObject
{
    private static Color[] colors = {Color.blue, Color.clear, Color.cyan, Color.green, Color.red, Color.yellow};
    private static string[] tags = { "blue", "clear", "cyan", "green", "red", "yellow" };
    private bool isCompanion = false;
    private string currentTag;
    private float speed = 6f;
    private Rigidbody rb;
    [SerializeField]
    private Transform spawner;
    private Vector3 CompanionPlace;
    private Quaternion CompanionOrientation;
    private Transform playerPosition;
    private int CompanionIndex;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
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
            rb.position = Vector3.Lerp(rb.position, playerPosition.position + CompanionPlace, 0.2f);
            rb.rotation = Quaternion.Lerp(rb.rotation, CompanionOrientation, 0.2f);
            //Debug.Log("Test: "+ Vector3.Distance(rb.position, playerPosition.position + CompanionPlace).ToString());
            if(Vector3.Distance(rb.position, playerPosition.position + CompanionPlace) < 0.5 && gameObject.GetComponent<BulletSpawner>() == null)
            {
                BulletSpawner bs = gameObject.AddComponent<BulletSpawner>();
                bs.setSpawning(0.3f, spawner);
                gameObject.AddComponent<CompanionAlocator>();
            }
        }

    }

    public void setAsCompanion(Vector3 postion, Quaternion orientation, int index)
    {
        CompanionPlace = postion;
        CompanionOrientation = orientation;
        isCompanion = true;
        CompanionIndex = index;
    }

    public void OnObjectSpawn()
    {
        int aux = Random.Range(0, colors.Length);
        gameObject.GetComponent<Renderer>().material.color = colors[aux];
        currentTag = tags[aux];
        gameObject.transform.rotation = Quaternion.identity;
        isCompanion = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ObjectPooler.Instance.StoreInPool("unit", gameObject);
        }
    }

    public bool IsCompanion()
    {
        return isCompanion;
    }

    public string GetColor()
    {
        return currentTag;
    }

    public int GetCompanionIndex()
    {
        return CompanionIndex;
    }
}
