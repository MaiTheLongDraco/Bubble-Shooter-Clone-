using System.Numerics;
using System.Collections.Generic;
namespace Test
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.IO.Compression;

    public class CheckHitTest : MonoBehaviour
    {
        [SerializeField] GameObject shootPoint;
        private BallTest ballTest;
        [SerializeField] private List<GameObject> listIcon;
        public static CheckHitTest Instance;
        int index = 1;
        private string[] startLayerMask = { "UpLimit", "RightLimit", "LeftLimit", "DownLimit" };
        private LayerMask layerMask1;
        // Start is called before the first frame update
        void Start()
        {
            layerMask1 = LayerMask.GetMask(startLayerMask);
            ballTest = FindObjectOfType<BallTest>();
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
            Vector2 direction = mousePos - (Vector2)shootPoint.transform.position;
            CheckHit(shootPoint.transform.position, direction.normalized, 100, layerMask1);
        }
        public void CheckHit(Vector2 ballPos, Vector2 direction, float distance, LayerMask layerHit)
        {
            if (GetInput.IsMousePress())
            {
                RaycastHit2D hit2D = Physics2D.Raycast(ballPos, direction, distance, layerHit);
                ballTest.DeclareLine();
                ballTest.SetPos(0, shootPoint.transform.position);
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
                        SetLastPoint(hit2D.point);
                        SetActiveICon(true, hit2D.point);
                        Debug.DrawRay(hit2D.point, normal, Color.cyan);
                        Debug.Log(" hit down limit vvvv");
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
                        layerHit = HandleTag(layerHit, hitObjTag);
                        CheckHit(hit2D.point, newDirection, distance, layerHit);
                        Debug.Log(" hit left or right limit vvvv");
                    }
                    break;
            }
        }

        private LayerMask HandleTag(LayerMask layerHit, string hitObjTag)
        {
            if (hitObjTag == "UpLimit")
            {
                layerHit = LayerMask.GetMask(new string[] { "DownLimit", "RightLimit", "LeftLimit" });
            }
            if (hitObjTag == "DownLimit")
            {
                layerHit = LayerMask.GetMask(new string[] { "UpLimit", "RightLimit", "LeftLimit" });
            }
            if (hitObjTag == "RightLimit")
            {
                layerHit = LayerMask.GetMask(new string[] { "UpLimit", "DownLimit", "LeftLimit" });
            }
            if (hitObjTag == "LeftLimit")
            {
                layerHit = LayerMask.GetMask(new string[] { "UpLimit", "DownLimit", "RightLimit" });
            }

            return layerHit;
        }

        private void EndCheck()
        {
            ballTest.ResetLine();
            index = 1;
            SetActiveIcon(false);
        }

        void AddPoint(Vector3 point)
        {
            if (index > ballTest.Line.positionCount - 1) return;
            SetActiveICon(true, point);
            ballTest.SetPos(index, point);
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
            if (index > ballTest.Line.positionCount - 1)
            {
                index = 1;
                return true;
            }
            else
            {
                index++;
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
                AddPoint(pos);
                return false;
            }
        }
    }
}



