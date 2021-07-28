using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    Transform player;
    public float turnSpeed;
    private Vector3 diff;
    public Vector3 offset;
    public bool reverseX;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        diff = new Vector3(player.position.x, player.position.y + offset.x, player.position.z + offset.y);
        if (reverseX) { turnSpeed *= -1; }
    }
    void Update()
    {
        /*
        float mouseX;
        float mouseY;
        mouseX = Input.GetAxisRaw("Mouse X") * speed;
        mouseY = Input.GetAxisRaw("Mouse Y") * speed;

        transform.Rotate()
    }
        */
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
