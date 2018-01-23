using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {
    [SerializeField]
    GameObject obj;
    [SerializeField]
    float speed = 2f;
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.right*obj.transform.position.x, speed*Time.deltaTime);
	}
}
