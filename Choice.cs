using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{
    public ChoiceData data;
    public int level;

    Image icon;
    TextMeshProUGUI textName;
    TextMeshProUGUI textDesc;
    TextMeshProUGUI textStardust;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textName = texts[0];
        textDesc = texts[1];
        textStardust = texts[2];
    }

    private void OnEnable()
    {
        switch (data.itemType)
        {
            case ChoiceData.ItemType.Bullet:
                icon.sprite = data.prefab.GetComponent<Bullet>().bulletSprtie[level];
                textName.text = string.Format("{0} ({1}/5)", data.itemName, level + 1);
                textDesc.text = data.itemDesc[level];
                textStardust.text = string.Format("x {0}", data.stardustNeed[level]);
                //Set btn interactable by bag cnt
                if (level == 0)
                {
                    if (GameManager.instance.bag.bagUseCnt >= GameManager.instance.bag.bagCnt)
                        GetComponent<Button>().enabled = false;
                    else
                        GetComponent<Button>().enabled = true;
                }    
                break;
            case ChoiceData.ItemType.Antenna:
            case ChoiceData.ItemType.Rocket:
                icon.sprite = data.sprite;
                textName.text = string.Format("{0} ({1}/5)", data.itemName, level + 1);
                textDesc.text = data.itemDesc[0];
                textStardust.text = string.Format("x {0}", data.stardustNeed[level]);
                break;
            case ChoiceData.ItemType.Bag:
                icon.sprite = data.sprite;
                textName.text = string.Format("{0} ", data.itemName);
                textDesc.text = data.itemDesc[0];
                textStardust.text = string.Format("x {0}", data.stardustNeed[level]);
                break;
            case ChoiceData.ItemType.Helmet:
            case ChoiceData.ItemType.HealthPotion:
            case ChoiceData.ItemType.StarPotion:
                icon.sprite = data.sprite;
                textName.text = string.Format("{0}", data.itemName);
                textDesc.text = data.itemDesc[level];
                textStardust.text = string.Format("x {0}", data.stardustNeed[level]);
                break;
        }

        if (GameManager.instance.stardust < data.stardustNeed[level])
            GetComponent<Button>().interactable = false;
        else
            GetComponent<Button>().interactable = true;
    }

    public void OnClick()
    {
        GameManager.instance.stardust -= data.stardustNeed[level];

        switch (data.itemType)
        {
            case ChoiceData.ItemType.Bullet:
                if (level == 0)
                {
                    if (GameManager.instance.bag.bagUseCnt >= GameManager.instance.bag.bagCnt)
                    {
                        Debug.Log("사용 불가");
                        break;
                    }
                    //activate bullet skill
                    GameManager.instance.bulletManager[data.itemId].gameObject.SetActive(true);

                    //add bag
                    GameManager.instance.bag.AddBag(GameManager.instance.bag.bagUseCnt, data.sprite);
                }
                else
                {
                    GameManager.instance.bulletManager[data.itemId].bulletLevel++;
                }
                level++;
                break;
            case ChoiceData.ItemType.Antenna: //Increase scanner range by 10%
                GameManager.instance.player.scanner.scanRange += (GameManager.instance.player.scanner.scanRange * 0.1f); 
                level++;
                break;
            case ChoiceData.ItemType.Bag: //Increase the number of available skills
                //unlock bag
                GameManager.instance.bag.UnlockBag(GameManager.instance.bag.bagCnt);
                GameManager.instance.bag.bagCnt++;
                level = GameManager.instance.bag.bagCnt;
                break;
            case ChoiceData.ItemType.Rocket: //Increase character speed by 10%
                GameManager.instance.player.speed += (GameManager.instance.player.speed * 0.1f);
                level++;
                break;
            case ChoiceData.ItemType.Helmet: //Invincible for 10 seconds
                //추후 추가 필요
                break;
            case ChoiceData.ItemType.HealthPotion: //Recovery 50% of max health
                GameManager.instance.player.health += (GameManager.instance.player.maxHealth * 0.5f);
                break;
            case ChoiceData.ItemType.StarPotion: //Add Stardust 25
                GameManager.instance.stardust += 25;
                if(GameManager.instance.stardust >= GameManager.instance.maxStardust)
                {
                    GameManager.instance.stardust=GameManager.instance.maxStardust;
                }
                break;
        }

        if (level == 5)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
