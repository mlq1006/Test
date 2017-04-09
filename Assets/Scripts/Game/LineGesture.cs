using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineGesture : MonoBehaviour {

    private LineRenderer line;
    private Camera camera;
    private int indexcount;
    private List<Vector3> posList = new List<Vector3>();

    void Awake()
    {
        camera = this.transform.parent.GetComponent<Camera>();
        line = this.GetComponent<LineRenderer>();
        line.SetWidth(0.025f,0.025f);
        indexcount = 0;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
            DrawLine(pos);
        }

        if(Input.GetMouseButtonUp(0))
        {
            posList.Clear();
            indexcount = 0;
            line.SetVertexCount(0);
        }
    }

    private void DrawLine(Vector3 pos)
    {
        bool draw = true;
        if (indexcount > 0)
        {
            Vector3 lastpos = posList[indexcount - 1];
            if(Mathf.Abs(pos.x - lastpos.x) < 0.01f && Mathf.Abs(pos.y- lastpos.y) < 0.01f )
            {
                draw = false;
            }
        }

        if(draw)
        {
            line.SetVertexCount(++indexcount);
            line.SetPosition(indexcount - 1, pos);
            posList.Add(pos);
        }

    }

}
