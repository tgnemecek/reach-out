using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAlocator : MonoBehaviour
{
    private static int positionIndex = 0;
    private static List<bool> ocupiedPosition = new List<bool>();
    private static List<string> companionList = new List<string>();
    Vector3 positionDelta;
    Quaternion orientation;
	public GameObject AUDIO_Connect;
    // Start is called before the first frame update
    void Start()
    {

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
                Debug.Log(companionList.Count);
                foreach(string col in companionList)
                {
                    if (col.CompareTo(ai.GetColor())==0)
                    {
						collision.gameObject.transform.Translate(0, 0, 1.5f);
                        return;
                    }
				}
				AUDIO_Connect.GetComponent<FMODUnity.StudioEventEmitter>().Play();
                //Debug.Log("Index: " + positionIndex.ToString() + " ; Count: " + ocupiedPosition.Count.ToString());
                for (int i= ocupiedPosition.Count-1; i>=0; i--)
                {
                    if (!ocupiedPosition[i])
                    {
                        positionIndex = i;
                    }
                }

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
                MoraleTracker.Instance.IncreaseMorale(50);
                if (positionIndex >= ocupiedPosition.Count)
                {
                    ocupiedPosition.Add(true);
                }
                else
                {
                    ocupiedPosition[positionIndex] = true;
                }
                companionList.Add(ai.GetColor());
                ai.setAsCompanion(positionDelta, transform.rotation, positionIndex);
                ++positionIndex;
                //Debug.Log("Index: " + positionIndex.ToString() + " ; Count: " + ocupiedPosition.Count.ToString());
            }
        }
    }


}
