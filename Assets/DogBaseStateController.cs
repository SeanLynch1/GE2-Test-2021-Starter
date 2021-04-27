using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DogBaseStateController : MonoBehaviour
{
    [SerializeField]
    private IDogState currentState;

    private float distance;
    public float movementSpeed;
    private float pickUpDistance = 1;
    private bool canSeePickUp = false;

    public Transform playerTarget;
    private GameObject ballAttachPos;
    private List<GameObject> currentBall = new List<GameObject>();
    //STATES
    public SitState sitState = new SitState();
    public FetchState fetchState = new FetchState();

    private void OnEnable()
    {
        currentState = sitState;
        ballAttachPos = GameObject.Find("ballAttach");
    }

    private void Update()
    {
        currentState = currentState.DoState(this);
    }

}
