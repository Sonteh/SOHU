using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    [SerializeField]
    private float _outOfRange = 2.0f;

    private Vector3 _mouse;

    private void Start()
    {
        _mouse = Input.mousePosition;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mouse);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);      

        Destroy(this.gameObject, _outOfRange);
  
    }
}
