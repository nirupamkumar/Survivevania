using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _runSpeed = 5f;
    [SerializeField] float _jumpSpeed = 28f;
    [SerializeField] float _climbSpeed = 5f;
    [SerializeField] Vector2 _deathThrow = new Vector2(25f, 25f);

    //state
    bool _isAlive = true;

    //cached reference components
    private Rigidbody2D _rigidBody;
    private Animator _myAnimator;
    private CapsuleCollider2D _bodyCollider;
    private BoxCollider2D _feetCollider;
    private float _gravityInLadder;
   
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _gravityInLadder = _rigidBody.gravityScale;
    }

    void Update()
    {
        if (!_isAlive)
        {
            return;
        }

        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Death();
    }

    void Run()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        _rigidBody.velocity = new Vector2(horizontalMovement * _runSpeed, _rigidBody.velocity.y);

        bool toPlayerFlip = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool("Running", toPlayerFlip);
    }

    void ClimbLadder()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _myAnimator.SetBool("Climbing", false);
            _rigidBody.gravityScale = _gravityInLadder;
            return;
        }

        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector2 climbVelocity = new Vector2(_rigidBody.velocity.x, verticalMovement * _climbSpeed);
        _rigidBody.velocity = climbVelocity;
        _rigidBody.gravityScale = 0f;

        bool playerVerticalSpeed = Mathf.Abs(_rigidBody.velocity.y) > Mathf.Epsilon;
        _myAnimator.SetBool("Climbing", playerVerticalSpeed);
    }

    void Jump()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground Touch")))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpVelocity = new Vector2(0f, _jumpSpeed);
            _rigidBody.velocity += jumpVelocity;
        }
    }

    void FlipSprite()
    {
        bool toPlayerFlip = Mathf.Abs(_rigidBody.velocity.x) > Mathf.Epsilon;

        if (toPlayerFlip)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidBody.velocity.x), 1f);
        }
    }

    void Death()
    {
        if (_bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            _isAlive = false;
            _myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = _deathThrow;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
