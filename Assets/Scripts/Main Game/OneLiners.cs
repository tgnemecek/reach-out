using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class OneLiners : MonoBehaviour
{
    private FMOD.Studio.EventInstance audioEventInstance;
    private float coodownTime = 4f;
    private float count;
    private bool canSpeak = true;
    [System.Serializable]
    public class Liner
    {
        public string character;
        public string text;
        public Sprite Portrait;
        public string name;
        public bool hasVoice;
    }

    [System.Serializable]
    public class LinerCategory
    {
        public string category;
        public List<Liner> liners;
    }

    public List<LinerCategory> liners;

    #region Singleton

    public static OneLiners Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField]
    private GameObject oneLinerContainer;
    [SerializeField]
    private Image portraitContainer;
    [SerializeField]
    private GameObject oneLinerList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpeak)
        {
            count += Time.deltaTime;
            if (count > coodownTime)
            {
                canSpeak = true;
                count = 0;
            }
        }
    }

    public void GenerateLiner(string category)
    {
        if (canSpeak)
        {
            for (int i = 0; i < liners.Count; i++)
            {
                if (liners[i].category.CompareTo(category) == 0)
                {
                    Liner selected = liners[i].liners[Random.Range(0, liners[i].liners.Count)];
                    portraitContainer.sprite = selected.Portrait;
                    portraitContainer.color = Color.white;
                    oneLinerContainer.GetComponent<TextMeshProUGUI>().text = selected.character + ": " + selected.text;
                    oneLinerContainer.transform.parent = oneLinerList.transform;
                    if (selected.hasVoice)
                    {
                        audioEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Sound Effects/Speech/One Liners/" + selected.name);
                        audioEventInstance.start();
                    }
                    canSpeak = false;
                }
            }
        }
    }
}
