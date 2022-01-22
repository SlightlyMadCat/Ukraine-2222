using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base draggable item logic
 */

public abstract class ResourceItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int resourceId;
    [SerializeField] private float moveSpeed;
    private float minimalSpeed = .15f;
    
    private Transform moveTarget;
    [SerializeField] private Transform defaultPlanet;   //planet to fall into
    private SpaceSide spaceSide;
    
    private void FixedUpdate()
    {
        if(!spaceSide.IsInsideArea(transform))  //check bounds
            DestroyObject();
        
        if(moveTarget == null) return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTarget.localPosition, moveSpeed);

        if (moveTarget != defaultPlanet)    //speed reducing if item is moving from planet towards the orbit
        {
            moveSpeed = moveSpeed - (moveSpeed * .00981f);
        }
        else //speed increasing if item is moving from orbit towards the planet
        {
            moveSpeed = moveSpeed + (moveSpeed * .03981f);
        }

        if (moveSpeed < minimalSpeed)   //if target has low speed on the orbit - move item to the planet
        {
            moveTarget = defaultPlanet;
        }

        if (moveTarget == defaultPlanet)    //check to destroy target
        {
            if(Vector3.Distance(transform.localPosition, moveTarget.localPosition) < .025f)
            {
                DestroyObject();
            }
        }
    }

    public void Init(Vector3 _startPos, Transform _defaultPlanet, SpaceSide _spaceSide)
    {
        transform.position = _startPos;
        defaultPlanet = _defaultPlanet;
        spaceSide = _spaceSide;
    }
    
    public void SetMoveTarget(Transform _target)
    {
        moveTarget = _target;
    }

    //called to start item falling
    public void ResetToDefaultTarget()
    {
        moveTarget = defaultPlanet;
    }

    public void ResetSpeed()
    {
        moveSpeed = minimalSpeed;
    }

    public void DestroyObject()
    {
        //here may be destroying effect
        Destroy(gameObject);
    }
}
