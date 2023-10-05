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

    // 시간 제한 관련 기능 추가
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeController;

    private void Start()
    {
        timeController = GetComponent<TimeController>();
        if (timeController != null)
        {
            //시간 제한이 없는 경우 숨겨줍니다.
            if(timeController.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }
        //함수 이름, 시간 넣고 그 시간 이후 함수를 실행하게 하는 Invoke
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
    }

    //게임 클리어 / 게임 오버에 대한 처리
    private void Update()
    {
        if(PlayerController.gameState == "GameClear")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            //RESTART 버튼 비활성화
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
            //NEXT 버튼 비활성화
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
