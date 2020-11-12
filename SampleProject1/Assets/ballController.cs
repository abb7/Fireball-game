using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    public bool isPressed;
    private Rigidbody2D rb;
    public Vector2 mousePosition;
    private SpringJoint2D sj;
    private float releseDelay;
    private float maxDragDistance = 2f;
    private Rigidbody2D slingRb;


    enum State
    {
        playing,
        gameComplete,
        gameOver,
    }

    State gameState = State.playing;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        slingRb = sj.connectedBody;

        releseDelay = 1 / (sj.frequency * 4);
    }

    private void OnMouseDown()
    {
        isPressed = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        StartCoroutine(Release());

    }
    void Update()
    {
        if (isPressed)
        {
            DragTheBall();
        }
    }

    void DragTheBall()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, slingRb.position);
        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePosition - slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePosition;
        }


    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releseDelay);
        sj.enabled = false;
    }

    public void Goal()
    {
        gameState = State.gameComplete;
        GameStop();
    }

    void GameStop()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, 0);
    }
}
