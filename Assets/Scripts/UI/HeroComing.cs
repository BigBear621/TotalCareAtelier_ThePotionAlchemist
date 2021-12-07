using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroComing : Singleton<HeroComing>
{
    public Slider slider;
    // ���� �ִ� �Ÿ� (�پ��� �þ�� �Ÿ�)
    public float comingDistance;
    float comingTime;
    int leftTime;
    // �� �Ÿ�
    float distance;
    public TextMeshProUGUI currentDistance;

    private void Start()
    {
        comingDistance = 0;
        // 10��(600��)
        distance = 600;
        comingTime = 0;
    }

    public void Update()
    {
        // 1�ʿ� 1/600 �Ÿ��� �̵� (comingDistance �þ)
        comingTime += Time.deltaTime * 2;
        comingDistance += Time.deltaTime * 2;
        leftTime = (int)(distance - comingDistance);
        slider.value = comingDistance / distance;

        // text
        currentDistance.text = "������ ���� �Ÿ� : " + leftTime.ToString() + "km";
        
        // �Ÿ��� ���� gameOver ���� �Ǻ�
        if (comingTime > distance) GameManager.instance.isGameOver = true;
        else GameManager.instance.isGameOver = false;
    }
}
