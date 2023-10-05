using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSprite;
    public Sprite gameClearSprite;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    Image titleImage;

    // �ð� ���� ���� ��� �߰�
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeController;

    private void Start()
    {
        timeController = GetComponent<TimeController>();
        if (timeController != null)
        {
            //�ð� ������ ���� ��� �����ݴϴ�.
            if(timeController.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }
        //�Լ� �̸�, �ð� �ְ� �� �ð� ���� �Լ��� �����ϰ� �ϴ� Invoke
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
    }

    //���� Ŭ���� / ���� ������ ���� ó��
    private void Update()
    {
        if(PlayerController.gameState == "GameClear")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            //RESTART ��ư ��Ȱ��ȭ
            restartButton.GetComponent<Button>().interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSprite;
            PlayerController.gameState = "GameClear";

            if(timeController != null)
            {
                timeController.isTimeOver = true;
            }
        }
        else if(PlayerController.gameState == "GameOver")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            //NEXT ��ư ��Ȱ��ȭ
            nextButton.GetComponent<Button>().interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            PlayerController.gameState = "GameOver";

            if (timeController != null)
            {
                timeController.isTimeOver = true;
            }
        }
        else if(PlayerController.gameState == "Playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerController = player.GetComponent<PlayerController>();

            if(timeController != null)
            {
                if(timeController.gameTime > 0.0f)
                {
                    int time = (int)timeController.displayTime;

                    timeText.GetComponent<Text>().text = $"{time}";

                    if(time == 0)
                    {
                        playerController.GameOver();
                    }
                }
            }
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
