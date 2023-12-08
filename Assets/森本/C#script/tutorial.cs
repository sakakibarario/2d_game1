using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{

    static public bool tut = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickStartButton()
    {
        if (tut == true)
        {
            SceneManager.LoadScene("Tutorial");
        }
        if (tut == false)
        {
            SceneManager.LoadScene("Home");
        }
        tut = false;
    }
}
