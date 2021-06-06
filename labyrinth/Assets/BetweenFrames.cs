using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFrames : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeInOutScript;
    // Start is called before the first frame update
    internal bool finishedFading = false;
    void Start()
    {
        fadeInOutScript.whiteFade.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fadeInOutScript.whiteFade.canvasRenderer.GetAlpha());
        if (fadeInOutScript.whiteFade.canvasRenderer.GetAlpha() == 1 && finishedFading == false)
        {
            fadeInOutScript.FadeOut();
            finishedFading = true;
        }

        if (finishedFading == true && fadeInOutScript.whiteFade.canvasRenderer.GetAlpha() == 0)
        {
            finishedFading = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            fadeInOutScript.FadeIn();
        }   
    }
}
