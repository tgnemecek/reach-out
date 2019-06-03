using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu_Functions : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter audioEmitter;
  	FMOD.Studio.PARAMETER_DESCRIPTION musicVolume;
    [SerializeField]
    MenuScrolling menu;
    [SerializeField]
    Toggle easy;
    [SerializeField]
    Toggle medium;
    [SerializeField]
    Toggle hard;
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(640, 480, false);
        audioEmitter = GameObject.Find("AUDIO_Music").GetComponent<FMODUnity.StudioEventEmitter>();
        FMOD.Studio.EventDescription eventDesc = FMODUnity.RuntimeManager.GetEventDescription(audioEmitter.Event);
        eventDesc.getParameterDescriptionByName("Volume", out musicVolume);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        Debug.Log("start Game");
        if (!menu.IsDisplaying())
        {
            if (easy.isOn)
            {
                SystemConstants.WINCONDITION = 3;
            }
            else if (medium.isOn)
            {
                SystemConstants.WINCONDITION = 4;
            }
            else if (hard.isOn)
            {
                SystemConstants.WINCONDITION = 5;
            }
            audioEmitter.EventInstance.setParameterByID(musicVolume.id, 0.7f);
            SceneManager.LoadScene(1);
        }
    }

    public void DisplayCredit()
    {
        Debug.Log("Display Credits");
        if (!menu.IsDisplaying())
        {
            menu.Display();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
