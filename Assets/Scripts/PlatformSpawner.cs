using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*                                              Learning
    
    Instantiate => The Instantiate method in Unity 
                   is used to create copies of a prefab in the game world.
  
    
 */

public class PlatformSpawner : MonoBehaviour
{
    // variables
    public GameObject platform;

    public Transform lastPltform;
    /*
     Vector3 => store position of an object like (x value, y value)
     */
    Vector3 lastPosition;
    Vector3 newPosition;

    public bool stop;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = lastPltform.position;

        // to call coroutine function
        StartCoroutine(SpawnPlatform());


    } // end start

    // Update is called once per frame
    void Update()
    {
        
    } // end update

    // function to regenerate platform
    void GeneratePosition()
    {
        newPosition = lastPosition;
        int rand = Random.Range(0, 2); // it will print either 0 or 1 , but not 2

        if(rand > 0)
        {
            newPosition.x += 2f;

        }else
        {
            newPosition.z += 2f;
        }

        // Instantiate a new platform at the calculated position with no rotation.
        // Instantiate(platform, newPosition,Quaternion.identity);

    } // end GeneratePosition()

    // functon to spawnPlatforms
    IEnumerator SpawnPlatform() // this is also called coroutine function 
    {
        while (!stop)
        {
            GeneratePosition();
            Instantiate(platform, newPosition,Quaternion.identity);
            lastPosition = newPosition;
            yield return new WaitForSeconds(0.2f);
        }
    } // end spawnPlatforms


}
