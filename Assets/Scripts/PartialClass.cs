using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DogBaseStateController : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.onLookForOwner += LookForOwner;
        GameEvents.Instance.onLookForPickUp += CanSeePickUp;
        GameEvents.Instance.onFetchBall += FetchBall;
        GameEvents.Instance.onBallPickUp += PickUpBall;
        GameEvents.Instance.onDropBall += DropBall;
    }

    private void OnDisable()
    {
        GameEvents.Instance.onLookForOwner -= LookForOwner;
        GameEvents.Instance.onLookForPickUp -= CanSeePickUp;
        GameEvents.Instance.onFetchBall -= FetchBall;
        GameEvents.Instance.onBallPickUp -= PickUpBall;
        GameEvents.Instance.onDropBall -= DropBall;
    }


    public void LookForOwner()
    {
        distance = Vector3.Distance(this.transform.position, playerTarget.position);
        if(distance >= 10)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, playerTarget.position, Time.deltaTime * movementSpeed);
            transform.LookAt(playerTarget.position);
        }
        else //IF DOG HAS ARRIVED AT PLAYER
        {
            DropBall();
        }
    }

    public bool CanSeePickUp()
    {
        if(canSeePickUp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private GameObject ball;
    public bool PickUpBall()
    {
        Vector3 ballPos = BallController.ballList[BallController.ballList.Count - 1].transform.position;
        ball = BallController.ballList[BallController.ballList.Count - 1];
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        float dist = Vector3.Distance(ballAttachPos.transform.position, ballPos);
        if (dist < pickUpDistance)
        {
            ball.transform.parent = ballAttachPos.transform;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            currentBall.Add(ball);
            canSeePickUp = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void FetchBall()
    {
        Vector3 ballPos = BallController.ballList[BallController.ballList.Count - 1].transform.position;
        float dist = Vector3.Distance(ballAttachPos.transform.position, ballPos);

        Vector3 look = ballPos - transform.position;
        look.y = 0;

        Quaternion q = Quaternion.LookRotation(look);
        if (Quaternion.Angle(q, transform.rotation) <= 180)
            BallController.ballList[BallController.ballList.Count - 1].transform.rotation = q;

        DropBall();
        if (dist > pickUpDistance)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, ballPos, movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, BallController.ballList[BallController.ballList.Count - 1].transform.rotation, Time.deltaTime * 2.0f);
        }
    }
    public void DropBall()
    {
        if (currentBall.Count == 1)
        {
            GameObject ball = currentBall[currentBall.Count - 1];
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            ball.transform.parent = null;
            currentBall.Clear();
        }
        else if (currentBall.Count == 1)
        {
            if (currentBall[currentBall.Count - 1] != ball)
            {
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
                ball.transform.parent = null;
                currentBall.Clear();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            canSeePickUp = true;
            Debug.Log("Ball Was Found");
        }
    }
}
