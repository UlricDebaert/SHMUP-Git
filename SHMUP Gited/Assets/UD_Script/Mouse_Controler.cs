using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Controler : MonoBehaviour
{
    Vector2 mouseAxes;
    Rigidbody2D pcRB;
    public  float sensibility = 50f;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        pcRB = this.GetComponent<Rigidbody2D>();

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        InputHandler();
        BoundariesDetection();
        Move();
    }

    void InputHandler()
    {
        mouseAxes.x = Input.GetAxis("Mouse X");
        mouseAxes.y = Input.GetAxis("Mouse Y");
    }

    void BoundariesDetection()
    {
        Vector3 selfPos = transform.position;

        if (selfPos.x >= screenBounds.x - objectWidth)
        {
            transform.position = new Vector2(screenBounds.x - objectWidth, selfPos.y);
            selfPos = transform.position;
            if (mouseAxes.x >= 0) mouseAxes.x = 0f;
        }

        if (selfPos.x <= screenBounds.x * -1 + objectWidth)
        {
            transform.position = new Vector2(screenBounds.x * -1 + objectWidth, selfPos.y);
            selfPos = transform.position;
            if (mouseAxes.x <= 0) mouseAxes.x = 0f;
        }

        if (selfPos.y >= screenBounds.y - objectWidth)
        {
            transform.position = new Vector2(selfPos.x, screenBounds.y - objectWidth);
            selfPos = transform.position;
            if (mouseAxes.y >= 0) mouseAxes.y = 0f;
        }

        if (selfPos.y <= screenBounds.y * -1 + objectWidth)
        {
            transform.position = new Vector2(selfPos.x, screenBounds.y * -1 + objectWidth);
            selfPos = transform.position;
            if (mouseAxes.y <= 0) mouseAxes.y = 0f;
        }
    }

    void Move()
    {
        pcRB.velocity = mouseAxes * sensibility;
    }
}
