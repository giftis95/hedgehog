using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeOgject : MonoBehaviour {

    [SerializeField]
    Sprite ligthTex;
    [SerializeField]
    Sprite darkTex;

    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("is");
            sprite.sprite = darkTex;
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("si");
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("is");
            sprite.sprite = darkTex;
        }
    }

}
