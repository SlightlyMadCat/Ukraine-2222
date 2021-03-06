using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FinishFactory : MonoBehaviour, IDropHandler
{
    [SerializeField] private int itemIDForFactory; // get this type of item
    [SerializeField] private float spawnRange = 10;
    [SerializeField] private Transform baseParent;  //parent to money prefs
    
    [SerializeField] private Image image;
    // detect cargo
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var resourceItem = eventData.pointerDrag.GetComponent<ResourceItem>();
            if (resourceItem.GetResourceID() == itemIDForFactory)
            {
                EconomyController.Instance.AddResourceAmount(resourceItem.GetResourceID(), resourceItem.GetResAmount() * resourceItem.GetBoost());
                resourceItem.DestroyObject(false);

                SpawnMoneyPrefab(resourceItem.GetBoost());
                SoundsManager.Instance.PlayCustomSoundByID(2);
            }
            else
            {
                resourceItem.DestroyObject(true);
                Debug.LogError("bad item type");
            }

            //GameController.Instance.Recalculate();
        }
    }

    private void SpawnMoneyPrefab(int boost)
    {
        var _money = Instantiate(EconomyController.Instance.moneyPrefab, transform.parent);
        _money.AddBoost(boost);
        
        Vector3 _randomOffset = new Vector2();
        _randomOffset.x = Random.Range(0, spawnRange);
        _randomOffset.y = Random.Range(0, spawnRange);

        _money.gameObject.transform.localPosition = transform.localPosition + _randomOffset;
        _money.gameObject.transform.SetParent(baseParent);
        Debug.LogError("money++");        
    }

    // set item type for finish panel
    public void SetItemType(int _type)
    {
        itemIDForFactory = _type;
        image.sprite =  ItemDataBase.Instance.GetSpriteByItemID(_type);
    }

    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    public int GetAttachedItemId()
    {
        return itemIDForFactory;
    }

    public void SetFactoryScale(Vector3 _scale)
    {
        transform.localScale = _scale;
    }
}
