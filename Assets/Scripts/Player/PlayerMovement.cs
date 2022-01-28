using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(SpriteRenderer))]
//[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
  public float m_GravityScale = 2.5f;
  public float m_Gravity = 2.5f;

  /** Controls the physics of the character. */
  Rigidbody2D m_body;

  /** The direction of the character*/
  public Vector2 m_direction;

  /** Controls how fast the character can go */
  public float m_MaxSpeed;
  public float m_walkSpeed;
  public float m_currentMaxSpeed;

  /** The running speed of the character*/
  public float m_currentSpeed;

  /** Controls how high the character jumps*/
  public float m_jumpingForce;
  public float m_jumpingForce2;
  public float m_jumpingWallForce;
  public float m_currentJumpingWallForce;
  public float m_jumpingWallSpeed;
  public float m_jumpingWallTimeTofinish;
  public float m_jumpingWallTime;
  /** Controls how high the character jumps*/
  public float m_currentJumpForce;

  /** The active acceleration of the character*/
  public float m_acceleration;

  /** The active acceleration of the character*/
  public float m_JumpUpForceMultiply;
  /** The active acceleration of the character*/
  public float m_JumpFallForceMultiply;

  /** The active acceleration of the character*/
  float m_NormalAcceleration;

  /** The active acceleration of the character*/
  float m_JumpAcceleration;


  /** Controls how long it takes for the character to reach max speed*/
  public float m_timeToMaxSpeed = 0.5f;

  Animator m_animator;
  SpriteRenderer m_sprite;
  Vector3 m_scale;

  public bool m_bGorunded = false;
  public bool m_bJumping = true;
  public bool m_bDobleJumping = true;
  public bool m_bInWall = false;
  public bool m_bInWallLeft = false;
  public bool m_bInWallRight = false;
  [SerializeField] private LayerMask m_platformsLayer;
  [SerializeField] private LayerMask m_wallsLayer;
  [SerializeField] private LayerMask m_cellingsLayer;

  BoxCollider2D m_boxCollider;
  public BoxCollider2D m_bodyCollider;
  public BoxCollider2D m_headCollider;


  float m_rightSpeed = 0;
  float m_rightTime = 0;

  float m_leftSpeed = 0;
  float m_leftTime = 0;

  public float m_jumpSpeed = 0;
  public float m_jumpTime = 0;

  public float m_waitToCheckGround = 0.2f;
  public float m_elaptseToCheckGround = 0.2f;
  // Start is called before the first frame update
  // Start is called before the first frame update
  void Start() {
    m_body = GetComponent<Rigidbody2D>();
    m_sprite = GetComponent<SpriteRenderer>();
    m_animator = GetComponent<Animator>();
    //m_shooter = GetComponent<PlayerShoot>();
    m_boxCollider = GetComponent<BoxCollider2D>();

    m_body.gravityScale = m_GravityScale;
    //m_shooter.onStart();
    m_currentMaxSpeed = m_MaxSpeed;
    m_scale = transform.localScale;
    m_acceleration = 1.0f;
    m_direction = Vector3.right;
    m_NormalAcceleration = m_MaxSpeed / m_timeToMaxSpeed;
    //m_JumpAcceleration = m_NormalAcceleration * 0.5f;
  }
  public void Update()
  {
    if (m_waitToCheckGround <= m_elaptseToCheckGround)
    {
      m_bGorunded = checkIsGrounded();
    }
    if (m_bGorunded)
    {
      m_jumpSpeed = 0;
      m_jumpTime = 0;
      m_currentJumpForce = 0;
      m_bJumping = false;
      m_bDobleJumping = false;
    }
    else
    {
      m_elaptseToCheckGround += Time.deltaTime;
      m_bJumping = true;
    }
    
    m_NormalAcceleration = m_currentMaxSpeed / m_timeToMaxSpeed;

    handleInput();

    float speedx = Mathf.Abs(m_body.velocity.x);
    bool isTooMuchVelocity = speedx > m_MaxSpeed;
    //m_animator.SetFloat("speed", speedx);
    //m_animator.SetBool("jumping", !m_bGorunded);
    //m_animator.SetFloat("jumpSpeed", m_jumpSpeed);
    //m_animator.SetBool("Attacking", m_shooter.shooting);
  }
  bool
  checkIsGrounded()
  {
    RaycastHit2D cast =
      Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size, 0, Vector2.down, 0.1f, m_platformsLayer);
    return cast.collider != null;
  }

  void
  checkCelling()
  {
    RaycastHit2D cast =
       Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size, 0, Vector2.up, 0.1f, m_platformsLayer);
    if (cast.collider != null)
    {
      m_currentJumpForce = 0;
      m_jumpTime = 0;
    }
  }

  void
  checkWall()
  {
    m_bInWall = false;
    m_bInWallRight = false;
    m_bInWallLeft = false;
    float distance = 0.4f;
    RaycastHit2D cast =
    Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size * 0.5f, 0, Vector2.right, distance, m_platformsLayer);
    if (cast.collider != null)
    {
      m_body.AddForce(new Vector2(-m_currentSpeed, 0));
      m_rightSpeed = 0;
      m_rightTime = 0;
      m_currentSpeed = 0;
      m_bInWall = true;
      m_bInWallRight = true;
    }
    cast =
    Physics2D.BoxCast(m_boxCollider.bounds.center, m_boxCollider.bounds.size * 0.5f, 0, Vector2.left, distance, m_platformsLayer);
    if (cast.collider != null)
    {
      m_body.AddForce(new Vector2(-m_currentSpeed, 0));
      m_leftSpeed = 0;
      m_leftTime = 0;
      m_currentSpeed = 0;
      m_bInWall = true;
      m_bInWallLeft = true;
    }
  }

  private void
  handleInput()
  {

    m_direction.x = 0;
    bool firstJump = false;
    if (Input.GetKeyDown(KeyCode.C))
    {
      m_currentMaxSpeed = m_walkSpeed;
    }
    if (Input.GetKeyUp(KeyCode.C))
    {
      m_currentMaxSpeed = m_MaxSpeed;
    }


    if (m_bGorunded && Input.GetKeyDown(KeyCode.Z))
    {
      m_acceleration = m_JumpAcceleration;
      //m_body.velocity = Vector2.up * m_jumpingForce;
      m_currentJumpForce = m_jumpingForce;
      m_elaptseToCheckGround = 0;
      m_bJumping = true;
      m_bGorunded = false;
      firstJump = true;
      //GetComponent<JumpSound>().play();
    }
    else
    {
      m_acceleration = m_NormalAcceleration;
    }
    checkCelling();
    if (m_bJumping)
    {
      calculeJump();
    }

    if (Input.GetKey(KeyCode.RightArrow))
    {
     // m_shooter.m_direcrtion = Vector3.right;
      transform.localScale = new Vector3(m_scale.x, m_scale.y, m_scale.z);
      m_direction += Vector2.right;
      calculateRigthSpeed();
    }
    else
    {
      calculateDesAcelerateRigthSpeed();
    }
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      //m_shooter.m_direcrtion = -Vector3.right;
      transform.localScale = new Vector3(-m_scale.x, m_scale.y, m_scale.z);
      m_direction += Vector2.left;
      calculateLeftSpeed();
    }
    else
    {
      calculateDesAcelerateLeftSpeed();
    }
    checkWall();
    if (m_bInWall && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && !m_bGorunded)
    {
      if (m_jumpingWallTime == 0)
      {
        m_jumpTime = 0;
        m_jumpSpeed = 0;
        m_currentJumpForce = 0;

        m_currentSpeed = 0;
      }

      m_bDobleJumping = false;
      if (Input.GetKeyDown(KeyCode.Z))
      {
          m_jumpingWallTime = m_jumpingWallTimeTofinish;
        if (m_bInWallRight)
          m_currentJumpingWallForce = -m_jumpingWallForce;
        else
          m_currentJumpingWallForce = m_jumpingWallForce;

        m_currentJumpForce = m_jumpingForce2;
        //m_body.AddForce(new Vector2(0, -m_body.velocity.y));
        calculeJump();
        m_bDobleJumping = false;
      }
    }
    else if (m_bJumping && Input.GetKeyDown(KeyCode.Z) && !m_bDobleJumping && !firstJump)
    {
      m_bDobleJumping = true;
      m_currentJumpForce = m_jumpingForce2;
      m_elaptseToCheckGround = 0;

      m_body.AddForce(new Vector2(0, -m_jumpSpeed));
      m_jumpSpeed = 0;
      m_jumpTime = 0;
    }
    calculateWallSpeed();
    m_currentSpeed = m_rightSpeed - m_leftSpeed + m_jumpingWallSpeed;
    m_body.velocity = new Vector2(m_currentSpeed, m_jumpSpeed);
    //transform.position = new Vector2(transform.position.x+m_currentSpeed, transform.position.y + m_jumpSpeed);
  }

  void calculateRigthSpeed()
  {
    m_rightTime += Time.deltaTime;
    if (m_rightTime >= m_timeToMaxSpeed)
    {
      m_rightTime = m_timeToMaxSpeed;
    }
    m_rightSpeed = m_acceleration * m_rightTime;
  }

  void calculateWallSpeed()
  {
    m_jumpingWallTime -= Time.deltaTime;
    if (m_jumpingWallTime < 0)
    {
      m_jumpingWallTime = 0;
    }
    m_jumpingWallSpeed = m_currentJumpingWallForce * m_jumpingWallTime;

  }

  void calculateDesAcelerateRigthSpeed()
  {
    m_rightTime -= Time.deltaTime;
    if (m_rightTime <= 0)
    {
      m_rightTime = 0;
    }
    m_rightSpeed = m_acceleration * m_rightTime;
  }

  void calculateLeftSpeed()
  {
    m_leftTime += Time.deltaTime;
    if (m_leftTime >= m_timeToMaxSpeed)
    {
      m_leftTime = m_timeToMaxSpeed;
    }
    m_leftSpeed = m_acceleration * m_leftTime;
  }

  void calculateDesAcelerateLeftSpeed()
  {
    m_leftTime -= Time.deltaTime;
    if (m_leftTime <= 0)
    {
      m_leftTime = 0;
    }
    m_leftSpeed = m_acceleration * m_leftTime;
  }

  void calculeJump()
  {
    if (m_jumpSpeed < 0)
    {
      m_jumpTime += Time.deltaTime * m_JumpFallForceMultiply;
    }
    else
      m_jumpTime += Time.deltaTime * m_JumpUpForceMultiply;
    m_jumpSpeed = m_currentJumpForce - m_Gravity * m_jumpTime;
  }
}
