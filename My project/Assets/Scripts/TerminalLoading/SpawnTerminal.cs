using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Spawn terminal sample,
 * have position and move target list
 */
public class SpawnTerminal : MonoBehaviour
{
    private RectTransform myRect;

    [SerializeField] private List<RectTransform> movePosition = new List<RectTransform>(); // move to this position
    private int lastUsedPosition = -1;

    private void Awake()
    {
        myRect = gameObject.GetComponent<RectTransform>();
    }

    public Vector3 GetTerminalPosition()
    {
        return myRect.position;
    }

    public RectTransform GetRandomMovePosition()
    {
        var _id = Random.Range(0, movePosition.Count);
        while (_id == lastUsedPosition)
        {
            _id = Random.Range(0, movePosition.Count);
        }

        lastUsedPosition = _id;
        
        return movePosition[_id];
    }
}