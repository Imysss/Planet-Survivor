using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        //WorldToScreenPoint: ���� ���� ������Ʈ ��ġ�� ��ũ�� ��ǥ�� ��ȯ
        Vector3 playerVec = GameManager.instance.player.transform.position;
        rect.position = new Vector3(playerVec.x, playerVec.y + 2, playerVec.z); 
    }
}
