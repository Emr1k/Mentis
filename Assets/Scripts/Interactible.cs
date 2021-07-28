using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactible : MonoBehaviour
{
    public Animator anim;
    public GameObject text;
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    void Update()
    {
        if (isInRange)
        { 
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInRange = true;
            anim.SetBool("isEnter", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
            anim.SetBool("isEnter", false);
        }
    }

}
