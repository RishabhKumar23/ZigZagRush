using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Time.deltaTime => this make sure 

public class CarController : MonoBehaviour
{
    // variables
    public float carSpeed;
    bool faceLeft, firstTab;



    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameStarted)
        {
            Move();
            CheckInput();
        }

        if(transform.position.y <= -2)
        {
            GameManager.instance.GameEnd();
        }

    } // end update

    void Move()
    {
        // this make sure no matter what device game is running the speed will be the same
        transform.position += transform.forward * carSpeed * Time.deltaTime;

    } // end move

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // function to change car direction
            ChangeDirection();
        }
    } //end CheckInput()

    // function to make car change direction (left or right)
    void ChangeDirection()
    {
        if(faceLeft)
        {
            faceLeft = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            faceLeft= true;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
