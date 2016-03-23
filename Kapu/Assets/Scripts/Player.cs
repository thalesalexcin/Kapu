using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour 
{
    public Collider2D Collider { get; set; }

    public float JumpHeight = 2;
    public float TimeToJumpApex = .4f;
    public float MoveSpeed = 1;

    private float _JumpVelocity;
    private float _Gravity;
    private Vector2 _Velocity;
    private Controller2D _Controller;

    void Awake()
    {
        _Controller = GetComponent<Controller2D>();
        Collider = GetComponent<Collider2D>();
    }

	// Use this for initialization
	void Start () 
    {
        _Gravity = -(2 * JumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
        _JumpVelocity = Mathf.Abs(_Gravity) * TimeToJumpApex;
	}
	
	// Update is called once per frame
	void Update () 
    {
        _ResetVelocities();
        
        _AddRunningVelocity();
        _AddJumpForce();
        _AddGravityForce();

        if(!MusicSheet.Enabled)
            _Controller.Move(_Velocity * Time.deltaTime);
	}

    private void _AddRunningVelocity()
    {
        _Velocity.x = MoveSpeed;
    }

    private void _AddGravityForce()
    {
        _Velocity.y += _Gravity * Time.deltaTime;
    }

    private void _AddJumpForce()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _Controller.Collisions.Below)
            _Velocity.y = _JumpVelocity;
    }

    private void _ResetVelocities()
    {
        if (_Controller.Collisions.Above)
            _Velocity.y = Mathf.Min(0, _Velocity.y);

        if (_Controller.Collisions.Below)
            _Velocity.y = Mathf.Max(0, _Velocity.y);
    }
}
