using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RewardingScript : MonoBehaviour
{
    public GameObject scroe;
    public int gemReward = 1;
    private void Awake()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*10);
        
        StartCoroutine(Collect());
    }

    private IEnumerator Collect()
    {
        yield return new WaitForSeconds(1);
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        //Destroy(gameObject);
    }
}
