using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // References to the particle effects for different items
    public GameObject starEffect, diamondEffect;

    // Update is called once per frame
    void Update()
    {
        // To Rotate star and diamond
        transform.Rotate(new Vector3(0f, 0f, 100f)*Time.deltaTime);
    } // End Update

    // Called when the object collides with another collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Find the GameManager instance at runtime
            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager != null)
            {
                if (gameObject.tag == "Star")
                {
                    //Debug.Log("start inside Items star");
                    gameManager.GetStar();
                    //Debug.Log("end inside Items star");
                    Instantiate(starEffect, transform.position, Quaternion.identity);
                }
                else if (gameObject.tag == "Diamond")
                {
                    gameManager.GetDiamond();
                    Instantiate(diamondEffect, transform.position, Quaternion.identity);
                }
            }
            else
            {
                Debug.LogError("GameManager not found in the scene");
            }

            Destroy(gameObject);
        }
    }
}
