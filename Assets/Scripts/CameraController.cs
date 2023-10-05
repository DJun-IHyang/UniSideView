using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //스크롤 제한
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    //서브 스크린 추가
    public GameObject subScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            //카메라의 좌표를 갱신
            float x = player.transform.position.x; //플레이어 추적
            float y = player.transform.position.y; //플레이어 추적
            float z = transform.position.z; //카메라의 z축을 따라가기 위함

            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }
            if (y > topLimit)
            {
                y = topLimit;
            }
            else if (y < bottomLimit)
            {
                y = bottomLimit;
            }

            //카메라 위치에 대한 저장
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

            //서브 스크린에 대한 스크롤 작업
            if (subScreen != null)
            {
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x * 0.5f, y, z);
                //카메라 x 수치 절반만큼 이동, y,z 는 서브스크린을 따라간다
                // --> 기존 배경과 어긋나게 스크롤됨
                subScreen.transform.position = v;
            }
        }
    }
}
