using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePersistent : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
