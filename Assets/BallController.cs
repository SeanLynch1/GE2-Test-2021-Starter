using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ballPrefab;

    public static List<GameObject> ballList = new List<GameObject>();

    public Transform throwPosition;

    public float throwForce = 15;

    private void Update()
    {
        ThrowBall();
    }
    public void ThrowBall()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBall = Instantiate(ballPrefab, throwPosition.position, Quaternion.identity);
            ballList.Add(newBall);
            newBall.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            //Destroy(other.gameObject);
        }
    }
}
