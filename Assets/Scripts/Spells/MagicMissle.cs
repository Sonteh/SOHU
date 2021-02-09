using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private float _outOfRange = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 target = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        Destroy(this.gameObject, _outOfRange);
    }
}
