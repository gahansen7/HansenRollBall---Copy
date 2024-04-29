using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(offsetX, offsetY, -offsetZ);
    }


}
