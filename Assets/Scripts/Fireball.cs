using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    [SerializeField]
    private float _outOfRange = 2.0f;

    private void Start()
    {
        //target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        

        /*
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        transform.Translate(worldPosition * _speed * Time.deltaTime);
        */

        /*
        Vector3 MyScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, MyScreenPos.z));
        

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        */
        

        Destroy(this.gameObject, _outOfRange);
  
    }
}
