using UnityEngine;
using UnityEngine.Events;

public class BallShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public BallHolderType ballHolderType;
    private Rigidbody2D rb;
    [SerializeField] private float _velocity;
    [SerializeField] private LineHandle lineHandle;
    private PredictBallPosToAdd predictBall;
    private BallHolderManger ballHolderManger;
    public UnityEvent onHitMatrix;

    public float Velocity { get => _velocity; set => _velocity = value; }

    void Start()
    {
        ballHolderManger = FindObjectOfType<BallHolderManger>();
        predictBall = PredictBallPosToAdd.Instance;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
    }
    private void Awake()
    {
        lineHandle = FindObjectOfType<LineHandle>();
    }

    // Update is called once per frame
    public void ShowShootDirection(Vector2 shootDir)
    {
        print($" shootDIR {shootDir}");
        Shoot(shootDir);
    }
    private void Shoot(Vector3 direction)
    {
        rb.AddForce(direction.normalized * _velocity);
        ballHolderType = BallHolderType.NONE;
        this.gameObject.AddComponent<CircleCollider2D>();
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
            this.transform.position = predictBall.DesireBallPos;
            transform.parent = ballHolderManger.BallParent;
            AddToMatrix();
        }
    }
    private void AddToMatrix()
    {
        onHitMatrix?.Invoke();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        this.gameObject.layer = LayerMask.NameToLayer("Ball");
        this.gameObject.tag = "Ball";
        Destroy(this);
    }

    public void SetBallVelocity(float vel)
    {
        _velocity = vel;
    }
    public void AddListenerFotHitEvent(UnityAction call)
    {
        onHitMatrix.AddListener(call);
    }
    public MatrixBall GetMatrixBall()
    {
        return this.GetComponent<MatrixBall>();
    }
}
