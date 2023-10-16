using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    //1. 프리펩들을 저장할 변수
    public GameObject[] prefabs;

    //2. 풀 담당을 하는 리스트들 (1과 2는 1대1 관계)
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int idx = 0; idx < pools.Length; idx++)
        {
            pools[idx] = new List<GameObject>();
        }
    }

    //pool에 있는 object를 반환하는 함수
    public GameObject Get(int idx)
    {
        GameObject select = null;

        foreach (GameObject item in pools[idx])
        {
            //선택한 pool의 비활성화된 게임 오브젝트 접근
            if (!item.activeSelf)
            {
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //비활성화된 게임 오브젝트가 없다면
        if (!select)
        {
            //새롭게 생성하고 select 변수에 할당
            //Instantiate: 원본 오브젝트를 복제하여 장면에 생성하는 함수 (풀 매니저 안에다가 오브젝트를 넣어서 생성하겠다는 의미)
            select = Instantiate(prefabs[idx], transform);
            //pool에다가 instance 등록
            pools[idx].Add(select);
        }
        return select;
    }
}
