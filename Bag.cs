using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public GameObject[] bag;
    public int bagCnt;
    public int bagUseCnt;
    Image icon;
    Image lockIcon;
    Button exitBtn;

    private void Start()
    {
        for(int i=0; i<bagCnt; i++)
        {
            UnlockBag(i);
        }
    }

    public void UnlockBag(int bagIdx)
    {
        Image[] objs = bag[bagIdx].GetComponentsInChildren<Image>(true);
        icon = objs[1];
        lockIcon = objs[2];
        exitBtn = bag[bagIdx].GetComponentInChildren<Button>(true);

        icon.gameObject.SetActive(true);
        lockIcon.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(true);
    }

    public void AddBag(int bagIdx, Sprite sprite)
    {
        Debug.Log("Add Bag");
        Image[] objs = bag[bagIdx].GetComponentsInChildren<Image>(true);
        icon = objs[1];
        icon.sprite = sprite;
        
        bagUseCnt++;
    }
}
