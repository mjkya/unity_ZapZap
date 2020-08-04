using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float respawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hide()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
        Invoke("Show", respawnTime);
    }

    void Show()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}