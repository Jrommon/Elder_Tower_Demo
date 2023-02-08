using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField, Range(0f,1f)] private float _parallaxMultiplier;

    private Transform _cameraTransform;
    private Vector3 _previousCameraPosition;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _previousCameraPosition = _cameraTransform.position;
    }

    //USamos el late update en lugar del update para garantizar que los movimientos de camara ya se han realizado
    private void LateUpdate()
    {
        // desplazamiento con multiplicador entre (rapido) 0-1 (lento)
        Vector3 translate = _cameraTransform.position - _previousCameraPosition;
        transform.position += new Vector3(translate.x * _parallaxMultiplier, translate.y, translate.z);
        _previousCameraPosition = _cameraTransform.position;
    }
}
