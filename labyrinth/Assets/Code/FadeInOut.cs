using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeInOut : MonoBehaviour
{
    [SerializeField] internal Image whiteFade;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    internal void BecomeDark()
    {
        whiteFade.CrossFadeAlpha(1, 0.5f, false);
    }

    internal void BecomeTransparent()
    {
        whiteFade.CrossFadeAlpha(0, 0.8f, false);
    }
}
