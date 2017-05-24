using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ballStates { NOT_CARRYING_BALL, CAN_PICK_UP_BALL, CARRYING_BALL, SHOULD_DROP_BALL }

public class Pickup : MonoBehaviour
{
    [SerializeField]
    Collider ball, dropZone;

    [SerializeField]
    float ballLerpSpeed = 10;

    [SerializeField]
    Text instructionsText;

    ballStates currentBallState = ballStates.NOT_CARRYING_BALL;

    private void Start()
    {
        instructionsText.text = "Press space to pick\nup the ball.";
    }

    private void Update()
    {
        switch (currentBallState)
        {
            case ballStates.CAN_PICK_UP_BALL:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    instructionsText.text = "Bring the ball over here\nand drop it in.";
                    PickUpBall();

                    currentBallState = ballStates.CARRYING_BALL;
                }
                break;
            case ballStates.CARRYING_BALL:
                ball.transform.position = Vector3.Lerp(ball.transform.position, transform.position, ballLerpSpeed * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    instructionsText.text = "Press space to pick\nup the ball.";
                    DropBall();
                }
                break;
            case ballStates.SHOULD_DROP_BALL:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    instructionsText.text = "Ta-daaaa!";
                    DropBall();
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == ball) currentBallState = ballStates.CAN_PICK_UP_BALL;
        if (other == dropZone && currentBallState == ballStates.CARRYING_BALL)
        {
            instructionsText.text = "Press space to\ndrop the ball.";
            currentBallState = ballStates.SHOULD_DROP_BALL;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == ball) currentBallState = ballStates.NOT_CARRYING_BALL;
        if (other == dropZone)
        {
            instructionsText.text = "Bring the ball over here\nand drop it in.";
            currentBallState = ballStates.CARRYING_BALL;
        }
    }

    void PickUpBall()
    {
        ball.transform.parent = transform;
        ball.transform.position = transform.position;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void DropBall()
    {
        ball.transform.parent = null;
        ball.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        currentBallState = ballStates.NOT_CARRYING_BALL;
    }
}
