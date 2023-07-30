using System;
using UnityEngine;
[Serializable]
public class Item : MonoBehaviour
{
    public BallType ballType;
    // Start is called before the first frame update
    [SerializeField] private float radius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
public enum BallType
{
    BALL_01,
    BALL_02,
    BALL_03,
    BALL_04,
    BALL_05
}
