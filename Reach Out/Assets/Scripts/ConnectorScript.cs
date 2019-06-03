using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectorScript : MonoBehaviour, IPooledObject
{
    LineRenderer lr;
    GameObject player;
    GameObject unit;
    Color playerColor;
    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerColor = player.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(unit != null)
        {
            lr.startColor = playerColor;
            lr.SetPosition(0, player.transform.position);
            lr.SetPosition(1, unit.transform.position);
        }
    }

    public void OnObjectSpawn()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerColor = player.GetComponent<Renderer>().material.color;
    }

    public void SetConnection(UnitAI ai)
    {
        unit = ai.gameObject;
        lr.startColor = playerColor;
        lr.endColor = unit.GetComponent<Renderer>().material.color;
        lr.SetPosition(0, player.transform.position);
        lr.SetPosition(1, unit.transform.position);
    }
}
