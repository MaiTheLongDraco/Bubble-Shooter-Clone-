using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDirectionLine : MonoBehaviour
{
    private LineManager lineManager;

    public LineManager LineManager { get => lineManager; set => lineManager = value; }

    // Start is called before the first frame update
    void Start()
    {
        lineManager = FindObjectOfType<LineManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawWithDirection();
    }
    private void DrawWithDirection()
    {
        if (GetInput.IsMousePress())
        {
            var mousePos = GetInput.GetMouseInput();
            lineManager.AddNewPointToLine(mousePos);
            LineManager.DirectionLine.SetPosForLastPoint(mousePos);
        }
    }
}
