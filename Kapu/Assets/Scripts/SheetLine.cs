using UnityEngine;
using System.Collections;

public class SheetLine : MonoBehaviour {

    public KeyCode AssociatedKey;

    void Awake()
    {
        RegionCollider = GetComponent<BoxCollider2D>();
    }

    public BoxCollider2D RegionCollider { get; set; }
}
