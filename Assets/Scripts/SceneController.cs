using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int nextScene;
    public float transitionTime;
    public bool infinite;
    private float time;

    void Start()
    {
        time = 0;
    }

    void Update()
    {
        if (!infinite)
        {
            if (transitionTime <= time)
            {
                SceneManager.LoadScene(nextScene);
            }
            time += Time.deltaTime;
        }
        
    }

    public void Skip()
    {
        SceneManager.LoadScene(nextScene);
    }
}
