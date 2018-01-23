using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    [SerializeField]
    public int lives = 20;
    [SerializeField]
    protected float speed = 3f;
    [SerializeField]
    protected float jumpForce = 15f;
    [SerializeField]
    protected float attackRange = 0.7f;
    [SerializeField]
    protected int attackDamage;
    protected new Rigidbody2D rigidbody;
    protected Animator animator;

    protected bool isGrounded = false;
    protected CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    void FixedUpdate()
    {
        CheckGround();
    }
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public virtual void ReceiveDamage(Unit enemy, int dam)
    {
        lives-=dam;
        if (lives <= 0)
        {
            Die();
        }
        Debug.Log(lives);
        rigidbody.AddForce(Vector3.Normalize(transform.position-enemy.transform.position) * 5f, ForceMode2D.Impulse);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        isGrounded = colliders.Length > 1;

    }
}
public enum CharState
{
    Idle,
    Walk,
    Jump,
    Attack,
    Attack2,
    Magic
}