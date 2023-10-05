using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �̵��� �����ϴ� ��ũ��Ʈ
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //������ ���� ǥ��(���α׷� ��ü���� ������ �����ϵ���)
    //���߱� ���� ����� ����� ����
    public static string gameState = "Playing";

    //�̵� �ⴱ
    Rigidbody2D rbody;
    float axisH = 0.0f;        //�Է�
    public float speed = 3.0f; //�̵��ӵ�

    //���� ���
    public float jump = 8.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;  

    // �ִϸ��̼� ���
    Animator animator;
    public string[] animations; //�ִϸ��̼� �̸�
    // 0 : Player_Idle
    // 1 : Player_Jump
    // 2 : Player_Move
    // 3 : Player_Clear
    // 4 : Player_Over
    string cur_Anime = "";      //������ �ִϸ��̼�
    string pre_Anime = "";      //������ �ִϸ��̼�

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


        axisH = Input.GetAxisRaw("Horizontal"); //���� �̵�

        //���� ����
        //1. sprite renderer�� Flip ���
        //2. Scale�� Ư���� �̿�

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector3(-1, 1);
        }

        // ���� ��� �߰�
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
        // ����ĳ��Ʈ : ������ �� ���� �����ϴ� ������ ���� ���� ������Ʈ�� �����ϴ����� ���θ� üũ�ϴ� �޼ҵ�
        //transform.up * 0.1f�� ĳ���� �� ������ ���� ǥ������ �������. (Pivot)
        if(onGround || axisH != 0)
        {
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);

        }

        //������ ������ �õ�������
        if(onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }

        //������ ���ִ� ����
        if(onGround)
        {
            //Idle����
            if(axisH == 0)
            {
                cur_Anime = animations[0];
            }
            //Move����
            else
            {
                cur_Anime = animations[2];
            }
        }
        //Jump����
        else
        {
            cur_Anime = animations[1];
        }

        //�ִϸ��̼� ó��
        if(cur_Anime != pre_Anime) //�ִϸ��̼��� �ٲ� ���
        {
            pre_Anime = cur_Anime;
            animator.Play(cur_Anime);
        }
    }

    //Ʈ���� �浹�� �߻����� �� ȣ���� �Լ�
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
        //�浹��Ȱ��ȭ
        GetComponent<CapsuleCollider2D>().enabled = false;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    //������ ���ߴ� ���
    void GameStop()
    {
        rbody.GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
    }
}
