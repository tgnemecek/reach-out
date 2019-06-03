using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScrolling : MonoBehaviour
{
    [SerializeField]
    private Transform creditsEnd;
    private Vector3 initialPosition;
    private bool displaying = false;
    private float scrollSpeed = 40f;
    private CanvasGroup creditScreen;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        creditScreen = gameObject.GetComponentInParent<CanvasGroup>();
        creditScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (displaying)
        {
            transform.Translate(0, scrollSpeed * Time.deltaTime, 0);
            if(transform.position.y > creditsEnd.position.y)
            {
                Hide();
            }
            if (Input.GetMouseButtonDown(0))
            {
                Hide();
            }
        }
    }

    public void Display()
    {
        creditScreen.gameObject.SetActive(true);
        displaying = true;
    }

    public void Hide()
    {
        creditScreen.gameObject.SetActive(false);
        displaying = false;
        gameObject.transform.position = initialPosition;
    }

    public bool IsDisplaying()
    {
        return displaying;
    }
}
