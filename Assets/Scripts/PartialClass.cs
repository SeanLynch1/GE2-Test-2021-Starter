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
        GameEvents.Instance.onSit += Sit;
        GameEvents.Instance.onStopMoving += StopMoving;
    }

    private void OnDisable()
    {
        GameEvents.Instance.onLookForOwner -= LookForOwner;
        GameEvents.Instance.onLookForPickUp -= CanSeePickUp;
        GameEvents.Instance.onFetchBall -= FetchBall;
        GameEvents.Instance.onBallPickUp -= PickUpBall;
        GameEvents.Instance.onDropBall -= DropBall;
        GameEvents.Instance.onSit -= Sit;
        GameEvents.Instance.onStopMoving -= StopMoving;
    }

    Vector3 force = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    public void LookForOwner()
    {
        distance = Vector3.Distance(this.transform.position, playerTarget.position);
        transform.LookAt(new Vector3(playerTarget.position.x, 0, playerTarget.position.z));
        
        WagTail(distance / 3);
        if (distance >= 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z), distance * Time.deltaTime);
         
        }
        else //IF DOG HAS ARRIVED AT PLAYER
        {
            DropBall();
            Bark();
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

    public bool PickUpBall()
    {
        Vector3 ballPos = BallController.ballList[BallController.ballList.Count - 1].transform.position;
        ball = BallController.ballList[BallController.ballList.Count - 1];
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        float dist = Vector3.Distance(ballAttachPos.transform.position, ballPos);
        if (dist < pickUpDistance)
        {
            ball.transform.position = ballAttachPos.transform.position;
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
        WagTail(dist / 3);
        DropBall();
            transform.LookAt(new Vector3(ballPos.x, 0, ballPos.z));

        if (dist > pickUpDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(ballPos.x,transform.position.y,ballPos.z), dist * Time.deltaTime);
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
    public void Bark()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            int n = Random.Range(0, barkSounds.Count);
            barkSounds[n].Play();
            time = Random.Range(1.5f,5f);
        }
    }
    private bool toggle = false;

    public bool StopMoving()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            toggle = !toggle;
            Debug.Log("Sit!");
        }
        return toggle;
    }
    public void Sit()
    {
        transform.LookAt(playerTarget);
    }

    public void WagTail(float distSpeed)
    {
        startTime += Time.deltaTime;
        tail.transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startTime * distSpeed + Mathf.PI / 2) + 1.0f) / 2.0f );
    }
    
    Quaternion TailRotation(float angle)
    {
        var tailRotation = tail.transform.rotation;
        var angleY = tailRotation.eulerAngles.y + angle;

        if (angleY > 90)
        {
            angleY += 360;
        }
        else if (angleY < -90)
        {
            angleY -= 360;
        }

        tailRotation.eulerAngles = new Vector3(tailRotation.eulerAngles.x, angleY, tailRotation.eulerAngles.z);
        return tailRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            canSeePickUp = true;
        }
    }
}
