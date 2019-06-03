using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroll : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    private float yoffset = 0;
    private int textureHeight;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        textureHeight = rend.material.mainTexture.height;
    }

    // Update is called once per frame
    void Update()
    {
        yoffset -= speed * Time.deltaTime;
        Vector2 offSet = new Vector2(0, yoffset);
        rend.material.mainTextureOffset = offSet;
        if (-yoffset >= textureHeight)
        {
            yoffset = 0;
        }
    }
}
