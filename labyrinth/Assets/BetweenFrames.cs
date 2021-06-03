using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFrames : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeInOutScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            fadeInOutScript.whiteFade.canvasRenderer.SetAlpha(0.0f);

            Debug.Log("asdsad");
            fadeInOutScript.FadeIn();
            //fadeInOutScript.FadeOut();
        }   
    }
}
