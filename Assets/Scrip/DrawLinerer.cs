using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Terrain;


public class DestroyEventHandler : MonoBehaviour
{
    public delegate void OnDestroyDelegate(GameObject destroyedObject);
    public event OnDestroyDelegate OnDestroyed;
    public static int index;
    bool isdrawline00;
    bool isdrawline01;
    
    private void OnDestroy()
    {
        if (OnDestroyed != null)
        {
            OnDestroyed(gameObject);
            index = transform.GetSiblingIndex();
        }
    }
}
public class DrawLinerer : MonoBehaviour
{
    public List<GameObject> Nodes;
    public GameObject Player;
    public LineRenderer LineRenderer;
    public LineRenderer lineRenderer00;
    public LineRenderer lineRenderer01;
    public bool drawLinererFirst;
    public bool drawLinererLast;
    private int objectIndex;
    public Material MaterialRope;
    public Material newMat2;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        drawLinererLast = false;
        drawLinererFirst = false;
        LineRenderer = GetComponent<LineRenderer>();
        lineRenderer00 = gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        lineRenderer01 = gameObject.transform.GetChild(0).GetComponent<LineRenderer>();
        addNode();
       

    }
    void Update()
    {
        RenderLine();
        drawNewLinerer();
    }
    void addNode()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            gameObject.transform.GetChild(i).gameObject.AddComponent<DestroyEventHandler>().OnDestroyed += HandleObjectDestroyed;

            Nodes.Add(gameObject.transform.GetChild(i).gameObject);
        }
        Nodes[Nodes.Count - 1].AddComponent<HingeJoint2D>().connectedBody = Player.GetComponent<Rigidbody2D>();
    }
    void RenderLine()
    {
        if (!drawLinererFirst)
        {
            LineRenderer.SetVertexCount(Nodes.Count + 1);
            int i;
            for (i = 0; i < Nodes.Count; i++)
            {
                LineRenderer.SetPosition(i, Nodes[i].transform.position);
            }
            LineRenderer.SetPosition(i, Player.transform.position);
        }
    }
    private void HandleObjectDestroyed(GameObject destroyedObject)
    {
        objectIndex = Nodes.IndexOf(destroyedObject);
        drawLinererFirst = true;
        drawLinererLast = true;
        newMat2 = new Material(MaterialRope);
        LineRenderer.material   = newMat2;
        lineRenderer00.material = newMat2;
        for (int i =0; i< Nodes.Count; i++)
        {
            Nodes[i].GetComponent<Rigidbody2D>().gravityScale = 1;
            Nodes[i].GetComponent<Rigidbody2D>().drag = 0.5f;
            Nodes[i].GetComponent<Rigidbody2D>().angularDrag = 0.5f;
        }
    }
    void drawNewLinerer()
    {
        if (drawLinererLast == true)
        {
            int i =0;
            int a = 0;
            LineRenderer.SetVertexCount(objectIndex);
            lineRenderer00.SetVertexCount(Nodes.Count - objectIndex);
            for ( i = 0; i < objectIndex; i++)
            {
                LineRenderer.SetPosition(i, Nodes[i].transform.position);
                if (Nodes[i] == null)
                {
                    return;
                }
            }
            for (int b = objectIndex+1; b < Nodes.Count; b++)
            {
                Debug.Log(Nodes[b].transform.position);
                lineRenderer00.SetPosition(a, Nodes[b].transform.position);
                a++;
            }
            lineRenderer01.SetPosition(a, Player.transform.position);
           // Invoke("deleteLinerer", 0.5f);
        }
    }
    //void deleteLinerer()
    //{
    //    Color marterial = newMat2.color;
    //    while (marterial.a > 0)
    //    {
    //        marterial.a -= 1;
    //        newMat2.color = marterial;
    //    }
    //}
} 
