using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * This script control spawn terminal
 */
public class NewPlanetLoadSpawner : MonoBehaviour
{
    public static NewPlanetLoadSpawner Instance;

    [SerializeField] private List<SpawnTerminal> spawnTerminals = new List<SpawnTerminal>();
    [SerializeField] private RectTransform rightPlanet;
    [SerializeField] private int _lastUsedTerminal = -1;
    [SerializeField] private SpaceSide spaceSide;
    private void Awake()
    {
        Instance = this;
    }

    // spawn item, call when item moved through loading terminal
    public void SpawnItem(ResourceItem _item)
    {
        var _terminalID = GetTerminal();
        var _terminal = spawnTerminals[_terminalID];
       
        _item.SetParent(spaceSide.gameObject.transform);
        _item.Init(_terminal.GetTerminalPosition(), rightPlanet, spaceSide);
        _item.SetInTerminal(false);
        _item.SetMoveTarget(_terminal.GetRandomMovePosition());
        _item.SetStateGameobject(true);
    }

    // get terminal for cargo
    private int GetTerminal()
    {
        int _randomTerminal = Random.Range(0, spawnTerminals.Count);
        while (_randomTerminal == _lastUsedTerminal)
        {
            _randomTerminal = Random.Range(0, spawnTerminals.Count);
        }
        _lastUsedTerminal = _randomTerminal;
        return _lastUsedTerminal;
    }
}
