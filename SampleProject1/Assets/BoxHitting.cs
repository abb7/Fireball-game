using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxHitting : MonoBehaviour
{
    [SerializeField] private GameObject idle;
    [SerializeField] private GameObject[] boxPieces;
    [SerializeField] private GameObject[] boxReward;
    private float timeSinceHit;
    private bool boxGotHit;
    public Text scroe;
    int scoreNum;
    public bool status = false;

    private void Awake()
    {
        scroe = GameObject.Find("Score").GetComponent<Text>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            boxGotHit = true;
            idle.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;

            for (int i = 0; i < boxPieces.Length; i++)
            {
                boxPieces[i].SetActive(true);
            }
            for (int i = 0; i < boxReward.Length; i++)
            {
                boxReward[i].SetActive(true);
                boxReward[i].GetComponent<Rigidbody2D>().AddForce(boxReward[i].transform.up * 50);
                scoreNum = int.Parse(scroe.text) + 1;
                scroe.text = scoreNum.ToString();
            }
            status = true;
        }

    }

    private void Update()
    {
        if (boxGotHit)
        {
            timeSinceHit += Time.deltaTime;

        }
        if (timeSinceHit > 2)
        {
            for (int i = 0; i < boxPieces.Length; i++)
            {
                boxPieces[i].SetActive(false);
            }
        }
    }
}
