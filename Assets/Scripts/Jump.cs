using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    public float jumpForce = 500;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    private bool k = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         요약 : 한번 점프 후 대기 
         Ending_win, Ending_lose 씬의 닭과 사자오브젝트
         */
        while (k)
        {
            canJump -= Time.deltaTime;
            if (canJump <= 0)
            {
                canJump = timeBeforeNextJump;
                rb.AddForce(0, jumpForce, 0);
                anim.SetTrigger("jump");
            }
            k = false;
        }

    }
}
