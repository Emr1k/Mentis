using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Dialogue : MonoBehaviour
{
    public Text text;
    public string sahne;
    [TextArea(0,20)]
    public string[] sentences;
    
    public float textSpeed;
    public Animator anim;
    public GameObject[] objeler;

    int index;
    void Start()
    {
        text.text = string.Empty;

    }
    private void Update()
    {
        if (index + 1 > sentences.Length) { endDialogue();}
    }

    public void StartDialogue()
    {
        Debug.Log("a");
        anim.SetBool("Open", true);
        index = 0;
        StartCoroutine(Type());
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().speed = 0f;
        GetComponent<Interactible>().enabled = false;

    }
    IEnumerator Type()
    {
        //if (index+1 > sentences.Length) { endDialogue(); yield return null; }
        objeler[index].SetActive(true);
        foreach (GameObject item in objeler)
        {
            if(item != objeler[index]) { item.SetActive(false); }
        }
        foreach (char c in sentences[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    public void ButtonFonk()
    {
        if(text.text == sentences[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            text.text = sentences[index];
        }
    }

    void NextLine()
    {
        if(index < sentences.Length + 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(Type());
        }
        else
        {
            endDialogue();
            Debug.LogError("asdasdad");
        }
    }

    private void endDialogue()
    {
        SceneManager.LoadScene(sahne);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().speed = 8f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Interactible>().enabled = true;
        anim.SetBool("Open", false);
    }

    // Input

}
