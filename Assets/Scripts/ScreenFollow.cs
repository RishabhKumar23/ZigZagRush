using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFollow : MonoBehaviour
{
    // variable
    public Transform target;
    Vector3 distance;
    public float followSpeed;

    [SerializeField][Range(0f, 1f)] float LerpTime;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float change = 0f;
    int len;

    private void Awake()
    {
        //target = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        // this will give us distance between car and camera
        distance = target.position - transform.position;
        len = myColors.Length;
    } // end start

    // Update is called once per frame
    void Update()
    {
        if(target.position.y >= 0)
        {
            FollowTheCar();
        }
    } // end update

    // to follow the car
    void FollowTheCar()
    {
        Vector3 currentPosition = transform.position;
        // this will make sure that the distance between can and camera remain same.
        Vector3 targetPosition = target.position - distance;

        transform.position = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
    }
}
