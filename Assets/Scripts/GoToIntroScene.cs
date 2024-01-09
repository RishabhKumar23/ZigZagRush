using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToIntroScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GotoScene",8);
    } // End Start

    void GotoScene()
    {
        SceneManager.LoadScene("carGarage");
    } //End GotoScene

    // Update is called once per frame
    void Update()
    {
        
    } // End Update
}
