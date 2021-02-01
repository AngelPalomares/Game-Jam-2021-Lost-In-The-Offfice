using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")] public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelTimeFlying = .4f;
    public float accelTimeAir = .2f;
    public float accelTimeGround = .1f;
    public float moveSpeed = 6;
    public float moveSpeedFlying = 16;

    public Vector2 maxSpeed;

    private float _gravity;
    private float _jumpVelocity;

    public Vector2 velocity;
    public float velocityXSmoothing;
    public float velocityYSmoothing;

    private Controller2D _controller;
    [SerializeField] private Vector2 input;

    private float _targetVelocityX;

    private float _targetVelocityFlyingX;
    private float _targetVelocityFlyingY;

    private Quaternion _oldRotation;

    [Header("Camera")] public Camera cam;
    public float camSpeed = .6f;
    public Vector3 camRotationOffset;


    private PlayerInput _playerInput;


    public Transform ChildVisible;

    [Header("Check Point")] [SerializeField]
    private Transform currentCheckpoint;
    
    
    #if UNITY_EDITOR
    public GameObject[] Spawns;
    public int SpawnNumber;
    #endif

    private void Awake()
    {
        _controller = GetComponent<Controller2D>();
    }



    private void Start()
    {
        _gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity * timeToJumpApex);
        _playerInput = new PlayerInput();
        cam = Camera.main;
        _playerInput = InputManager.PlayerInput;

        
        #if UNITY_EDITOR
        Spawns = GameObject.FindGameObjectsWithTag(UnityTags.CHECK_POINT);
        SpawnNumber = 0;
        #endif
    }

    private void FixedUpdate()
    {


        //Update Settings When Changed In Scene Editor
        _gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity * timeToJumpApex);

        UpdatePhysics();

        UpdateCamera();
        
        
        
        
        #if UNITY_EDITOR
        if (_playerInput.ToggleMiniMapJustPressed)
            Respawn();

        if (_playerInput.ToggleBlockWindowJustPressed)
        {
            SpawnNumber = Mathf.Clamp(SpawnNumber, 0, Spawns.Length - 1);
            currentCheckpoint = Spawns[SpawnNumber++].transform;
            Respawn();
        }
        #endif
    }

    private void UpdatePhysics()
    {

        input = _playerInput.Movement;

        if (_playerInput.JumpJustPressed && _controller.collisions.Below)
        {
            velocity.y = _jumpVelocity;
            AudioManager.instance.PlaySFX(2);
        }

        _targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, _targetVelocityX, ref velocityXSmoothing,
            (_controller.collisions.Below) ? accelTimeGround : accelTimeAir);
        velocity.y += _gravity * Time.fixedDeltaTime;


        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed.x, maxSpeed.x);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed.y, maxSpeed.y);

        _controller.Move(velocity * Time.fixedDeltaTime);

        if (_controller.collisions.Above || _controller.collisions.Below)
        {
            velocity.y = 0;
        }

        if (Mathf.Abs(velocity.x) < 0.01)
            velocity.x = 0;


        if ((_controller.collisions.Left && velocity.x < 0) || (_controller.collisions.Right && velocity.x > 0))
        {
            velocity.x = 0;
            velocityXSmoothing = 0;
        }

        if (_playerInput.Movement.x < 0)
            _scaleX = -1;
        if (_playerInput.Movement.x > 0)
            _scaleX = 1;
        ChildVisible.localScale = new Vector3(_scaleX, 1, 1);
    }

    private int _scaleX = 1;


    private void UpdateCamera()
    {
        var disX = transform.position.x - cam.transform.position.x;
        disX *= camSpeed;
        var disY = transform.position.y - cam.transform.position.y;
        disY *= camSpeed;
        cam.transform.position = new Vector3(cam.transform.position.x + disX, cam.transform.position.y + disY, -10);
        var rot = transform.rotation;
        cam.transform.rotation = Quaternion.Euler(camRotationOffset.x + rot.eulerAngles.x, camRotationOffset.y + rot.eulerAngles.y, camRotationOffset.z + rot.eulerAngles.y);
    }

    private void Respawn()
    {
        Transform trans = null;
        if (currentCheckpoint != null)
            trans = currentCheckpoint;
        else 
            trans = GameController.Spawn;
        
        
        transform.position = trans.position;
        transform.rotation = trans.rotation;
    }

    private void SetCheckPoint(Transform trans)
    {
        currentCheckpoint = trans;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(UnityTags.CHECK_POINT))
        {
            SetCheckPoint(other.transform);
        }
        else if (other.CompareTag(UnityTags.HAZARD))
        {
            Respawn();
        }
    }
}