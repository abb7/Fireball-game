using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BirdController : MonoBehaviour
{
    Vector3 _initialPostion;
    public float launchSpeed = 100f;
    int totalBallNum = 3;
    bool lunchedYet = false;
    private float _timeSittingAround;

    public GameManager gameManager;
    private void Awake()
    {
        _initialPostion = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lunchedYet && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }
        //idnetify if ball outside the frame
        if (transform.position.y > 7 || transform.position.y < -4 || transform.position.x > 10 || transform.position.x < -9 )
        {
            //string currentSceneName = SceneManager.GetActiveScene().name;
            Destroy(this.transform.parent.gameObject);
            gameManager.StartGame();
            //SceneManager.LoadScene(currentSceneName);
        }
        if (_timeSittingAround > 3 && lunchedYet)
        {
            Destroy(this.transform.parent.gameObject);
            gameManager.StartGame();
            //check if avaiable balls and bring one more
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        UnityEngine.Vector2 directionToInitalPosition = _initialPostion - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitalPosition * launchSpeed);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        lunchedYet = true;
    }
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

}
