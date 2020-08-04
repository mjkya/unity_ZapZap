using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : FadeAnimation
{
    
    protected override void Tick()
    {
        if (time < fadeTime)
        {
            spriteRenderer.color = new Color(1, 1, 1,1f - time / fadeTime);
        }
        else
        {
            time = 0;
            gameObject.SetActive(false);
            
        }
        time += Time.deltaTime;
    }

}
