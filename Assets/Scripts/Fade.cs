using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator anim;
    public void FadeScene()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
    }

}
