using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Base draggable item logic
 */

public abstract class ResourceItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int resourceId;
    private float moveSpeed = 4f;
    private float maxSpeed;
    private float minimalSpeed = .15f;
    
    private Transform moveTarget;
    [SerializeField] private Transform defaultPlanet;   //planet to fall into
    private SpaceSide spaceSide;
    private DragDrop dragDrop;

    private RectTransform myRect;
    private bool isInTerminal; // loading to right planet
    [SerializeField] private float standardItemSendingTime = 2f; // seconds
    [SerializeField] private float resAmountOnItem = .25f;
    [SerializeField] private float buildCurrency = 100;
    [SerializeField] private Image image;

    [HideInInspector] public SpaceSide.Side side;

    [Header("boost")]
    [SerializeField] private int boost = 1;
    [SerializeField] private TextMeshProUGUI boostText;

    [Header("boost distance and speed to target in privat")]
    [SerializeField] private ResourceItem boostTarget;
    private float boostDistance = 1f;
    private float boostSpeedToTarget = 2.55f;
    
    private void Awake()
    {
        maxSpeed = moveSpeed;
        dragDrop = GetComponent<DragDrop>();
        myRect = gameObject.GetComponent<RectTransform>();
        
        ItemDataBase.Instance.AddSpawnedItem(this);

        UpdateBoostText();
        OnAwake();
    }

    private void FixedUpdate()
    {
        if(UiController.Instance.SomeViewIsActive()) return;
        
        if(!spaceSide.IsInsideArea(transform))  //check bounds
            DestroyObject(true);

        if(dragDrop.IsTouched() && !IsRightSide()) // work for left side
            OnFixedUpdate();

        if (boostTarget != null && !IsRightSide())
        {
            transform.position = Vector3.Lerp(   transform.position ,boostTarget.transform.position, Time.fixedDeltaTime * boostSpeedToTarget);
            if (Vector3.Distance(transform.position, boostTarget.transform.position) < boostDistance)
            {
                boostTarget.AddOneBoost(GetBoost());
                Destroy(gameObject);
            }
        }
        else
        {
            if(dragDrop.IsTouched()) return;
        
            if(moveTarget == null) return;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTarget.localPosition, moveSpeed);

            if (moveTarget != defaultPlanet)    //speed reducing if item is moving from planet towards the orbit
            {
                moveSpeed = moveSpeed - (moveSpeed * .02081f);
            }
            else //speed increasing if item is moving from orbit towards the planet
            {
                moveSpeed = moveSpeed + (moveSpeed * .02581f);
            }

            if (moveSpeed < minimalSpeed)   //if target has low speed on the orbit - move item to the planet
            {
                moveTarget = defaultPlanet;
            }

            if (moveTarget == defaultPlanet)    //check to destroy target
            {
                if(Vector3.Distance(transform.localPosition, moveTarget.localPosition) < .025f)
                {
                    DestroyObject(true);
                }
            }
        }
    }

    public abstract void OnFixedUpdate();

    public void Init(Vector3 _startPos, Transform _defaultPlanet, SpaceSide _spaceSide)
    {
        spaceSide = _spaceSide;
        transform.position = _startPos;
        defaultPlanet = _defaultPlanet;
        side = _spaceSide.side;
    }
    
    public void SetMoveTarget(Transform _target)
    {
        moveTarget = _target;
    }

    //called to start item falling
    public void ResetToDefaultTarget()
    {
        if(GetIsInTerminal()) return;
        SetMoveTarget(defaultPlanet);
    }

    public void ResetToMinSpeed()
    {
        moveSpeed = minimalSpeed;
    }

    public void ResetToMaxSpeed()
    {
        moveSpeed = maxSpeed;
    }

    public void DestroyObject(bool _playVfx)
    {
        //here may be destroying effect
        ItemDataBase.Instance.RemoveSpawnedItem(this);

        if (_playVfx)
        {
            VfxSpawner.Instance.SpawnVfx(0, transform.position);
            SoundsManager.Instance.PlayCustomSoundByID(3);
        }

        Destroy(gameObject);
    }

    // Get rect transform
    public void SetRectPosition (Vector3 _newPos)
    {
        myRect.position = _newPos;
    }

    //enable/disable item on scene
    public void SetStateGameobject(bool _val)
    {
        gameObject.SetActive(_val);
    }

    // set state cargo in terminal or not
    public void SetInTerminal(bool _val)
    {
        isInTerminal = _val;
    }

    private bool GetIsInTerminal()
    {
        return isInTerminal;
    }

    public void SetParent(Transform parent)
    {
        gameObject.transform.SetParent(parent);
    }

    public int GetResourceID()
    {
        return resourceId;
    }

    public float GetSendingTime()
    {
        return standardItemSendingTime;
    }

    public float GetResAmount()
    {
        return resAmountOnItem;
    }

    public float GetBuildCurrency()
    {
        return buildCurrency;
    }

    public Image GetImage()
    {
        return image;
    }

    public void SetRaycastingState(bool _val)
    {
        image.raycastTarget = _val;
    }

    public void UpdateBoostText()
    {
        boostText.text = "x" + boost;
    }

    public void UpdateBoostInt(int _boost)
    {
        boost = _boost;
        
    }

    public void AddOneBoost(int _boost)
    {
        boost += _boost;
        UpdateBoostText();
    }

    public int GetBoost()
    {
        return boost;
    }

    public bool IsRightSide()
    {
        return side == SpaceSide.Side.right;
    }

    public abstract void OnAwake();

    public void SetBoostTarget(ResourceItem target)
    {
        boostTarget = target;
    }
}
