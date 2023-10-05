using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform pointA;
    public Transform pointB;
    public float duration;
    float currentTime;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (enemy.transform.position.x == pointA.position.x)
        {
            enemy.transform.position = Vector2.Lerp(pointA.position, pointB.position, currentTime / duration);
            enemy.transform.localScale = new Vector3(1, 1);


        }
        else if (enemy.transform.position.x == pointB.position.x)
        {
            enemy.transform.position = Vector2.Lerp(pointB.position, pointA.position, currentTime / duration);
            enemy.transform.localScale = new Vector3(-1, 1);

        }


    }
}
