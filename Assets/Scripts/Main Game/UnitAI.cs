using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitAI : MonoBehaviour, IPooledObject
{
    private static Color[] colors = {Color.blue, Color.cyan, Color.green, Color.red, Color.yellow};
    private static string[] tags = { "blue", "cyan", "green", "red", "yellow" };
    private bool isCompanion = false;
    private string currentTag;
    private float speed = SystemConstants.UNIT_SPEED;
    private Rigidbody rb;
    [SerializeField]
    private Transform spawner;
    [SerializeField]
    private GameObject exclamation;
    private Vector3 CompanionPlace;
    private Quaternion CompanionOrientation;
    private Transform playerPosition;
    private int CompanionIndex;
    [SerializeField]
    private SpriteRenderer unitImage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        OnObjectSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCompanion)
        {
            rb.velocity = transform.forward * speed;
            exclamation.transform.Translate(-transform.forward * speed * Time.deltaTime);
            if (rb.position.z> -4.5 && exclamation.activeSelf)
            {
                exclamation.SetActive(false);
            }
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
        unitImage.color = colors[aux];
        currentTag = tags[aux];
        gameObject.transform.rotation = Quaternion.identity;
        exclamation.SetActive(true);
        Vector3 parentPos = transform.position;
        parentPos.z += 3;
        parentPos.y += 2.5f;
        exclamation.transform.position = parentPos;
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
