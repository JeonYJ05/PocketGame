using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField] float _speed;

    private MeshRenderer _render;
    private float _offset;

    private void Start()
    {
        _render = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        _offset += Time.deltaTime * _speed;
        _render.material.mainTextureOffset = new Vector2(_offset, 0);
    }
}
