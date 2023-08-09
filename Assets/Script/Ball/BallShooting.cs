using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooting : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    public BallHolderType ballHolderType;
    private Rigidbody2D rb;
    [SerializeField] private float bounceForce;
    private SpriteRenderer spriteRenderer;
    private LineManager lineManager;
    private bool hasHit;
    private float Count;
    public BallShooting(bool isShoot)
    {

    }
    public Vector3 Direction { get => direction; set => direction = value; }

    void Start()
    {
        Count = 5;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        bounceForce = 400f;
        //        lineManager.SetOriginForLine(transform.position);
    }
    private void Awake()
    {
        lineManager = FindObjectOfType<LineManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
        CheckHit(new Vector2(transform.position.x, transform.position.y + spriteRenderer.bounds.size.y), direction, 100);
        Shoot(direction);
    }
    public void CheckHit(Vector3 ballPos, Vector3 direction, float distance)
    {
        hasHit = false;
        if (Count < -1000) return;
        if (GetInput.IsMousePress())
        {
            RaycastHit2D hit2D = Physics2D.Raycast(ballPos, direction, distance);
            if (hit2D)
            {
                if (hit2D.collider.tag != "Ball")
                {
                    Count--;
                    hasHit = true;
                    Debug.Log("hit other");
                    if (hit2D.collider == null) return;
                    Vector2 normal = hit2D.normal;
                    Vector2 newDirection = Vector2.Reflect(direction, normal);
                    Debug.Log($" Count {Count}");
                    Debug.DrawRay(hit2D.point, newDirection, Color.green);
                    // hit2D = Physics2D.Raycast(hit2D.point, newDirection, distance);
                    //CheckHit(hit2D.point - new Vector2(0.001f, 0), newDirection, distance);
                }
                else
                {
                    //lineManager.AddNewPointToLine(hit2D.point);
                    Debug.Log($"hit ball {hit2D.collider.name}");
                }

            }
        }
    }
    private void Shoot(Vector3 direction)
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb.isKinematic = false;
            rb.AddForce(direction * bounceForce);
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
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Debug.Log("collide with matrix ball");
            rb.isKinematic = true;
        }
        else
        {
            // direction = new Vector2(-direction.y, direction.x);
            Debug.Log($"original dir {direction}");
            Vector2 inNormal = other.contacts[0].normal;
            direction = Vector2.Reflect(direction, inNormal);
            Debug.Log($"reflex dir {direction}");
            rb.AddForce(direction * bounceForce);
        }
    }
}
