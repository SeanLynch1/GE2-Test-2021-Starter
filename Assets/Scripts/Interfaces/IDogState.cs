using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDogState 
{
     IDogState DoState (DogBaseStateController dogBaseStateController);
}
