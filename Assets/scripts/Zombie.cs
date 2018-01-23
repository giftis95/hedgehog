using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Unit {
    [SerializeField]
    float ViewRange = 5f;

    bool isAgro = false;
    bool isAttack = false;
    void Update()
    {
        checkAttack();
        if(!isAttack) Run();
        if (isAttack) Attack();
    }

    Vector3 oldPosition = Vector3.zero;
    int dir = 1;
    void Run()
    {
        Transform player = GameObject.FindObjectOfType<caracter>().transform;
        if (Vector3.Distance(player.position, transform.position) > ViewRange)
        {   
            Vector3 direction = transform.right*dir;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            transform.localScale = new Vector3(dir*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isAgro = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.localScale = new Vector3((player.position.x > transform.position.x ? 1 : -1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isAgro = true;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        if (colliders.Length > 3 ) dir = -dir;
        State = CharState.Walk;
    }

    void checkAttack()
    {
        Transform player = GameObject.FindObjectOfType<caracter>().transform;
        isAttack  = Vector3.Distance(player.position, transform.position) <= attackRange;
    }

    void Attack()
    {
            State = CharState.Attack;
    }


    public void attackSystem()
    {
        Unit[] enemyes = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit obj in enemyes)
        {
            if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <= attackRange && obj.tag == "Player")
            {
                obj.ReceiveDamage(this, attackDamage);
            }
        }
    }
}
