using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CompanionAlocator : MonoBehaviour
{
    private static int positionIndex = 0;
    private static List<bool> ocupiedPosition = new List<bool>();
    private static List<string> companionList = new List<string>();
    private static List<GameObject> companions = new List<GameObject>();
    private static List<GameObject> connections = new List<GameObject>();
    Vector3 positionDelta;
    Quaternion orientation;
    private GameObject player;
    StudioEventEmitter connectSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        connectSound = GameObject.Find("AUDIO_Connect").GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Unit")
        {
            UnitAI ai = collision.gameObject.GetComponent<UnitAI>();
            if (!ai.IsCompanion())
            {
                foreach(string col in companionList)
                {
                    if(col == null)
                    {
                        continue;
                    }
                    if (col.CompareTo(ai.GetColor())==0)
                    {
                        collision.gameObject.transform.Translate(0, 0, 1.5f);
                        return;
                    }
                }
                //Debug.Log("Index: " + positionIndex.ToString() + " ; Count: " + ocupiedPosition.Count.ToString());
                for (int i= 0; i<ocupiedPosition.Count; i++)
                {
                    if (!ocupiedPosition[i])
                    {
                        positionIndex = i;
                        break;
                    }
                }
                Debug.Log("index: " + positionIndex + " list size: " + companionList.Count);
                positionDelta = Vector3.zero;
                if (positionIndex % 2 == 1)
                {
                    positionDelta.x -= ((int)(positionIndex / 2) + 1.5f);
                }
                else
                {
                    positionDelta.x += ((int)(positionIndex / 2) + 1.5f);
                }
                if (positionIndex % 4 < 2)
                {
                    positionDelta.z -= 0.8f;
                }
                MoraleTracker.Instance.IncreaseMorale(SystemConstants.CONECTION_BONUS);
                GameObject line = ObjectPooler.Instance.SpawnFromPool("line", Vector3.zero, Quaternion.identity);
                line.GetComponent<ConnectorScript>().SetConnection(ai);
                if (positionIndex >= ocupiedPosition.Count)
                {
                    ocupiedPosition.Add(true);
                    companionList.Add(ai.GetColor());
                    companions.Add(ai.gameObject);
                    connections.Add(line);
                }
                else
                {
                    ocupiedPosition[positionIndex] = true;
                    companionList[positionIndex] = ai.GetColor();
                    companions[positionIndex] = ai.gameObject;
                    connections[positionIndex] = line;
                }
                connectSound.Play();
                ai.setAsCompanion(positionDelta, transform.rotation, positionIndex);
                player.GetComponent<Renderer>().material.color = (player.GetComponent<Renderer>().material.color + ai.gameObject.GetComponent<Renderer>().material.color) / 2;
                positionIndex = ocupiedPosition.Count;
                //Debug.Log("Index: " + positionIndex.ToString() + " ; Count: " + ocupiedPosition.Count.ToString());
            }
        }
    }

    public static int CheckForCompanion(string tag)
    {
        if(tag == null)
        {
            return -1;
        }
        for(int i = 0; i<companionList.Count; i++)
        {
            if(companionList[i] == null)
            {
                continue;
            }
            if (companionList[i].CompareTo(tag) == 0)
            {
                return i;
            }
        }
        return -1;
    }

    public static void RemoveCompanion(int index)
    {
        if (index >= companions.Count)
        {
            return;
        }
        if(index < 0)
        {
            return;
        }
        Debug.Log("removed at: " + index);
        ocupiedPosition[index] = false;
        companionList[index] = null;
        GameObject noLongerCompanion = companions[index];
        companions[index] = null;
        GameObject noConnection = connections[index];
        connections[index] = null;

        Destroy(noLongerCompanion.GetComponent<BulletSpawner>());
        Destroy(noLongerCompanion.GetComponent<CompanionAlocator>());
        ObjectPooler.Instance.StoreInPool("unit", noLongerCompanion);
        ObjectPooler.Instance.StoreInPool("line", noConnection);
    }
}
