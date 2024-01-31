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

    }

    // Update is called once per frame
    void Update()
    {
        //if (tut || ResetButton.Tutflag)//tutorialシーンへ
        //{
        //    homebutton.SetActive(false);
        //    tut = false;
        //    ResetButton.Tutflag = false;
        //}
        //else if (tut == false)//Homeシーンへ
        //{
        //    tutorialbutton.SetActive(false);
        //    homebutton.SetActive(true);
        //}

    }
}
