using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour 
{
    private SheetLine _SheetLine;
    private Collider2D _Collider;

	// Use this for initialization
	void Start () 
    {
        _SheetLine = transform.parent.GetComponent<SheetLine>();
        _Collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (MusicSheet.Enabled)
        {
            transform.Translate(MusicSheet.Speed * -1 * Time.deltaTime, 0, 0);

            if (Input.GetKeyDown(_SheetLine.AssociatedKey))
            {
                if (_SheetLine.RegionCollider.bounds.Intersects(_Collider.bounds))
                {
                    Debug.Log("Hit");
                    Destroy(gameObject);
                }
                else
                    Debug.Log("Missed");
            }
        }
	}
}
