using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchState : IDogState
{
    public IDogState DoState(DogBaseStateController dogBaseStateController)
    {
        if (Stop())
        {
            return dogBaseStateController.sitState;
        }
        if (!PickUpBall())
        {
            FetchBall();
            return dogBaseStateController.fetchState;
        }
        
        else
        {
            return dogBaseStateController.moveState;
        }
    }

    private void FetchBall()
    {
         GameEvents.Instance.FetchBall();
    }

    private bool PickUpBall()
    {
        return GameEvents.Instance.PickUpBall();

        
    }
    public bool Stop()
    {
        return GameEvents.Instance.StopMoving();
    }
}
