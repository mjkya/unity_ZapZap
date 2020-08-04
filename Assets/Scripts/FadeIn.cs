using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : FadeAnimation
{
    protected override void Tick()
    {
        if (time < fadeTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, time / fadeTime);
        }
        else
        {
            time = 0;
            isStart = false;
            //gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }

}
