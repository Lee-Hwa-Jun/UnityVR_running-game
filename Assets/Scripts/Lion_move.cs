using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lion_move : MonoBehaviour
{
    public GameObject Chicken,Dead;
    private float Speed = 10.2f;//속도
    private float CountDown_Time = 3f;

    Animator anim;
    Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetInteger("Walk", 1);//걷는 애니메이션 활성화
    }

    void Update()
    {   
        //게임이 시작되면
        if(Chicken.GetComponent<PlayerController>().Start_game && !Chicken.GetComponent<PlayerController>().Pause)
        {
            //전진 및 닭의 x,y포지션 팔로우
            this.transform.position += this.transform.forward * Speed * Time.deltaTime;
            this.transform.position = new Vector3(Chicken.transform.position.x, Chicken.transform.position.y, this.transform.position.z);
            if (Speed == 0)//닭과 닿으면 3초대기 후 데이터저장 및 씬 변경
            {
                CountDown_Time -= Time.deltaTime;
                if (CountDown_Time < 0)
                {
                    Chicken.GetComponent<Save_Data>().Save_lose();
                    SceneManager.LoadScene("Ending_lose");
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            //닭과 닿으면 죽음 메세지 활성화 및 속도 0
            CountDown_Time = 3f;
            Chicken.GetComponent<PlayerController>().Audio1.Stop();
            GetComponent<AudioSource>().Play();
            Dead.SetActive(true);
            Speed = 0f;
        }    
    }
}
