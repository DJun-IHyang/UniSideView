using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //��ũ�� ����
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    //���� ��ũ�� �߰�
    public GameObject subScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            //ī�޶��� ��ǥ�� ����
            float x = player.transform.position.x; //�÷��̾� ����
            float y = player.transform.position.y; //�÷��̾� ����
            float z = transform.position.z; //ī�޶��� z���� ���󰡱� ����

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

            //ī�޶� ��ġ�� ���� ����
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

            //���� ��ũ���� ���� ��ũ�� �۾�
            if (subScreen != null)
            {
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                Vector3 v = new Vector3(x * 0.5f, y, z);
                //ī�޶� x ��ġ ���ݸ�ŭ �̵�, y,z �� ���꽺ũ���� ���󰣴�
                // --> ���� ���� ��߳��� ��ũ�ѵ�
                subScreen.transform.position = v;
            }
        }
    }
}
