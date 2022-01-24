using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  [SerializeField] private Animator swordAnimator;

  [SerializeField] private float speed;
  [SerializeField] private float jumpForce;
  [SerializeField] private float checkGroundDistance;
  [SerializeField] private LayerMask groundLayer;

  private Rigidbody2D rb;
  private BoxCollider2D bc;
  private bool isGrounded;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    bc = GetComponent<BoxCollider2D>();
  }

  private void jump() {
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

  }

  private void move() {
    float x = Input.GetAxisRaw("Horizontal");
    float moveBy = x * speed;
    rb.velocity = new Vector2(moveBy, rb.velocity.y);
  }

  private void checkIfGrounded() {
    Vector2 sizeOfCollider = bc.size * (new Vector2(transform.localScale.x, transform.localScale.y));
    Vector2 point = bc.offset + new Vector2(transform.position.x, transform.position.y - sizeOfCollider.y / 2 - checkGroundDistance / 2);
    float angle;
    Vector3 axis;
    transform.rotation.ToAngleAxis(out angle, out axis);
    Collider2D collider = Physics2D.OverlapBox(point, new Vector2(sizeOfCollider.x, checkGroundDistance), angle, groundLayer);

    if (collider != null) {
      isGrounded = true;
    }
    else {
      isGrounded = false;
    }

  }

  void Update() {
    move();
    jump();
    checkIfGrounded();

    if (Input.GetKeyDown(KeyCode.J)) {
      swordAnimator.SetBool("swingSword", true);
    }
    else {
      swordAnimator.SetBool("swingSword", false);
    }

  }

}
