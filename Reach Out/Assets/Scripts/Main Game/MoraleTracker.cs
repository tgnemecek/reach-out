using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoraleTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI morale_display;
    private int moraleValue = SystemConstants.INITIAL_POINTS;

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
        morale_display.text = "Atitude: " + moraleValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseMorale(int val)
    {
        moraleValue += val;
        morale_display.text = "Atitude: " + moraleValue.ToString();
    }

    public void DecreaseMorale(int val)
    {
        moraleValue -= val;
        if(moraleValue <= 0)
        {
            CompanionAlocator.ClearAlocator();
            SceneManager.LoadScene(4);
        }
        morale_display.text = "Atitude: " + moraleValue.ToString();
    }
}
