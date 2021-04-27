using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DogBaseStateController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private IDogState currentState;

    [SerializeField] private List<AudioSource> barkSounds = new List<AudioSource>();

    [SerializeField, Range(0.0f, 10f)]
    private float startTime = 0.0f;

    [SerializeField, Range(0.0f, 360f)]
    private float angle = 90.0f;
    Quaternion start, end;
    private float distance;
    public float movementSpeed;
    private float pickUpDistance = 1;
    private float time = 1.5f;

    private bool canSeePickUp = false;

    public Transform playerTarget;
    private GameObject ballAttachPos;
    private GameObject tail;
    private GameObject ball;
    private List<GameObject> currentBall = new List<GameObject>();

    #endregion
    #region States
    public MoveState moveState = new MoveState();
    public FetchState fetchState = new FetchState();
    public SitState sitState = new SitState();
    #endregion

    private void OnEnable()
    {
        currentState = moveState;
        ballAttachPos = GameObject.Find("ballAttach");
        tail = GameObject.Find("tail");

        start = TailRotation(angle);
        end = TailRotation(-angle);
    }

    private void Update()
    {
        currentState = currentState.DoState(this);
    }

    private void FixedUpdate()
    {
        
    }
}
