using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    [SerializeField]
    private float _outOfRange = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 mouse = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            
        }

        //transform.position = Vector3.MoveTowards(transform.position, ray.origin, _speed * Time.deltaTime);


        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        Physics.Raycast(ray2, out hit2);
     
        transform.Translate(ray2.origin * _speed * Time.deltaTime);
   


        Destroy(this.gameObject, _outOfRange);
  
    }
}
