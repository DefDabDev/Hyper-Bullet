using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Hero.Hero._hero.transform.position + new Vector3(0, 0, -50), 80 * Time.deltaTime);
    }
}
