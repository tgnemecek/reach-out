using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoraleTracker : MonoBehaviour
{
    [SerializeField]
    private Text morale_display;
    private int moraleValue = 500;

    #region Singleton

    public static MoraleTracker Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        morale_display.text = moraleValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseMorale(int val)
    {
        moraleValue += val;
        morale_display.text = moraleValue.ToString();
    }

    public void DecreaseMorale(int val)
    {
        moraleValue -= val;
        if(moraleValue <= 0)
        {
            Debug.Log("You Lose!");
        }
        morale_display.text = moraleValue.ToString();
    }
}
