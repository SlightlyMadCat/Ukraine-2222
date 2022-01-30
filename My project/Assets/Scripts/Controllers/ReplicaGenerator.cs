using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReplicaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject replicaText;
    [SerializeField] private List<int> possibleSounds = new List<int>();
    private Coroutine replicaShowCor;
    private int lastReplicaId = -99;
    
    public void GenerateReplica()
    {
        if(replicaShowCor != null) StopCoroutine(replicaShowCor);

        int _randId = -99;

        while (_randId == -99)
        {
            int _rand = Random.Range(0, possibleSounds.Count);
            if (_rand != lastReplicaId)
            {
                _randId = _rand;
                lastReplicaId = _rand;
            }
        }
        
        _randId = possibleSounds[_randId];
        
        SoundsManager.Instance.PlayCustomSoundByID(_randId);
        
        replicaShowCor = StartCoroutine(WaitToHideReplica(SoundsManager.Instance.GetSoundLengthById(_randId)));
    }

    private IEnumerator WaitToHideReplica(float _time)
    {
        replicaText.SetActive(true);
        yield return new WaitForSeconds(_time);
        replicaText.SetActive(false);
    }
}
