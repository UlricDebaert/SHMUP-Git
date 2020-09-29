using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool playerIsMoving;

    public float playerSpeed = 5.0f;
    public float timeSinceAccelerated;
    public float timeSinceDeccelerated;
    public float accelerationTime = 0.5f;
    public float deccelerationTime = 0.5f;

    public AnimationCurve acceleration = AnimationCurve.EaseInOut(0, 0, 0.75f, 2);
    public AnimationCurve decceleration = AnimationCurve.EaseInOut(0, 1, 1, 0);

    public Rigidbody2D rb;

    //////////

    public Vector2 playerInput;
    Vector2 mouseAxes;
    Rigidbody2D pcRB;
    public float sensibility = 50f;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public SpriteRenderer PCsprite;

    void Start()
    {
        pcRB = this.GetComponent<Rigidbody2D>();

        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = PCsprite.bounds.extents.x;
        objectHeight = PCsprite.bounds.extents.y;
    }

    void Update()
    {
        InputHandler();
    }

    void InputHandler()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            playerIsMoving = true;
        }
        else playerIsMoving = false;

        playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * sensibility;
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

    //////////

    void FixedUpdate()
    {
        //Mouvement();
        BoundariesDetection();
        rb.velocity = playerInput.normalized * playerSpeed;
    }

    void Mouvement()
    {
        if (!playerIsMoving)
        {
            timeSinceAccelerated = 0;
            timeSinceDeccelerated += Time.deltaTime;
        }
        else if (playerIsMoving)
        {
            timeSinceAccelerated += Time.deltaTime;
            timeSinceDeccelerated = 0;
        }

        float accelerationMultiplier = 1;
        if (accelerationTime > 0)
            accelerationMultiplier = acceleration.Evaluate(timeSinceAccelerated / accelerationTime);

        float deccelerationMultiplier = 1;
        if (deccelerationTime > 0)
            deccelerationMultiplier = decceleration.Evaluate(timeSinceDeccelerated / deccelerationTime);

        if (playerIsMoving)
        {
            rb.velocity = playerInput.normalized * playerSpeed * accelerationMultiplier;
        }

        if (!playerIsMoving)
        {
            rb.velocity = new Vector2(rb.velocity.x * deccelerationMultiplier, rb.velocity.y * deccelerationMultiplier);
        }
    }
}
