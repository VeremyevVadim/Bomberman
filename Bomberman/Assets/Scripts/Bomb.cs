using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    [FormerlySerializedAs("ExplosionMask")] [SerializeField] private LayerMask InteractableMask;
    [SerializeField] private LayerMask NonExplosionMask;
    
    [SerializeField] 
    [Range(0, 10)]
    private int _blowRadius = 2;
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOShakeRotation(2.5f, 40f, 15));
        sequence.Append(transform.DOScale(Vector3.one * 0.5f, 0.2f));
        sequence.OnComplete(Blow);
        
    }

    private RaycastHit2D Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, _blowRadius, InteractableMask);
        return hit;
    }
    private void Blow()
    {
        RaycastHit2D hitUp = Raycast(Vector2.up);
        if (hitUp.collider != null && ((1<<hitUp.collider.gameObject.layer) & NonExplosionMask) == 0)
        {
            Destroy(hitUp.collider.gameObject);
        }
        
        RaycastHit2D hitRight = Raycast(Vector2.right);
        if (hitRight.collider != null && ((1<<hitRight.collider.gameObject.layer) & NonExplosionMask) == 0)
        {
            Destroy(hitRight.collider.gameObject);
        }
        
        RaycastHit2D hitLeft = Raycast(Vector2.left);
        if (hitLeft.collider != null && ((1<<hitLeft.collider.gameObject.layer) & NonExplosionMask) == 0)
        {
            Destroy(hitLeft.collider.gameObject);
        }
        
        RaycastHit2D hitDown = Raycast(Vector2.down);
        if (hitDown.collider != null && ((1<<hitDown.collider.gameObject.layer) & NonExplosionMask) == 0)
        {
            Destroy(hitDown.collider.gameObject);
        }
        
        Destroy(gameObject);
    }
}
