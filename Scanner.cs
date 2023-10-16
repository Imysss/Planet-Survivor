using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scanner : MonoBehaviour
{
    public float scanRange;

    public LayerMask targetLayer;
    public RaycastHit2D[] targets;

    private void FixedUpdate()
    {
        //CircleCastAll: 원형의 캐스트를 쏘고 모든 결과를 반환하는 함수
        //(캐스팅 시작 위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
    }
}
