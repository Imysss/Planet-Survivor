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
        //CircleCastAll: ������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ�
        //(ĳ���� ���� ��ġ, ���� ������, ĳ���� ����, ĳ���� ����, ��� ���̾�)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
    }
}
