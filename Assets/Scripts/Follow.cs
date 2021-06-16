using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Follow : MonoBehaviour
{
    public GameObject Chicken, Lion;
    private RectTransform RectTransform;
    private bool isplay = false;
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();    
    }
    void Update()
    {
        //닭과 사자의 거리 차이를 소수점 1자리까지만 구하기 위함
        Debug.Log(string.Format("{0:N1}",(Chicken.transform.position.z - Lion.transform.position.z)/10));
        float len = float.Parse((string.Format("{0:N1}", (Chicken.transform.position.z - Lion.transform.position.z) / 10)));
        RectTransform.anchoredPosition = new Vector2(len-5f,0);//닭의 raw_image가 -5에 위치했기때문에 -5f

        //거리차이가 2보다 적을 때 무서운 음악으로 변경
        if ((Chicken.transform.position.z - Lion.transform.position.z) / 10 <= 2 && !isplay)
        {

            Chicken.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
            isplay = true;
        }
        //2보다 크면 기본 배경음으로 변경
        else if((Chicken.transform.position.z - Lion.transform.position.z) / 10 >= 2 && isplay)
        {
            GetComponent<AudioSource>().Stop();
            Chicken.GetComponent<PlayerController>().Audio1.Play();
            isplay = false;
        }
        //10보다 크면 데이터를 저장하고, 씬변경
        if ((Chicken.transform.position.z - Lion.transform.position.z)/ 10 >= 10)
        {
            Chicken.GetComponent<Save_Data>().Save_win();
            SceneManager.LoadScene("Ending_win");
        }

    }
}
