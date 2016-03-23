using UnityEngine;
using System.Collections;

public class BossZone : MonoBehaviour 
{    
    private Player _Player;
    private Collider2D _Collider;
    private bool _Activated;

    void Awake()
    {
        _Collider = GetComponent<Collider2D>();
    }

	// Use this for initialization
	void Start () 
    {
        _Activated = false;
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!_Activated && _Player.Collider.bounds.Intersects(_Collider.bounds))
        {
            _Activated = true;
            MusicSheet.Enable();
        }
	}
}
