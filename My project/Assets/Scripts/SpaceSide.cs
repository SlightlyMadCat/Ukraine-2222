using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script allows to detect if item is placed in the selected area bounds
 */

public class SpaceSide : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private Transform[] spaceCorners;  //left top + right bottom

    public enum Side
    {
        left, right
    }
    public Side side;
    
    public bool IsInsideArea(Transform _item)
    {
        if (_item.localPosition.x > spaceCorners[0].localPosition.x &&
            _item.localPosition.x < spaceCorners[1].localPosition.x)
        {
            if (_item.localPosition.y < spaceCorners[0].localPosition.y &&
                _item.localPosition.y > spaceCorners[1].localPosition.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
