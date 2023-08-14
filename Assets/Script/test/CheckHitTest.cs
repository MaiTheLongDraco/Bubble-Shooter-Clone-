using System.Numerics;
using System.Collections.Generic;
namespace Test
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;

    public class CheckHitTest : MonoBehaviour
    {
        [SerializeField] GameObject shootPoint;
        [SerializeField] private Vector2 ballPosMain;
        private BallTest ballTest;
        [SerializeField] private Vector2 vectorDistance;
        [SerializeField] private List<GameObject> listIcon;
        [SerializeField] private Transform test;
        [SerializeField] private List<Vector3> listHitPos = new List<Vector3>();
        public static CheckHitTest Instance;
        public List<Vector3> ListHitPos { get => listHitPos; set => listHitPos = value; }
        int index = 1;
        private Vector2 recursiveStartPoint;
        private string[] startLayerMask = { "UpLimit", "RightLimit", "LeftLimit", "DownLimit" };
        private LayerMask layerMask1;
        // Start is called before the first frame update
        void Start()
        {
            layerMask1 = LayerMask.GetMask(startLayerMask);
            ballTest = FindObjectOfType<BallTest>();
            ballPosMain = shootPoint.transform.position;
            SetActiveIcon(false);
        }
        private void Awake()
        {
            Instance = this;
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
            ballTest.DeclareLine();
            CheckHit(ballPosMain, direction, 100, layerMask1);
        }
        public void CheckHit(Vector2 ballPos, Vector2 direction, float distance, LayerMask layerHit)
        {
            if (GetInput.IsMousePress())
            {
                RaycastHit2D hit2D = Physics2D.Raycast(ballPos, direction, distance, layerHit);
                //   ballTest.SetPos(0, ballPos);
                HandleHit(hit2D, ballPos, direction, distance, layerHit);
            }
            else
            {
                EndCheck();
            }
        }
        private void HandleHit(RaycastHit2D hit2D, Vector2 ballPos, Vector2 direction, float distance, LayerMask layerHit)
        {
            var hitObjTag = hit2D.collider.tag;
            var normal = hit2D.normal;
            var newDirection = Vector2.Reflect(direction, normal);
            Debug.Log($"index {index}");
            Debug.DrawRay(ballPos, direction);
            switch (hitObjTag)
            {
                case "DownLimit":
                    {
                        //SetLastPoint(hit2D.point);
                        SetActiveICon(true, hit2D.point);
                        Debug.DrawRay(hit2D.point, normal, Color.cyan);
                        Debug.Log(" hit down limit vvvv");
                    }
                    break;
                case "UpLimit":
                    {
                        normal = Vector2.down;
                        Debug.DrawRay(hit2D.point, newDirection, Color.cyan);
                    }
                    break;
                default:
                    {
                        normal = Vector2.right;
                        Debug.DrawRay(hit2D.point, normal, Color.black);
                        Debug.DrawRay(hit2D.point, newDirection, Color.yellow);
                        listIcon[index - 1].transform.position = hit2D.point;
                        if (CheckLineContainPos(hit2D.point))
                            return;
                        if (IsOverIndex())
                            return;
                        CheckHit(hit2D.point, newDirection, distance, layerHit);
                        Debug.Log(" hit left or right limit vvvv");
                    }
                    break;
            }
        }
        private void EndCheck()
        {
            ballTest.ResetLine();
            index = 1;
            SetActiveIcon(false);
        }

        void AddPoint(Vector3 point)
        {
            SetActiveICon(true, point);
            ballTest.SetPos(index - 1, point);
            print($"index {index}");
            listIcon[index - 1].transform.position = point;
        }
        void SetLastPoint(Vector2 point)
        {
            SetActiveICon(true, point);
            ballTest.SetLastPointForLine(point);
        }
        private void SetActiveIcon(bool isActive)
        {
            listIcon.ForEach(icon => icon.gameObject.SetActive(isActive));
        }
        void SetActiveICon(bool isActive, Vector2 point)
        {
            listIcon[index - 1].gameObject.SetActive(isActive);
            listIcon[index - 1].transform.position = point;
        }
        private bool IsOverIndex()
        {
            if (index >= ballTest.Line.positionCount - 1)
            {
                index = 1;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckLineContainPos(Vector2 pos)
        {
            if (ballTest.GetPos().Contains(pos))
            {
                return true;
            }
            else
            {
                //AddPoint(pos);
                return false;
            }
        }
    }
}



