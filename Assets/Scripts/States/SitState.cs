using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : IDogState
{
    public IDogState DoState(DogBaseStateController dogBaseStateController)
    {
        if(!CanSeePickUp())
        {
            MoveTowardsPlayer();
            return dogBaseStateController.sitState;
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
}
