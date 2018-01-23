using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caracter : Unit
{
    bool isBusy = false;
    Spell spell = new Spell();
    void Update()
    {
        if (!isBusy)
        {
            if (isGrounded)
            {
                State = CharState.Idle;
            }
            else
            {
                State = CharState.Jump;
            }
            if (Input.GetButton("Horizontal")) Run();
            if (isGrounded && Input.GetButtonDown("Jump")) Jump();
            if (Input.GetButtonDown("Fire1")) Attack();
            if (Input.GetButtonDown("Fire2")) Magic();
        }
    }
    void Run()
    {

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");


        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        transform.localScale = new Vector3(Mathf.Sign(Input.GetAxis("Horizontal")), transform.localScale.y, transform.localScale.z);

        if (isGrounded) State = CharState.Walk;
    }
    void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = CharState.Jump;

    }
    void Attack()
    {
        isBusy = true;
        if (Random.Range(0, 100) > 35)
        {
            State = CharState.Attack;
        }else
        {
            State = CharState.Attack2;
        }
    }


    public void attackSystem()
    {
        Unit[] enemyes = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit obj in enemyes)
        {
            if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <= attackRange && obj.tag == "Enemy")
            {
                obj.ReceiveDamage(this, attackDamage);
            }
        }
        isBusy = false;
    }
    void Magic()
    {
        isBusy = true;
        State = CharState.Magic;
    }
    public void magicSystem()
    {
        Unit[] enemyes = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit obj in enemyes)
        {
            if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <=spell.range  && obj.tag == "Enemy")
            {
                obj.ReceiveDamage(this, spell.damage);
            }
        }
        isBusy = false;
    }
}
