using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private GameObject _player;
    private BoxCollider2D _boxCollider2D;
    private Transform _transform;
    private bool _touchingPlayer;
    private bool _solid;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find the player object with the "Player" tag
        _player = GameObject.FindGameObjectWithTag("Player");
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _transform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _touchingPlayer = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _touchingPlayer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _touchingPlayer = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _touchingPlayer = false;
        }
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        // !(_player.transform.position.y > _transform.position.y + _player.transform.localScale.y &&
        // !_touchingPlayer) || vertical < 0f;
        if (_solid && vertical < 0f)
        {
            _boxCollider2D.isTrigger = true;
            _solid = false;
        }
        else if (!_touchingPlayer && _player.transform.position.y > _transform.position.y + _player.transform.localScale.y)
        {
            _boxCollider2D.isTrigger = false;
            _solid = true;
        }
        else if (!_touchingPlayer && _player.transform.position.y < _transform.position.y - _player.transform.localScale.y)
        {
            _boxCollider2D.isTrigger = true;
            _solid = false;
        }
    }
}
