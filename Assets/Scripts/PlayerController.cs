using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public static event Action<float> onZoomIn;
    public static event Action<float> onZoomOut;

    // [Header("Data")]
    // [SerializeField] private GameMenu gm = null;
    [SerializeField] private GameObject helpInfoText = null;

    [Header("Movement Variables")]
    [SerializeField] private bool DisableMovement = false;
    [SerializeField] [Range(0.001f, 1000f)] private float movementSpeed = 10f;
    // [SerializeField] [Range(0,100)] private float sprintMultiplyer = 3f;
    [SerializeField] public bool invertMovementY = true;


    [Header("Camera Variables")]
    [SerializeField] public Transform followTarget = null;
    // [SerializeField] public Camera followCamera = null;
    [Tooltip("Distance to 'observed' model.")]
    [SerializeField] public float distance = 7.5f;
    [SerializeField] public float minDistance = 0f;
    [SerializeField] public float maxDistance = 250f;
    [Tooltip("How much to Zoom In/Out for each scroll 'unit' (Affects the number of 'Zoom Levels')")]
    [SerializeField] [Range(0.001f, 250f)] public float zoomStep = 1f;
    [SerializeField] public bool invertScrollZoom = false;
    [Tooltip("Smaller values represent slower movement.")]
    [SerializeField] [Range(0f, 5000f)] public float keyboardSensitivity = 1000f;
    [Tooltip("Smaller values represent slower movement.")]
    [SerializeField][Range(0f, 500f)] public float joystickSensitivity = 100f;
    [SerializeField] public float minAngleX = -90f;
    [SerializeField] public float maxAngleX = 0f;
    [Tooltip("Rotate player object to match camera facing direction.")]
    [SerializeField] private bool playerFollowsCameraView = true;
    [SerializeField] public bool invertLookX = false;
    [SerializeField] public bool invertLookY = false;
    [Tooltip("Whether the Mouse button toggles the camera movement or if it needs to be pressed continuously while moving the camera")]
    [SerializeField] private bool toggleLook = false;    
    
    [Header("Floor Mask")]
    [SerializeField] private LayerMask floorMask = new LayerMask();
    
    [Header("Auto Set")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] AnimationControllerScript animator;

    private bool mouseOverUI = false;

    private Camera mainCamera;
    private Vector2 moveJoystick = new Vector2(0,0);
    private Vector2 viewJoystick = new Vector2(0,0);
    private float hAxis = 0f;
    private float vAxis = 0f;
    // private bool toggleSprint = false;
    private float mouseXAxis;
    private float mouseYAxis;
    private Vector2 mouseScrollDelta;
    private bool mouseLeft, mouseLeftUpdated, mouseRight, mouseRightUpdated, mouseMid, mouseMidUpdated, mouseHitBool;
    // bool centerScreenHitBool;
    private RaycastHit mouseRayHit;
    private RaycastHit centerScreenRayHit;


    private bool enableRotation = false, enableJoystickRotation = false;
    private float mouseXRot = 0f;
    private float mouseYRot = 0f;
    private Vector3 offset;
    private int InvertY
    {
        get { return invertMovementY == true ? 1 : -1; }
        set { invertMovementY = value > 0 ? true : false; }
    }

    public void SetOverUIBool(bool newValue)
    {
        mouseOverUI = newValue;
    }

    void Start()
    {
        mainCamera = Camera.main;
        animator = gameObject.GetComponent<AnimationControllerScript>();
        // agent = gameObject.GetComponent<NavMeshAgent>();
        // agent.enabled = false;
        // agent.enabled = true;
        // agent.Warp(transform.position);
 
        // if(followCamera == null)
        //     followCamera = Camera.main;
        
        onZoomIn?.Invoke(distance);
    }

    // Update is called once per frame
    void Update()
    {
        // ProcessMouseLeft();
        GetInputs();
        ProcessRotation();
        ProcessZoom(mouseScrollDelta);
        if(!DisableMovement) {
            ProcessMouseLeft();
            ProcessMovement();
        }

    }

    private void GetInputs() {
        // if(Input.GetKeyDown(sprintKey)) {
        //     toggleSprint = true;
        // } else {
        //     toggleSprint = false;
        // }
        if(moveJoystick.x != 0 || moveJoystick.y != 0) {
            hAxis = moveJoystick.x; // moveJoystick.position.normalized.x - 1; // GetMaxValue(moveJoystick.position.x, 1);
            vAxis = moveJoystick.y; // moveJoystick.position.normalized.y; // GetMaxValue(moveJoystick.position.y, 1);
        } else {
            hAxis = -Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");
        }

        if(viewJoystick.x != 0 || viewJoystick.y != 0) {
            mouseXAxis = viewJoystick.x;
            mouseYAxis = viewJoystick.y;
        } else {
            mouseXAxis = Input.GetAxis("Mouse X");
            mouseYAxis = Input.GetAxis("Mouse Y");
        }

        mouseScrollDelta = Input.mouseScrollDelta;
        mouseLeft = Input.GetMouseButtonDown(0);
        mouseLeftUpdated = Input.GetMouseButton(0);

        mouseRight = Input.GetMouseButtonDown(1);
        mouseRightUpdated = Input.GetMouseButton(1);

        mouseMid = Input.GetMouseButtonDown(2);
        mouseMidUpdated = Input.GetMouseButton(2);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit,  Mathf.Infinity)) {
        //if(Physics.Raycast(ray, out RaycastHit hit,  Mathf.Infinity, floorMask)) {
            mouseHitBool = true;
            mouseRayHit = hit;
        } else {
            mouseHitBool = false;
            mouseRayHit = hit;
        }

        // ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // if(Physics.Raycast(ray, out RaycastHit screenHit,  Mathf.Infinity)) {
        // //if(Physics.Raycast(ray, out RaycastHit screenHit,  Mathf.Infinity, floorMask)) {
        //     centerScreenHitBool = true;
        //     centerScreenRayHit = screenHit;
        // } else {
        //     centerScreenHitBool = false;
        //     centerScreenRayHit = screenHit;
        // }
    }

    public float GetMaxValue(float value, float maximumValue) {
        if(value > maximumValue)
            return maximumValue;
        if(value < -maximumValue)
            return -maximumValue;
        return value;
    }

    public RaycastHit GetHit() {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(mouseXAxis, mouseYAxis, 0));
        mouseHitBool = Physics.Raycast(ray, out RaycastHit hit,  Mathf.Infinity, floorMask);
        return hit;
    }

    private void ProcessMouseLeft()
    {
        if(mouseLeftUpdated) {
        //if(mouseLeftUpdated) {
            // RaycastHit hit; // GetHit();
            
            // if (hit.point != null) {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), 
                                out RaycastHit hit, Mathf.Infinity, floorMask)) {
                agent.destination = hit.point;
            }
        //     if(demolishMode && objectUnderMouse) {
        //         Destroy(objectUnderMouse.gameObject);
        //     }
        }
    }

    private void ProcessRotation()
    {
        if (toggleLook)
        {
            if (mouseRight) /* if (Input.GetMouseButtonDown(1)) */
            {
                enableRotation = !enableRotation;
            }
        }
        else
        {
            if(!mouseOverUI) {
                if (mouseLeftUpdated || mouseRightUpdated) /* if (Input.GetMouseButton(1)) */
                //if (mouseRightUpdated) /* if (Input.GetMouseButton(1)) */
                    enableRotation = true;
                else
                    enableRotation = false;
            }
        }

        OrbitPlayer();
    }

    private float GetAgentSpeedPercent() {
        return agent.velocity.magnitude/agent.speed;
    }

    private void ProcessMovementAnimations() {
        // Debug.Log($"speed: {agent.velocity.magnitude}   |   s2: {100 * agent.velocity.magnitude/agent.speed}%");
        float speed = GetAgentSpeedPercent();
        if(speed == 0) {
            animator.PlayAnimation("idle");
        } else if (speed < .5) {
            animator.PlayAnimation("walk");
        } else {
            animator.PlayAnimation("run");
        }
    }
    private void ProcessMovement()
    {
        ProcessMovementAnimations();
        followTarget.Translate(InvertY * hAxis * movementSpeed * Time.deltaTime, 0f, vAxis * movementSpeed * Time.deltaTime);
    }
    private void ProcessOtherKeys()
    {
        /* TODO - add other key processings here :) */
    }
    private void ProcessZoom(Vector2 inputMouseScrollDelta)
    {
        if (inputMouseScrollDelta == Vector2.up)
            if (invertScrollZoom) ZoomOut();
            else ZoomIn();
        if (inputMouseScrollDelta == Vector2.down)
            if (invertScrollZoom) ZoomIn();
            else ZoomOut();
    }
    void OrbitPlayer()
    {
        float mX, mY, sen;
        NewMethod(out mX, out mY, out sen);
        if (enableRotation || enableJoystickRotation)
        {
            mouseXRot += (invertLookX ? -1 : 1) * mY * sen * Time.deltaTime;
            mouseYRot += (invertLookY ? -1 : 1) * mX * sen * Time.deltaTime;
            if (enableRotation)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (mouseXRot > maxAngleX)
        {
            mouseXRot = maxAngleX;
        }
        else if (mouseXRot < minAngleX)
        {
            mouseXRot = minAngleX;
        }

        // followCamera.transform.position = followTarget.position + Quaternion.Euler(mouseXRot, mouseYRot, 0f) * (distance * -Vector3.back);
        // followCamera.transform.LookAt(followTarget.position, Vector3.up);
        mainCamera.transform.position = followTarget.position + Quaternion.Euler(mouseXRot, mouseYRot, 0f) * (distance * -Vector3.back);
        mainCamera.transform.LookAt(followTarget.position, Vector3.up);

        if (playerFollowsCameraView)
            followTarget.transform.localEulerAngles = new Vector3(followTarget.transform.localEulerAngles.x,
                                                                  /*   followCamera.transform.localEulerAngles.y, */
                                                                  mainCamera.transform.localEulerAngles.y,
                                                                  followTarget.transform.localEulerAngles.z);
    }

    private void NewMethod(out float mX, out float mY, out float sen)
    {

        // if(viewJoystickHandle != null && viewJoystickHandle.GetComponent<FloatingJoystick>().ViewHeld) {
        //     mX = viewJoystick.normalized.x;
        //     mY = viewJoystick.normalized.y;
        //     sen = joystickSensitivity;
        //     enableJoystickRotation = true;
        // } else {
        mX = mouseXAxis;
        mY = mouseYAxis;
        sen = keyboardSensitivity;
        enableJoystickRotation = false;
        // }
        // Debug.Log($"mX: {mX} | mY: {mY}");
    }

    // called from UI
    public void ZoomIn()
    {
        if(mainCamera.orthographic) {
            mainCamera.orthographicSize -= zoomStep * 0.25f;
            if (mainCamera.orthographicSize < 0.1f) {
                mainCamera.orthographicSize = 0.1f;
            }

            onZoomIn?.Invoke(mainCamera.orthographicSize);
        } else {
            if (distance == minDistance)
                return;
            if (distance - zoomStep < minDistance)
            {
                distance = minDistance;
                return;
            }
            distance -= zoomStep;

            onZoomIn?.Invoke(distance);
        }
    }
    // called from UI
    public void ZoomOut()
    {
        if(mainCamera.orthographic) {
            mainCamera.orthographicSize += zoomStep * 0.25f;
            if (mainCamera.orthographicSize > 100f) {
                mainCamera.orthographicSize = 100f;
            }

            onZoomOut?.Invoke(distance);
        } 
        else {
            if (distance == maxDistance)
                return;
            if (distance + zoomStep > maxDistance)
            {
                distance = maxDistance;
                return;
            }
            distance += zoomStep;

            onZoomOut?.Invoke(distance);
        }
    }

    public void ToggleShowHelp() {
        helpInfoText.SetActive(!helpInfoText.activeSelf);
    }
}
