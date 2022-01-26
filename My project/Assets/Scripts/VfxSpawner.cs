using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxSpawner : MonoBehaviour
{
    #region Singleton

    public static VfxSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [Serializable]
    public class VfxSample
    {
        [SerializeField] private string name;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float lifeTime;

        public void Spawn(Vector3 _pos)
        {
            Transform _newVfx = Instantiate(prefab, _pos, Quaternion.identity).transform;
            _newVfx.position = new Vector3(_pos.x, _pos.y, 81.1f);
            Destroy(_newVfx.gameObject, lifeTime);
        }
    }
    [SerializeField] private List<VfxSample> vfxSamples = new List<VfxSample>();

    //called to spawn custom vfx
    public void SpawnVfx(int _id, Vector3 _pos)
    {
        vfxSamples[_id].Spawn(_pos);
    }
}
