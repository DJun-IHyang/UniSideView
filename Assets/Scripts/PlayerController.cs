using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 이동을 관리하는 스크립트
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //게임의 상태 표시(프로그램 전체에서 접근이 가능하도록)
    //멈추기 위한 명분을 만들기 위함
    public static string gameState = "Playing";

    //이동 기눙
    Rigidbody2D rbody;
    float axisH = 0.0f;        //입력
    public float speed = 3.0f; //이동속도

    //점프 기능
    public float jump = 8.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;  

    // 애니메이션 기능
    Animator animator;
    public string[] animations; //애니메이션 이름
    // 0 : Player_Idle
    // 1 : Player_Jump
    // 2 : Player_Move
    // 3 : Player_Clear
    // 4 : Player_Over
    string cur_Anime = "";      //현재의 애니메이션
    string pre_Anime = "";      //현재의 애니메이션

    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        cur_Anime = animations[0];
        pre_Anime = animations[0];
        gameState = "Playing";
    }


    void Update()
    {
        if(gameState != "Playing")
        {
            return;
        }


        axisH = Input.GetAxisRaw("Horizontal"); //수평 이동

        //방향 조절
        //1. sprite renderer의 Flip 기능
        //2. Scale의 특성을 이용

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector3(-1, 1);
        }

        // 점프 기능 추가
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        goJump = true;
    }

    private void FixedUpdate()
    {
        if (gameState != "Playing")
        {
            return;
        }

        onGround = Physics2D.Linecast(transform.position, (transform.position - transform.up * 0.1f), groundLayer);
        // 라인캐스트 : 지정한 두 점을 연결하는 가상의 선에 게임 오브젝트가 접속하는자의 여부를 체크하는 메소드
        //transform.up * 0.1f는 캐릭터 발 지점에 대한 표현으로 사용했음. (Pivot)
        if(onGround || axisH != 0)
        {
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        }

        //땅에서 점프를 시도했을때
        if(onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        //땅위에 서있는 상태
        if(onGround)
        {
            //Idle상태
            if(axisH == 0)
            {
                cur_Anime = animations[0];
            }
            //Move상태
            else
            {
                cur_Anime = animations[2];
            }
        }
        //Jump상태
        else
        {
            cur_Anime = animations[1];
        }

        //애니메이션 처리
        if(cur_Anime != pre_Anime) //애니메이션이 바뀐 경우
        {
            pre_Anime = cur_Anime;
            animator.Play(cur_Anime);
        }
    }

    //트리거 충돌이 발생했을 때 호출할 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if(collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    public void Goal()
    {
        animator.Play(animations[3]);
        gameState = "GameClear";
        GameStop();
    }
    public void GameOver()
    {
        animator.Play(animations[4]);
        gameState = "GameOver";
        GameStop();
        //충돌비활성화
        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    //게임을 멈추는 기능
    void GameStop()
    {
        rbody.GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
    }
}
