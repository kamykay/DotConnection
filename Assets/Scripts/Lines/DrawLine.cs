using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    private Transform linePrefab;

    private LineRenderer lineRenderer;
    private InputManager input;

    private GameObject lineObj;

    void Start()
    {
        input = GameObject.FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        DrawingPhases();
        DragBetweenDots();
    }

    private void DrawingPhases()
    {
        if (input.Drawing == DrawingPhase.StartedDrawing)
        {
            lineObj = Instantiate(linePrefab.gameObject);
            lineObj.transform.parent = transform;
            lineRenderer = lineObj.GetComponent<LineRenderer>();
            input.Drawing = DrawingPhase.IsDrawing;
        }

        if (input.Drawing == DrawingPhase.IsDrawing)
        {
            lineRenderer.positionCount = 2;
            Vector3[] points = new Vector3[2];
            points[0] = input.StartPosition;
            points[1] = Vector3.MoveTowards(input.StartPosition, input.CurrentPosition, 6.5f);
            lineRenderer.SetPositions(points);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startColor = input.CurrentSelected.GetComponent<SpriteRenderer>().color;
            lineRenderer.endColor = input.CurrentSelected.GetComponent<SpriteRenderer>().color;
        }
    }

    private void DragBetweenDots()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (input.CurrentSelected != null)
                {
                    input.CurrentPosition = input.GetCurrentInputPosition();

                    if (input.Drawing == DrawingPhase.StartedDrawing)
                    {
                        input.Drawing = DrawingPhase.IsDrawing;
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (input.CurrentSelected != null)
                {
                    if (!input.CurrentSelected.GetComponent<Dot>().IsConnected)
                    {
                        DeleteLastLine();
                        input.CurrentSelected = null;
                    }
                }
            }
        }
    }

    private void DeleteLastLine()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        }
    }

    private void RenderLine(Vector3 startPosition, Vector3 endPosition)
    {
        lineObj = Instantiate(linePrefab.gameObject);
        lineObj.transform.parent = transform;
        lineRenderer = lineObj.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;


        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = input.CurrentSelected.GetComponent<SpriteRenderer>().color;
        lineRenderer.endColor = input.CurrentSelected.GetComponent<SpriteRenderer>().color;
    }

    //else if (Input.GetMouseButtonUp(0))
    //{
    //    if (input.LastHovered.GetComponent<Dot>().dotType == input.CurrentSelected.GetComponent<Dot>().dotType)
    //    {
    //        //lineRenderer.SetPosition(0, new Vector3(input.LastSelected.transform.position.x, input.LastSelected.transform.position.y, 10));
    //        //lineRenderer.SetPosition(1, new Vector3(input.CurrentSelected.transform.position.x, input.CurrentSelected.transform.position.y, 10));
    //    }
    //}

    //private void DrawLineBetweenDots()
    //{
    //    if (input.CurrentSelected != null && input.LastSelected != null)
    //    {
    //        if (input.LastSelected.GetComponent<Dot>().dotType == input.CurrentSelected.GetComponent<Dot>().dotType)
    //        {
    //            if (input.LastSelected != input.CurrentSelected)
    //            {
    //                input.LastSelected = null;
    //            }
    //        }
    //    }
    //}
}
