using UnityEngine;
using System.Collections;

public class MusicSheet : MonoBehaviour 
{
    public float NoteSpeed;
    
    public static float Speed { get; set; }
    public static bool Enabled { get; set; }

    private static LayerMask _CollisionMask = LayerMask.NameToLayer("ShitHitRegion");
    private static Animator _Animator;
    public static LayerMask CollisionMask { get { return _CollisionMask; } }

    void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    void Start()
    {
        Enabled = false;
    }

    void Update()
    {
        Speed = NoteSpeed;
    }

    public static void Enable()
    {
        Enabled = true;
        _Animator.SetBool("Show", true);
    }
}
