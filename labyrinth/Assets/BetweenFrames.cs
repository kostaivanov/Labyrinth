using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenFrames : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeInOutScript;

    private TeleportController teleportController;
    private GameObject player;
    // Start is called before the first frame update
    internal bool finishedFading = false;
    void Start()
    {
        fadeInOutScript.whiteFade.canvasRenderer.SetAlpha(0.0f);
        teleportController = new TeleportController();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeInOutScript.whiteFade.canvasRenderer.GetAlpha() == 1 && finishedFading == false)
        {
            if (player != null)
            {
                teleportController.Teleport(player, this.gameObject);
            }

            StartCoroutine(WaitUntilFadeOut());
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
            player = otherObject.gameObject;
            fadeInOutScript.whiteFade.enabled = true;
            fadeInOutScript.FadeIn();
        }   
    }

    private IEnumerator WaitUntilFadeOut()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        fadeInOutScript.FadeOut();
        
    }
}
