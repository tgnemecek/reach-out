using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStop : MonoBehaviour
{
    private GameObject AudioMenuManager;
    // Start is called before the first frame update
    void Start()
    {
      AudioMenuManager = GameObject.Find("AudioMenuManager");
      if (AudioMenuManager != null) {
        Destroy(AudioMenuManager);
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
