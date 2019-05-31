using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class message
{
    private int value;
    private int transformationCount;


    public void IncreaseCount()
    {
        ++transformationCount;
    }

    public void changeValue(int newValue)
    {
        value = newValue;
    }
}
