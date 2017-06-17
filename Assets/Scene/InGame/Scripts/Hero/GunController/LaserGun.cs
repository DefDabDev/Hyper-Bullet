using AL.ALUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : GunBehaviour
{
    private LineRenderer _line = null;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    protected override IEnumerator Fire(float angle)
    {
        if (_state.Equals(GUN_STATE.FIRE))
            yield break;

        _state = GUN_STATE.FIRE;

        _line.startWidth = 0.2f;
        _line.endWidth = 0.2f;

        Vector3 endPoint = Vector3.zero;
        endPoint.x = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Rad2Deg * -0.2f;
        endPoint.y = Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Rad2Deg  * 0.2f;
        endPoint.z = 0f;

        Vector3 offset = transform.position;
        Vector3 temp = Vector3.zero;
        float timer = 0f;
        while(true)
        {
            if (_line.startWidth <= 0f)
                break;

            temp = offset - transform.position;
            _line.SetPosition(0, transform.position + temp);
            _line.SetPosition(1, endPoint + temp);
            timer += Time.deltaTime;
            _line.startWidth = ALLerp.Lerp(_line.startWidth, 0f, timer);
            _line.endWidth = ALLerp.Lerp(_line.endWidth, 0f, timer);
            yield return null;
        }
        yield return new WaitForSeconds(_fireDelay);
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
        _state = GUN_STATE.SLEEP;
    }

    public override void ChangeGun()
    {
        UIManager.instance.ChangeUI("LaserGun", 1f);
        image.sprite = _playerImage;
    }
}
