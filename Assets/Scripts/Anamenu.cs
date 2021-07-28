using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Anamenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Hikaye");
    }
    public void Sifirla()
    {
        PlayerPrefs.SetInt("warrokWin", 0);
        PlayerPrefs.SetInt("skeletonWin", 0);
        PlayerPrefs.SetInt("benWin", 0);
        PlayerPrefs.SetInt("awWin", 0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
