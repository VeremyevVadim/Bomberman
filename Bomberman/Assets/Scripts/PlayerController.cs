using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isInMovement = false;

    [SerializeField] private LayerMask RaycastMask = LayerMask.GetMask();
    [SerializeField] private LayerMask NoPlantZonetMask = LayerMask.GetMask();
    [SerializeField] private Bomb bombPrefab;

    private bool _canPlant = true;
    private void Update()
    {
        if (isInMovement)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayerTo(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayerTo(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayerTo(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayerTo(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlantBomb();
        }
    }
    
    private void MovePlayerTo(Vector2 dir)
    {
        if (Raycast(dir))
        {
            return;
        }

        isInMovement = true;
        _canPlant = false;
        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 0.28f).OnComplete(() =>
        {
            _canPlant = true;
            isInMovement = false;
        });
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, RaycastMask);
        return hit.collider != null;
    }

    private void PlantBomb()
    {
        if (_canPlant)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity); 
        }

    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
