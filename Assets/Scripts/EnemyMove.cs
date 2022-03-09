using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;

    Rigidbody2D _rigidBody;
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (IsFacingRight())
        {
            _rigidBody.velocity = new Vector2(_moveSpeed, 0f);
        }
        else
        {
            _rigidBody.velocity = new Vector2(-_moveSpeed, 0f);
        }
        
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_rigidBody.velocity.x)), 1f);
    }
}
