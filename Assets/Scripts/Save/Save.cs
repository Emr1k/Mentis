using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Animator[] anims;
    public GameObject winEkran;
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1f;

        int warrokWin = PlayerPrefs.GetInt("warrokWin", 0);
        if(warrokWin == 1) {
            GameObject.Find("Ev4").GetComponentInChildren<Interactible>().enabled = false;
            anims[3].enabled = false;
        }

        int skeletonWin = PlayerPrefs.GetInt("skeletonWin", 0);
        if (skeletonWin == 1) { 
            GameObject.Find("Ev1").GetComponentInChildren<Interactible>().enabled = false;
            anims[0].enabled = false;
        }

        int benWin = PlayerPrefs.GetInt("benWin", 0);
        if (benWin == 1) { 
            GameObject.Find("Ev3").GetComponentInChildren<Interactible>().enabled = false;
            anims[2].enabled = false;
        }

        int awWin = PlayerPrefs.GetInt("awWin", 0);
        if (awWin == 1) { 
            GameObject.Find("Ev2").GetComponentInChildren<Interactible>().enabled = false;
            anims[1].enabled = false;
        }

        Debug.Log(warrokWin + "" + skeletonWin +" "+ benWin + "" + awWin);

        if(awWin == 1 && benWin == 1 && skeletonWin == 1 && warrokWin == 1)
        {
            winEkran.gameObject.SetActive(true);
        }
    }

}
