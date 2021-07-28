using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HikayeScene : MonoBehaviour
{
    public Dialogue dia;
    void Start()
    {
        dia = GameObject.Find("DialogueBox").GetComponentInChildren<Dialogue>();
        dia.StartDialogue();
    }
}
