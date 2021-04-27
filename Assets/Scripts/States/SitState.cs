using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitState : IDogState
{
    public IDogState DoState(DogBaseStateController dogBaseStateController)
    {
        SitBoy();

        if (!Stop())
        {
            return dogBaseStateController.moveState;
        }
        else return dogBaseStateController.sitState;
    }

    public void SitBoy()
    {
        GameEvents.Instance.Sit();
    }
    public bool Stop()
    {
       return GameEvents.Instance.StopMoving();
    }
}
