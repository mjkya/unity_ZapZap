using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float fadeTime = 3f;

    [Header("Start after")]
    public float startTime;
    protected float time;
    protected SpriteRenderer spriteRenderer;
    protected bool isStart;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForTick());
        isStart = false;
    }

    protected virtual void Tick()
    {
     
    }

    IEnumerator WaitForTick()
    {
        yield return new WaitForSeconds(startTime);
        isStart = true;
    }

    protected void Update()
    {
        if (isStart)
        {
            Tick();
        }
    }

}
