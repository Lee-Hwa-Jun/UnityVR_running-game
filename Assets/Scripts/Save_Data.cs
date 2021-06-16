using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Data : MonoBehaviour
{
    public void Save_win()
    {
        //플레이 타임 저장
        int Play_time = (int)this.GetComponent<PlayerController>().Play_time;
        int Short_time;

        PlayerPrefs.SetInt("Play_time", Play_time);

        //만약 Short_time을 로드해서 지금의 Play_time이 보다 적으면 새로 갱신
        if (PlayerPrefs.GetInt("Short_time")!= 0)
        {
            Short_time = PlayerPrefs.GetInt("Short_time");
            if(Play_time < Short_time)
            {
                PlayerPrefs.SetInt("Short_time", Play_time);
            }
        }
        //Short_time이 0이면 게임을 처음 이긴 것이기 때문에 갱신
        else if (PlayerPrefs.GetInt("Short_time") == 0)
        {
            PlayerPrefs.SetInt("Short_time", Play_time);
        }

    }
    public void Save_lose()
    {
        //졌을때는 Short_time저장이 필요없음
        int Play_time = (int)this.GetComponent<PlayerController>().Play_time;

        PlayerPrefs.SetInt("Play_time", Play_time);
    }
}
