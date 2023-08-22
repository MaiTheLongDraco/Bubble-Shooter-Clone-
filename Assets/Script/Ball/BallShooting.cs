using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallShooting : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction;
    public BallHolderType ballHolderType;
    private Rigidbody2D rb;
    [SerializeField] private float _velocity;
    [SerializeField] private LineHandle lineHandle;
    private MatrixBall matrixBall;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
    }
    private void Awake()
    {
        lineHandle = FindObjectOfType<LineHandle>();
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
        Shoot(direction);
    }
    private void Shoot(Vector3 direction)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!IsMetCondition()) return;
            rb.isKinematic = false;
            rb.AddForce(direction.normalized * _velocity);
            ballHolderType = BallHolderType.NONE;
            this.gameObject.AddComponent<CircleCollider2D>();
        }
    }
    private bool IsMetCondition()
    {
        if (GetInput.GetMouseInput().y < BallHolderManger.Instance.MainTranform.position.y)
        {
            lineHandle.ResetLine();
            return false;
        }
        else return true;
    }

    private void OnMouseEnter()
    {
        if (!IsMainBall()) return;
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
            var ballIndex = other.collider.GetComponent<MatrixBall>().index;
            Debug.Log($"collide with index {ballIndex}");
            // AddMatrixIndex()
        }
    }
    private void AddMatrixIndex(Vector2Int passIndex)
    {
    }

    public void SetBallVelocity(float vel)
    {
        _velocity = vel;
    }
}
