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
            SetActiveIcon(true);
            CheckHit(ballPosMain, direction, 100, layerMask1);
        }
        public void CheckHit(Vector2 ballPos, Vector2 direction, float distance, LayerMask layerHit)
        {
            if (GetInput.IsMousePress())
            {
                RaycastHit2D hit2D = Physics2D.Raycast(ballPos, direction, distance, layerHit);
                ballTest.DeclareLine();
                SetActiveIcon(true);
                ballTest.SetPos(0, ballPos);
                HandleHit(hit2D, ballPos, direction, distance, layerHit);
                ResetIndex();
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
                case "UpLimit":
                    {
                        AddPoint(hit2D.point);
                    }
                    break;
                case "DownLimit":
                    {
                        SetLastPoint(hit2D.point);
                    }
                    break;
                default:
                    {
                        listIcon[index].transform.position = hit2D.point;
                        if (CheckLineContainPos(hit2D.point))
                            return;
                        CheckHit(ballPos, newDirection, distance, layerHit);
                    }
                    break;
            }
            EndCheck();
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
            ballTest.SetPos(index, point);
            listIcon[index - 1].transform.position = point;
        }
        void SetLastPoint(Vector2 point)
        {
            SetActiveICon(true, point);
            ballTest.SetLastPointForLine(point);
        }
        void SetActiveICon(bool isActive, Vector2 point)
        {
            listIcon[index - 1].gameObject.SetActive(isActive);
            listIcon[index - 1].transform.position = point;
        }
        private void ResetIndex()
        {
            index++;
            if (index > ballTest.Line.positionCount - 1)
            { index = 1; }
        }
        private bool CheckLineContainPos(Vector2 pos)
        {
            if (ballTest.GetPos().Contains(pos))
            {
                return true;
            }
            else
            {
                AddPoint(pos);
                return false;
            }
        }
    }
}



