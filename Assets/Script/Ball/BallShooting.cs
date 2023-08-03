using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooting : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    public BallHolderType ballHolderType;
    private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D collider2D;
    private bool isShoot;

    public BallShooting(bool isShoot)
    {

    }
    public Vector3 Direction { get => direction; set => direction = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        collider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ShowShootDirection();
    }
    public void ShowShootDirection()
    {
        if (!IsMainBall())
            return;
        var firstPos = GetInput.GetMouseInput();
        if (!IsPosValid(firstPos)) return;
        direction = firstPos - transform.position;
        direction.z = 0;
        Debug.DrawRay(transform.position, Direction, Color.green);
        GetInput.CheckHit(transform.position, direction, 100);
        Shoot(direction);
    }

    private void Shoot(Vector3 direction)
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb.isKinematic = false;
            rb.AddForce(direction * 100f);
            ballHolderType = BallHolderType.NONE;
        }
    }
    public void SetNullSelf()
    {
    }
    private void OnMouseEnter()
    {
        if (!IsMainBall()) return;
        LineManager.Instance.DirectionLine.ResetLine();
    }

    private bool IsPosValid(Vector3 firstPos)
    {
        if (Vector3.Distance(firstPos, transform.position) < 0.5f)
        {
            return false;
        }
        else return true;
    }
    private bool IsMainBall()
    {
        if (ballHolderType != BallHolderType.MAINBALL)
            return false;
        else return true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ball")
        {
            rb.isKinematic = true;
        }
    }
}
