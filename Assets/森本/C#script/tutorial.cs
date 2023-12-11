using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{

    static public bool tut = true;

    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

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
            Initiate.Fade(sceneName, fadeColor, fadeSpeed);
            //SceneManager.LoadScene("Tutorial");
        }
        if (tut == false)
        {
            SceneManager.LoadScene("Home");
        }
        tut = false;
    }
}
