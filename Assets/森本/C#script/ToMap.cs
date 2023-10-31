using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMap : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("map");
    }
}
