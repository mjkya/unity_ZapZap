using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine.Utility;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public GameObject[] Stages;
    public GameObject RestartBtn;
    public int numberToChange;

    [Header("Characters")]
    public Movement rat;
    public Movement cat;
    [Header("Cameras")]
    public CinemachineVirtualCamera ratCam;
    public CinemachineVirtualCamera catCam;
    public CinemachineVirtualCamera deadCam;
    public CinemachineBrain mainCameraBrain;

    private int itemCount;
    private int stageIndex;
    private ICinemachineCamera currentCamera;
    public Text Score;
    public float timeCount;

    private void Awake()
    {
        itemCount = 0;
        stageIndex = 0;
        Score = GameObject.Find("Score").GetComponent<Text>();
        currentCamera = mainCameraBrain.ActiveVirtualCamera;
        CinemachineTargetGroup targetGroup = deadCam.Follow.gameObject.GetComponent<CinemachineTargetGroup>();
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            targetGroup.m_Targets[i].weight = 1;
        }
        catCam.VirtualCameraGameObject.SetActive(false);
        deadCam.VirtualCameraGameObject.SetActive(false);
        ratCam.VirtualCameraGameObject.SetActive(true);
    }

    void Update()
    {
        timeCount += Time.deltaTime;
        Score.text = Mathf.Round(timeCount).ToString();
        Debug.Log(timeCount);
        if (itemCount == numberToChange)
        {
            rat.Turn();
            rat.SpeedUp();
            cat.Turn();
            cat.SpeedUp();
            if (rat.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                rat.Giant(2f);
            }
            else
            {
                rat.Giant(1f);
            }

            itemCount = 0;
            int tempLayer = rat.gameObject.layer;
            rat.gameObject.layer = cat.gameObject.layer;
            cat.gameObject.layer = tempLayer;

            NextStage();
            CameraSwitch();
        }
    }

    void CameraSwitch()
    {
        currentCamera = mainCameraBrain.ActiveVirtualCamera;

        if (currentCamera as CinemachineVirtualCamera == ratCam)
        {
            currentCamera.VirtualCameraGameObject.SetActive(false);
            catCam.VirtualCameraGameObject.SetActive(true);
        }
        else if (currentCamera as CinemachineVirtualCamera == catCam)
        {
            currentCamera.VirtualCameraGameObject.SetActive(false);

            ratCam.VirtualCameraGameObject.SetActive(true);
        }
    }
    void DeadCamSwitch(int alive)
    {
        currentCamera = mainCameraBrain.ActiveVirtualCamera;
        currentCamera.VirtualCameraGameObject.SetActive(false);
        CinemachineTargetGroup targetGroup = deadCam.Follow.gameObject.GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets[alive].weight = 0;
        deadCam.VirtualCameraGameObject.SetActive(true);

    }
    public void Die(Movement dead)
    {
        if (dead == rat)
        {
            DeadCamSwitch(0);
        }
        else if (dead == cat)
        {
            DeadCamSwitch(1);
        }
        else
        {
            Debug.Log("something err");
        }
        Time.timeScale = 0;
        RestartBtn.SetActive(true);
    }

    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex = Random.Range(0, Stages.Length);
            Stages[stageIndex].SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    public void CountUp()
    {
        itemCount++;
        timeCount += 10;
    }
}