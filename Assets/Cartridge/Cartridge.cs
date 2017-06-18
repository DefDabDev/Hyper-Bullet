using AL.ALUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cartridge : MonoBehaviour
{

    [SerializeField]
    private float _force = 15f;

    [SerializeField]
    private float _rotationForce = 10f;

    [SerializeField]
    private float _friction = 3f;

    [SerializeField]
    private float _randomDistance = 10;

    [SerializeField]
    private float _emissionDistance = 3f;

    [SerializeField]
    private Vector3 _moveVector = Vector3.zero;

    public void Emission(float angle)
    {
        angle = Random.Range(angle - _randomDistance, angle + _randomDistance);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        _moveVector = transform.up * -1;
        _moveVector = _moveVector.normalized;
        StartCoroutine("Mover");
    }

    private IEnumerator Mover()
    {
        float force = Random.Range(_force, _force + _emissionDistance);
        float friction = _friction;
        while (true)
        {
            transform.localPosition += _moveVector * force;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (transform.rotation.eulerAngles.z + _rotationForce));
            force -= friction;
            friction += Time.deltaTime;
            if ((_force * 0.5f) > force)
            {
                float timer = 0f;
                while (true)
                {
                    if (force <= 0f)
                        break;

                    timer += Time.deltaTime;
                    force = ALLerp.Lerp(force, 0f, timer);
                    transform.localPosition += _moveVector * force;
                    yield return null;
                }
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("ScaleDown");
        gameObject.SetActive(false);
    }

    private IEnumerator ScaleDown()
    {
        float timer = 0f;
        while (true)
        {
            if (transform.localScale.Equals(Vector3.zero))
                break;

            timer += Time.deltaTime;
            transform.localScale = ALLerp.Lerp(transform.localScale, Vector3.zero, timer);
            yield return null;
        }
    }
}