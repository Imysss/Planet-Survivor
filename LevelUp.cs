using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    public Choice[] choices;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        choices = GetComponentsInChildren<Choice>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    //������ ��ũ��Ʈ�� ���� Ȱ��ȭ �Լ� �ۼ�
    void Next()
    {
        //1. ��� ������ ��Ȱ��ȭ
        for(int i=0; i<choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
            if (choices[i].level == 5)
            {
                while(true)
                {
                    int ran = Random.Range(0, choices.Length);
                    if(choices[ran].level!=5)
                    {
                        Debug.Log("��ü");
                        choices[i] = choices[ran];
                        break;
                    }
                }
            }
        }

        //2. �� �߿��� �����ϰ� 3�� �����۸� Ȱ��ȭ
        int[] rand = new int[3];
        while (true)
        {
            rand[0] = Random.Range(0, choices.Length);
            rand[1] = Random.Range(0, choices.Length);
            rand[2] = Random.Range(0, choices.Length);

            if (rand[0] != rand[1] && rand[0] != rand[2] && rand[1] != rand[2])
            {
                break;
            }
        }

        for(int idx=0; idx<rand.Length; idx++)
        {
            Choice randChoice = choices[rand[idx]];
            randChoice.gameObject.SetActive(true);
        }

    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int idx)
    {
        choices[idx].OnClick();
    }
}
