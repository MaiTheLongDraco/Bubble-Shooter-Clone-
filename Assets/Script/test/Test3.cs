using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField] private Transform mainPos;
    [SerializeField] private Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        FollowMouseDir();
        End();
    }
    private void FollowMouseDir()
    {
        if (!Input.GetMouseButton(0)) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var direction = mousePos - mainPos.position;
        lineHandle.ResetLine();
        HitHandlle(mainPos.position, direction);
    }
    private void HitHandlle(Vector2 orrigin, Vector2 dirrection)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(orrigin, dirrection);
        if (hit2D)
        {
            Debug.Log("hit ");
            DrawLine(orrigin, hit2D.point);
            var newDir = Vector2.Reflect(dirrection, hit2D.normal);
            if (hit2D.collider.tag == "DownLimit") return;
            HitHandlle(hit2D.point - distance / hit2D.point, newDir);
        }
    }
    [SerializeField] private LineHandle lineHandle;
    private void DrawLine(Vector2 origin, Vector2 next)
    {
        // Debug.DrawLine(orrigin, next);
        lineHandle.AddNewPoint(origin, next);
    }
    private void End()
    {
        if (Input.GetMouseButtonUp(0))
        {
            lineHandle.ResetLine();
        }
    }
}
