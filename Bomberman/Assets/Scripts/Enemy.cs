using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask RaycastMask = LayerMask.GetMask();
    private bool isInMovement = false;
    
    [SerializeField] private bool isHorizontal = true;
    private int _positiveDirection = 1;
    private Vector2 direction;
    
    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, RaycastMask);
        if(hit.collider != null)
            Debug.Log(hit.collider.gameObject.layer.ToString());
        return hit.collider != null;
    }

    private void Start()
    {
        direction = isHorizontal ? Vector2.right : Vector2.up;
    }

    private void Update()
    {
        if (!isInMovement)
        {
            MovePlayerTo(direction * _positiveDirection);
        }
    }

    private void MovePlayerTo(Vector2 dir)
    {
        if (Raycast(dir))
        {
            _positiveDirection = -_positiveDirection;
            return;
        }

        isInMovement = true;

        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 0.7f).OnComplete(() =>
        {
            isInMovement = false;
        });
    }
}
