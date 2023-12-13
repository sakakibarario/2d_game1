using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Totutorial : MonoBehaviour
{
    public GameObject tutorialbutton;
    public GameObject homebutton;

    static public bool tut = true;

    // Start is called before the first frame update
    void Start()
    {
        if (tut)
        {
            homebutton.SetActive(false);
        }
        else if (tut == false)
        {
            tutorialbutton.SetActive(false);
            homebutton.SetActive(true);
        }
        tut = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
