using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseStation : MonoBehaviour
{
    [SerializeField] private ResourceItem itemToSpawn;
    [SerializeField] private Transform desireItemOrbitalPos;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform defaultPlanet;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private SpaceSide spaceSide;
    
    private Transform orbitCopy;    //copy added to link objects through global coords
    
    private List<int> itemsToSpawn = new List<int>();   //contains count of desire items to spawn
    private float itemCreationTime = 1f;
    private float currentCreationTime = 0;
    [SerializeField] private Slider creationBar;
    [SerializeField] private Image image; 
    
    private void Awake()
    {
        orbitCopy = new GameObject("orbit-copy of"+transform.name).transform;
        orbitCopy.parent = spawnParent;
    }

    private void FixedUpdate()
    {
        orbitCopy.position = desireItemOrbitalPos.position;

        if (itemsToSpawn.Count > 0)
        {
            currentCreationTime += Time.fixedDeltaTime;

            if (currentCreationTime >= itemCreationTime)
            {
                itemsToSpawn.RemoveAt(0);
                SpawnItem();
                currentCreationTime = 0;
            }
        }
        
        creationBar.value = currentCreationTime;
    }

    public void StartSpawnProcess()
    {
        if(!EconomyController.Instance.CanSpawnItem(itemToSpawn.GetBuildCurrency())) return;
        else
        {
            //reduce money count
            EconomyController.Instance.ChangeMoneyCount(-itemToSpawn.GetBuildCurrency());
        }
        
        itemsToSpawn.Add(0);
    }
    
    private void SpawnItem()
    {
        ResourceItem _newItem = Instantiate(itemToSpawn.gameObject, spawnParent.transform).GetComponent<ResourceItem>();
        _newItem.Init(transform.position, defaultPlanet, spaceSide);
        _newItem.SetMoveTarget(orbitCopy);
        _newItem.GetComponent<DragDrop>().Init(mainCanvas);
        
        SoundsManager.Instance.PlayCustomSoundByID(2);
    }
}
