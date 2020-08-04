using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAnimation : MonoBehaviour
{
    public SpriteRenderer[] cuts;
    public float btwTime;
    public float startTime;
    public bool turnOff;

    private bool isStart;
    private int cur;
    private float time;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(cuts.Length);
        isStart = false;
        cur = 0;
        time = 0;
        for (int i=0; i < cuts.Length; i++)
        {
            cuts[i].color = new Color(1, 1, 1, 0);
        }
        StartCoroutine(WaitForTick());
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            Tick();
        }
    }

    IEnumerator WaitForTick()
    {
        yield return new WaitForSeconds(startTime);
        isStart = true;
    }

    void Tick()
    {
        if (cur >= cuts.Length)
        {
            if (turnOff)
            {
                for (int i = 0; i < cuts.Length; i++)
                {
                    cuts[i].gameObject.SetActive(false);
                }
                isStart = false;
                gameObject.SetActive(false);
            }
            else
            {
                isStart = false;
            }

           
            
        }
        else
        {
            if (time < btwTime)
            {
                cuts[cur].color = new Color(1, 1, 1, 1);
            }
            else
            {
                time = 0;
                cur++;
            }
            time += Time.deltaTime;
        }
        
    }


}
