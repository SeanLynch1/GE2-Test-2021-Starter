using System;
using UnityEngine;
using System.Collections.Generic;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance 
    {                                 
        get;                          
        set;                        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public delegate bool BoolDel();
    public event BoolDel onLookForPickUp;
    public event BoolDel onBallPickUp;
    public event BoolDel onStopMoving; 

    public event Action onLookForOwner;
    public event Action onFetchBall;
    public event Action onDropBall;
    public event Action onSit;

    public bool StopMoving()
    {
        if(onStopMoving != null)
        {
           return onStopMoving();
        }
        else
        {
            return false;
        }
    }
    public void Sit()
    {
        if(onSit != null)
        {
            onSit();
        }
    }
    public void DropBall()
    {
        if(onDropBall != null)
        {
            onDropBall();
        }
    }
    public bool PickUpBall()
    {
        if(onBallPickUp != null)
        {
            return onBallPickUp();
        }
        else
        {
            return false;
        }
    }
    public void FetchBall()
    {
        if(onFetchBall != null)
        {
            onFetchBall();
        }
    }
    public bool CanSeePickUp()
    {
        if(onLookForPickUp != null)
        {
           return onLookForPickUp();
        }
        else
        {
            return false;
        }
    }
    public void LookForOwner()
    {
        if(onLookForOwner != null)
        {
            onLookForOwner();
        }
    }
}
