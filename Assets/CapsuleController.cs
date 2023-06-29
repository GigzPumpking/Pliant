using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 5f;
    private Vector3 _input;

    void Update() {
        GatherInput();
    }

    void FixedUpdate() {
        Move();
    }

    void GatherInput() {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Move() {
        _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }
}
