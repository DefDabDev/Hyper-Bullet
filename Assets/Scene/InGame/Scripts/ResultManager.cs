using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text scoreTxt;

    float time = 0;
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    public void GameEnd()
    {
        scoreTxt.text = (int)time + "";
    }

    public void ReStart()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        Blind.instance.ReplaceScene("Main");
    }
}
