namespace Test
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;

    public class CheckHitTest : MonoBehaviour
    {
        [SerializeField] private UnityEvent onHit;
        [SerializeField] GameObject shootPoint;
        [SerializeField] private Vector2 ballPosMain;
        private BallTest ballTest;
        [SerializeField] private Vector2 vectorDistance;
        [SerializeField]
        private BallShooting ballShooting;
        [SerializeField] private List<GameObject> listIcon;
        int index = 1;
        private Vector2 recursiveStartPoint;
        // Start is called before the first frame update
        void Start()
        {
            ballTest = FindObjectOfType<BallTest>();
            ballPosMain = shootPoint.transform.position;
            SetActiveIcon(false);
        }
        private void SetActiveIcon(bool isActive)
        {
            listIcon.ForEach(icon => icon.gameObject.SetActive(isActive));
        }

        // Update is called once per frame
        void Update()
        {
            ClickHandle();
        }
        public void ClickHandle()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - ballPosMain;
            CheckHit(ballPosMain, direction, 10);
        }
        public void CheckHit(Vector3 ballPos, Vector3 direction, float distance)
        {
            if (GetInput.IsMousePress())
            {
                ballTest.DeclareLine();
                // ballTest.SetPos(0, ballPosMain);
                RaycastHit2D hit2D = Physics2D.Raycast(ballPos, direction, distance);
                if (hit2D)
                {
                    ballTest.SetPosForListLine(0, ballPos, hit2D.point);
                    recursiveStartPoint = hit2D.point - vectorDistance / hit2D.point;
                    if (hit2D.collider.tag == "Ball")
                    {
                        // ballTest.SetPos(index, hit2D.point);
                        return;
                    }
                    else if (hit2D.collider.tag == "DownLimit")
                    {
                        //ballTest.SetPos(index, hit2D.point);
                        return;
                    }
                    else
                    {
                        Vector2 normal = hit2D.normal;
                        Vector2 newDirection = Vector2.Reflect(direction, normal);
                        if (hit2D.collider.tag == "UpLimit")
                        {
                            var targetNornal = Vector2.down;
                            float cosAngle = Vector3.Dot(normal.normalized, targetNornal.normalized);
                            recursiveStartPoint = hit2D.point - new Vector2(0, 0.1f);
                            if (!Mathf.Approximately(cosAngle, 1f))
                            {
                                normal = targetNornal;
                                newDirection = Vector2.Reflect(direction, normal);
                            }
                        }
                        Debug.Log($" dirrection {direction}--- new direction {newDirection}");
                        Debug.Log($" hit2D point--- {hit2D.point}");
                        Debug.Log($" hit2D normal--- {hit2D.normal}");
                        Debug.DrawRay(hit2D.point, normal, Color.cyan);
                        ballTest.SetActiveLineWithIndex(index - 1, true);
                        if (!ballTest.GetPos().Contains(hit2D.point))
                        {
                            // ballTest.SetPos(index, hit2D.point);
                            ballTest.SetPosForListLine(index, ballPos, hit2D.point);
                            listIcon[index - 1].SetActive(true);
                            listIcon[index - 1].transform.position = hit2D.point;
                            var angle = Vector2.Angle(direction, newDirection);
                            Debug.Log($"angle direction with newDirection {angle}");
                            var angle1 = Vector2.Angle(direction, normal);
                            Debug.Log($"angle direction with normal {angle1}");
                            var angle2 = Vector2.Angle(direction, Vector2.right);
                            Debug.Log($"angle direction with surrface {angle2}");

                        }
                        if (index >= ballTest.Line.positionCount - 1)
                        {
                            index = 1;
                            return;
                        }
                        index++;
                        CheckHit(recursiveStartPoint, newDirection, distance);
                    }

                }
            }
            else
            {
                index = 1;
                ballTest.ResetLine();
                ballTest.SetActiveListLine(false);
                SetActiveIcon(false);
            }
        }
    }
}



