using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Trapezoid : BaseMeshEffect
{
    [Range(0, 1)]
    public float TopScale = 1f;
    [Range(0, 1)]
    public float BottomScale = 1f;

    [Range(1, 10)]
    public int HorizontalTesselationFactor = 1;

    public override void ModifyMesh(VertexHelper verts_h) //List<UIVertex>
    {
        if (!IsActive())
            return;

        //hack ? 
        List<UIVertex> verts = new List<UIVertex>();
        verts_h.GetUIVertexStream(verts);


        tesselate(verts, HorizontalTesselationFactor);

        for (int index = 0; index < verts.Count; index++)
        {
            var uiVertex = verts[index];
            Debug.Log(uiVertex.position);

            if (uiVertex.position.y > 0)
            {
                uiVertex.position = new Vector3(uiVertex.position.x * TopScale, uiVertex.position.y, uiVertex.position.z);
            }
            else
            {
                uiVertex.position = new Vector3(uiVertex.position.x * BottomScale, uiVertex.position.y, uiVertex.position.z);
            }

            verts[index] = uiVertex;
        }
    }

    void tesselate(List<UIVertex> verts, int factor)
    {
        if (factor < 2)
        {
            return;
        }
        List<UIVertex> newVerts = new List<UIVertex>();

        for (int i = 0; i <= factor; i++)
        {
            float uvx = (float)i / factor;
            float uvx2 = (float)(i + 1) / factor;
            UIVertex bottomYV;
            UIVertex topYV;

            newVerts.Add(lerpVertex(verts[0], verts[3], uvx));
            newVerts.Add(lerpVertex(verts[1], verts[2], uvx));
            newVerts.Add(lerpVertex(verts[1], verts[2], uvx2));
            newVerts.Add(lerpVertex(verts[0], verts[3], uvx2));

        }
        verts.Clear();
        verts.AddRange(newVerts);
    }

    UIVertex lerpVertex(UIVertex from, UIVertex to, float t)
    {
        UIVertex ret = new UIVertex();
        ret.position = Vector3.Lerp(from.position, to.position, t);
        ret.normal = Vector3.Lerp(from.normal, to.normal, t);
        ret.color = Color32.Lerp(from.color, to.color, t);
        ret.tangent = Vector4.Lerp(from.tangent, to.tangent, t);
        ret.uv0 = Vector2.Lerp(from.uv0, to.uv0, t);
        ret.uv1 = Vector2.Lerp(from.uv1, to.uv1, t);
        return ret;
    }
}