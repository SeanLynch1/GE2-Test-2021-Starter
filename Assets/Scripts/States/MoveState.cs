using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IDogState
{
    public IDogState DoState(DogBaseStateController dogBaseStateController)
    {
        if (Stop())
        {
            return dogBaseStateController.sitState;
        }
        if (!CanSeePickUp())
        {
            MoveTowardsPlayer();
            return dogBaseStateController.moveState;
        }
        
        else
        {
            return dogBaseStateController.fetchState;
        }
    }


    private void MoveTowardsPlayer()
    {
        GameEvents.Instance.LookForOwner();
    }

    private bool CanSeePickUp()
    {
       return GameEvents.Instance.CanSeePickUp();
    }
    public bool Stop()
    {
        return GameEvents.Instance.StopMoving();
    }
}
