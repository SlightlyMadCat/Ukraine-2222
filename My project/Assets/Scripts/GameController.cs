using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Good work when
 * finish factory >= item in game
 * loading factory >= item in game
 * Set al least 1 different item type for factory and terminal
 */
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private List<LoadingTerminal> loadingTerminals = new List<LoadingTerminal>();
    [SerializeField] private List<FinishFactory> finishFactories = new List<FinishFactory>();
    [SerializeField] private List<int> itemIDInGame = new List<int>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Recalculate();
    }

    // change terminal and finish factory ID
    public void Recalculate()
    {
        MixLoadingTerminals();
        MixFinishFactories();
    }

    private void MixFinishFactories()
    {
        List<int> availableID = CreateItemListCopy();
        foreach (var terminal in loadingTerminals)
        {
            int terminalID = -1;
            var listNumber = -1;
            if (availableID.Count > 0) // try get at least 1 color for all item
            {
                listNumber = Random.Range(0, availableID.Count);
                terminalID = availableID[listNumber];
                availableID.RemoveAt(listNumber);
            }
            else 
            {
                listNumber = Random.Range(0, itemIDInGame.Count);
                terminalID = itemIDInGame[listNumber];

            }
            terminal.SetTerminalType(terminalID);
        }
    }

    List<int> CreateItemListCopy()
    {
        List<int> availableID = new List<int>();
        foreach (var item in itemIDInGame)
        {
            availableID.Add(item);
        }

        return availableID;
    }
    
    private void MixLoadingTerminals()
    {
        List<int> availableID =CreateItemListCopy();
        foreach (var factory in finishFactories)
        {
            int _factoryID = -1;
            var listNumber = -1;
            if (availableID.Count > 0) // try get at least 1 color for all item
            {
                listNumber = Random.Range(0, availableID.Count);
                _factoryID = availableID[listNumber];
                availableID.RemoveAt(listNumber);
            }
            else
            {
                listNumber = Random.Range(0, itemIDInGame.Count);
                _factoryID = itemIDInGame[listNumber];
            }
            factory.SetItemType(_factoryID);
        }
    }
}
