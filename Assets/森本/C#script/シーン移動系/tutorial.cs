using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    public GameObject tutorialbutton;
    public GameObject homebutton;
    void Start()
    {
        if (ResetButton.Tutflag)//tutorialシーンへ
        {
            homebutton.SetActive(false);

            tutorialbutton.SetActive(true);
            ResetButton.Tutflag = false;
        }
        else if (!ResetButton.Tutflag) //Homeシーンへ
        {
            homebutton.SetActive(true);

            tutorialbutton.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickStartButton()
    {
        if (ResetButton.Tutflag)//tutorialシーンへ
        {
            homebutton.SetActive(false);

            tutorialbutton.SetActive(true);
            ResetButton.Tutflag = false;
        }
        else if (!ResetButton.Tutflag) //Homeシーンへ
        {
            homebutton.SetActive(true);

            tutorialbutton.SetActive(false);
        }
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
    }
}
