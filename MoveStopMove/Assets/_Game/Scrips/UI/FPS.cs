using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpsText;

    private float time = 0; 

    void Update()
    {
        // test
        time += Time.deltaTime;
        if (time > 2)
        {
            float fps = 1 / Time.deltaTime;
            fpsText.text = fps.ToString();
            time = 0;
        }
    }
}
