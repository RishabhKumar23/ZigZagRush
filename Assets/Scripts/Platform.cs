using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject platformEffect;
    public GameObject diamond, star;
    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = Random.Range(1, 50);
        Vector3 tempPosition = transform.position; // to store temp position of the star and diamond
        tempPosition.y = 1f; // This will set star and diamond position above platform
        // TO generate star and diamond randomally
        if (randomNumber < 3)
        {
            Instantiate(star, tempPosition, star.transform.rotation);
        }
        if(randomNumber == 25)
        {
            Instantiate(diamond, tempPosition, diamond.transform.rotation);
        }

    } // end Start

    // Update is called once per frame
    void Update()
    {
        
    } //end Update

    private void OnCollisionExit(Collision collision)
    {
        // To Destroy platform
        if(collision.gameObject.tag == "Player")
        {
            Invoke("FallDown", 0.2f);
            // FallDown();
        }
    } // End OnCollisionExit

    void FallDown()
    {
        Instantiate(platformEffect, transform.position, Quaternion.identity);
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 0.5f);
    }
}
