using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tomap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //var fader = new FadeTransition()
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("map");
    }

}
