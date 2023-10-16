using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    //1. ��������� ������ ����
    public GameObject[] prefabs;

    //2. Ǯ ����� �ϴ� ����Ʈ�� (1�� 2�� 1��1 ����)
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int idx = 0; idx < pools.Length; idx++)
        {
            pools[idx] = new List<GameObject>();
        }
    }

    //pool�� �ִ� object�� ��ȯ�ϴ� �Լ�
    public GameObject Get(int idx)
    {
        GameObject select = null;

        foreach (GameObject item in pools[idx])
        {
            //������ pool�� ��Ȱ��ȭ�� ���� ������Ʈ ����
            if (!item.activeSelf)
            {
                //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //��Ȱ��ȭ�� ���� ������Ʈ�� ���ٸ�
        if (!select)
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            //Instantiate: ���� ������Ʈ�� �����Ͽ� ��鿡 �����ϴ� �Լ� (Ǯ �Ŵ��� �ȿ��ٰ� ������Ʈ�� �־ �����ϰڴٴ� �ǹ�)
            select = Instantiate(prefabs[idx], transform);
            //pool���ٰ� instance ���
            pools[idx].Add(select);
        }
        return select;
    }
}
