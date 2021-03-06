using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMenu : MonoBehaviour
{
    public GameObject fulfillment;

    float timeSpan = 0f;
    float[] fillTime = { 0.5f, 1.0f, 1.5f, 2.0f, 2.5f };
    float[] fillSize = { 0.02f, 0.04f, 0.06f, 0.08f, 0.10f };

    public void PutWaterCheck(string streamName)
    {
        Debug.Log("???? ?????ð? :" + timeSpan);
        timeSpan += Time.deltaTime;

        for (int i = 0; i < fillTime.Length; i++)
        {
            if (timeSpan > fillTime[i])
            {
                fulfillment.transform.localScale = new Vector3(0.35f, fillSize[i], 0.35f);

                if (timeSpan >= fillTime[fillTime.Length - 1])
                {
                    Debug.Log(streamName);
                    if (streamName.Contains("StartStream"))
                        GameManager.instance.GameStartEvent();
                    else if (streamName.Contains("EndStream"))
                    {
                        Debug.Log(GameManager.instance);
                        GameManager.instance.GameQuitEvent();
                    }
                    timeSpan = 0;
                    Destroy(gameObject);
                }
            }
        }
    }
}
