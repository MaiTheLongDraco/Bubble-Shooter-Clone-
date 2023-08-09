namespace Test
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BallTest : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;

        public LineRenderer Line { get => line; set => line = value; }

        // Start is called before the first frame update
        void Start()
        {
            line.positionCount = 5;
            line.SetPosition(0, transform.position);
        }
        public void SetPos(int index, Vector2 pos)
        {
            line.SetPosition(index, pos);
        }
        public void ResetLine()
        {
            line.positionCount = 0;
        }
        public void DeclareLine()
        {
            line.positionCount = 6;
        }
        public List<Vector2> GetPos()
        {
            List<Vector2> pos = new List<Vector2>();
            for (int i = 0; i < line.positionCount; i++)
            {
                pos.Add(line.GetPosition(i));
            }
            return pos;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}

