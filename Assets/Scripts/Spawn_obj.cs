using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_obj : MonoBehaviour
{
    public GameObject[] Spawn_points = new GameObject[18];//18개의 스폰포인트
    public GameObject Rock,Heart;
    public List<int> Random_choice = new List<int>();//포지션 중복방지를 위한 리스트
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(5,10);//5~10사이의 랜덤값
        for(int j = 0; j < i; j++)
        {
            while(true){//돌
                int k = Random.Range(0, 18);//포지션 랜덤값
                if (!Random_choice.Contains(k))//중복이 안된다면 생성
                {
                    GameObject tmp = Instantiate(Rock, Spawn_points[k].transform.position, Quaternion.identity);
                    tmp.transform.parent = this.transform;
                    Random_choice.Add(k);
                    break;
                }
            }
        }
        while (true)//하트
        {
            int k = Random.Range(0, 18);//포지션 랜덤값
            if (!Random_choice.Contains(k))//중복이 안된다면 생성
            {
                //공중에 떠있게 y값 조정
                GameObject tmp = Instantiate(Heart, new Vector3(Spawn_points[k].transform.position.x, 
                    Spawn_points[k].transform.position.y+0.5f, Spawn_points[k].transform.position.z), Quaternion.identity);
                tmp.transform.parent = this.transform;
                Random_choice.Add(k);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
