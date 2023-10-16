using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScanner : MonoBehaviour
{
    public float scanRange;

    public LayerMask targetLayer;
    public RaycastHit2D[] targets;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        foreach (RaycastHit2D target in targets)
        {
            target.transform.gameObject.GetComponent<Stardust>().Get();
        }
    }
}
