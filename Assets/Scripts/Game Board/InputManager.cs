using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DrawingPhase
{
    StartedDrawing,
    IsDrawing,
    StoppedDrawing,
    NotDrawing
}

public class InputManager : MonoBehaviour
{
    [Header("Event System")]
    private EventSystem eventSystem;

    [Header("Selected Dots")]
    public GameObject currentSelected;
    public GameObject lastSelected;

    [Header("Input Position")]
    private Vector2 startPosition;
    private Vector2 currentPosition;

    private DrawingPhase drawing;

    [Header("Layer Mask")]
    [SerializeField]
    private LayerMask layerMask;

    #region Properties
    public EventSystem EventSystem
    {
        get { return eventSystem; }
    }

    public GameObject CurrentSelected
    {
        get { return currentSelected; }
        set { currentSelected = value; }
    }

    public GameObject LastSelected
    {
        get { return lastSelected; }
        set { lastSelected = value; }
    }

    public Vector2 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    public Vector2 CurrentPosition
    {
        get { return currentPosition; }
        set { currentPosition = value; }
    }

    public DrawingPhase Drawing
    {
        get { return drawing; }
        set { drawing = value; }
    }

    #endregion

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();

        drawing = DrawingPhase.NotDrawing;
    }

    private void Update()
    {
        GetTouchedObject();
    }

    private void GetTouchedObject()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(GetCurrentInputPosition(), Camera.main.transform.forward, layerMask);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (hit.collider != null && hit.collider.GetComponent<Dot>())
                {
                    if (!hit.collider.GetComponent<Dot>().IsConnected)
                    {
                        if (hit.collider.gameObject != currentSelected)
                        {
                            drawing = DrawingPhase.StartedDrawing;
                            currentSelected = hit.collider.gameObject;
                            startPosition = GetCurrentSelectedPosition();
                            currentPosition = GetCurrentSelectedPosition();
                        }
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (hit.collider != null && currentSelected != null)
                {
                    if (hit.collider.GetComponent<Dot>().dotType == currentSelected.GetComponent<Dot>().dotType)
                    {
                        if (!hit.collider.GetComponent<Dot>().IsConnected)
                        {
                            if (hit.collider.gameObject != currentSelected)
                            {
                                currentSelected.GetComponent<Dot>().IsConnected = true;
                                lastSelected = currentSelected;
                                currentSelected = hit.collider.gameObject;
                                startPosition = GetCurrentSelectedPosition();
                                drawing = DrawingPhase.StartedDrawing;
                            }
                        }
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ClearInputs();
            }
        }
    }
    public Vector2 GetCurrentInputPosition()
    {
        Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

        return touchPosWorld2D;
    }

    public void ClearInputs()
    {
        lastSelected = null;
        startPosition = new Vector2(0, 0);
        currentPosition = new Vector2(0, 0);
        drawing = DrawingPhase.StoppedDrawing;
    }

    private Vector2 GetCurrentSelectedPosition()
    {
        return new Vector2(currentSelected.transform.position.x, currentSelected.transform.position.y);
    }
}
