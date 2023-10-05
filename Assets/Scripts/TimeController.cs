using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ �ð��� ���õ� ī��Ʈ �ٿ��� ó���ϱ� ���� ��Ʈ�ѷ�
/// </summary>
public class TimeController : MonoBehaviour
{
    public bool isCountDown = true; //true�� ��� �ð��� ����
    public float gameTime = 0;      //���ӿ� �־��� �ִ� �ð�
    public bool isTimeOver = false; //true�� ��� Ÿ�̸� ����
    public float displayTime = 0;   //ȭ�鿡 ǥ�õǴ� �ð�
    float times = 0;                //������ �ð�

    private void Start()
    {
        if(isCountDown)
        {
            displayTime = gameTime;
        }
    }

    private void Update()
    {
        if(isTimeOver == false)
        {
            times += Time.deltaTime;
            if(isCountDown)
            {
                displayTime = gameTime - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0;
                    isTimeOver = true;
                }
               
            }
            else
            {
                displayTime = times;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
        }
    }
}
