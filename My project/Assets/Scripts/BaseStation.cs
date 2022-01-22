using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStation : MonoBehaviour
{
    [SerializeField] private ResourceItem itemToSpawn;
    [SerializeField] private Transform desireItemOrbitalPos;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform defaultPlanet;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private SpaceSide spaceSide;
    
    private Transform orbitCopy;    //copy added to link objects through global coords

    private void Awake()
    {
        orbitCopy = new GameObject("orbit-copy of"+transform.name).transform;
        orbitCopy.parent = spawnParent;
    }

    private void FixedUpdate()
    {
        orbitCopy.position = desireItemOrbitalPos.position;
    }

    public void SpawnItem()
    {
        ResourceItem _newItem = Instantiate(itemToSpawn.gameObject, spawnParent.transform).GetComponent<ResourceItem>();
        _newItem.Init(transform.position, defaultPlanet, spaceSide);
        _newItem.SetMoveTarget(orbitCopy);
        _newItem.GetComponent<DragDrop>().Init(mainCanvas);
    }
}
