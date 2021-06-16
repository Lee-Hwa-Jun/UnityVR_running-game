using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_spawn : MonoBehaviour
{
    public GameObject Map,Chicken;
    
    private List<GameObject> Map_list = new List<GameObject>();//맵을 리스트로 관리하여 Add, Remove를 이용
    private int posZ = 1;//맵 생성 위치 계산

    void Update()
    {
        if(Map_list.Count <= 4)//초반 시작 시 맵 4개를 생성하기 위함
        {
            //기본 맵 z포지션을 기준으로 Map의 길이 만큼 더한 값의 포지션에 생성
            Map_list.Add(Instantiate(Map, this.transform.position + new Vector3(0, 0, 63.5f*posZ), this.transform.rotation));
            //posZ = 63.5f => 127f => 190.5f ...
            posZ += 1;
        }
        
        if (Map_list.Count > 2)
        {
            //맵이 2개 이상일때 닭의 위치에 비해 먼 맵이 있으면 그 맵은 삭제
            if (Chicken.transform.position.z >= Map_list[1].transform.position.z+64.5f)
            {
                Destroy(Map_list[0].gameObject);
                Map_list.RemoveAt(0);//리스트 제거
            }
        }
    }
}


