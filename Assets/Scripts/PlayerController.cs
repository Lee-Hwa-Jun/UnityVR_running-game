using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public GameObject Camera,Lion,Title;
    public TextMesh Time_text;

    public float jumpForce = 500;//점프 거리
    public float timeBeforeNextJump = 1.2f;//다음 점프를 위한 시간 텀
    public float Speed = 10.0f;//닭의 기본속도
    public float LR_Speed = 10.0f;//좌우 이동속도
    public float Play_time = 0f;//나중에 저장 될 시간
    public bool Start_game=false;//터치 전 움직이기 제한
    public bool Pause = true;//터치 후 3초 뒤 출발하기 위한 제한

    private float Change_speed_time = 2f;//돌이나 하트 충돌 시 속도 유지 시간
    private bool Change_Speed = false;//속도 변경 체크
    private float posX = 0.0f;//좌우 이동을 위한 변수
    private float CountDown_Time = 3.2f;

    public AudioSource Audio1, Audio2;
    public AudioClip CountDown, Playing, Jump,Get_heart;//4가지 오디오
    Animator anim;
    Rigidbody rb;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetInteger("Walk", 1);//걷는 애니메이션 시작
    }

    void Update()
    {
        if (!Start_game)//터치 전
        {
            if (Input.GetMouseButtonDown(0))//클릭 시
            {
                Start_game = true;
                Audio1.clip = CountDown;
                Audio1.Play();
                Title.SetActive(false);
            }
        }
        if (Pause && Start_game)//터치 후 3초 카운트 다운
        {
            CountDown_Time -= Time.deltaTime;
            if (CountDown_Time < 0)
            {
                Pause = false;
                Audio1.clip = Playing;
                Audio1.Play();
            }
        }
        else if (!Pause && Start_game)//게임 진행 중
        {
            Time_text.text = "Time : "+((int)Play_time).ToString();//플레이 시간
            Play_time += Time.deltaTime;//플레이 시간

            this.transform.position += this.transform.forward * Speed * Time.deltaTime;//전진

            if (Change_Speed)//속도 변경 시
            {
                Change_speed_time -= Time.deltaTime;
                if (Change_speed_time < 0)//속도 변경 시간 끝날 시 원상복구
                {
                    Speed = 10f;
                    Change_Speed = true;
                    Change_speed_time = 2f;
                }
            }



            if (Input.GetMouseButtonDown(0) && this.transform.position.y <= 0)//전진 중 터치 시 & 땅에 닿아있을 때 점프
            {
                Audio2.clip = Jump;
                Audio2.Play();
                rb.AddForce(0, jumpForce, 0);
                anim.SetTrigger("jump");
            }

            //콜라이더 대신 스크립트 벽
            if (this.transform.position.x < -7)
            {
                this.transform.position = new Vector3(-6.99f, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x > 7)
            {
                this.transform.position = new Vector3(6.99f, this.transform.position.y, this.transform.position.z);
            }
            //좌우 이동 제어
            else
            {
                posX = this.transform.position.x - Camera.transform.rotation.z;//카메라의 로테이션값에 대한 치킨 x값
                this.transform.position = new Vector3(posX, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //사자와 충돌 시 게임 일시정지 및 속도 0
        if(collision.transform.tag == "Lion")
        {
            Pause = true;
            Speed = 0;
        }
        //돌과 충돌 시 속도 5
        if (collision.transform.tag == "rock")
        {
            Speed = 5f;
            Change_Speed = true;
        }
        try
        {
            //하트와 충돌 시 속도 15
            if (collision.transform.tag == "Heart")
            {
                Audio2.clip = Get_heart;
                Audio2.Play();
                Speed = 15f;
                Change_Speed = true;
                Change_speed_time = 2f;
                Destroy(collision.gameObject);
            }
        }
        catch(Exception ex)
        {
        }
    }
}