using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Data : MonoBehaviour
{
    public TextMesh Best, Play;
    private int Short_time,Play_time;

    void Start()
    {
        /*
         요약 : 데이터 두개를 로드해서 텍스트에 기입
         */
        Play_time = PlayerPrefs.GetInt("Play_time");
        Short_time = PlayerPrefs.GetInt("Short_time");
        Best.text = "BEST        : " + Short_time.ToString();
        Play.text = "Play Time  : " + Play_time.ToString();
    }
}
