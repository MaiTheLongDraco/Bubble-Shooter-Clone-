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
    private bool hasCollide;

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
    private void Update()
    {
        CheckDistanceToAdd();
    }
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            // if (hasCollide) return;
            // Debug.Log("collide with matrix ball ]]]]]");
            // var ballIndex = other.collider.GetComponent<MatrixBall>().index;
            // Debug.Log($"collide with index {ballIndex}");
            // AddToMatrix();
        }
    }
    private void CheckDistanceToAdd()
    {
        var thisPos = transform.position;
        var predictPos = PredictBallPosToAdd.Instance.DesireBallPos;
        if (Vector2.Distance(thisPos, predictPos) <= 2f)
        {
            AddToMatrix();
        }
    }
    private void AddToMatrix()
    {
        onHitMatrix?.Invoke();
        hasCollide = true;
    }

    public void SetBallVelocity(float vel)
    {
        _velocity = vel;
    }

    public void AddListenerFotHitEvent(UnityAction call)
    {
        print($"ball shooting colide with  matrix ]]]]]");
        onHitMatrix.AddListener(call);
    }
    public MatrixBall GetMatrixBall()
    {
        return this.GetComponent<MatrixBall>();
    }
}
