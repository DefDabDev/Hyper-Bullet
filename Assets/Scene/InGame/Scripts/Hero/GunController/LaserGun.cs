using AL.ALUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : GunBehaviour
{
    private LineRenderer _line = null;
    private const float correction = 90f * Mathf.Deg2Rad;

    private void Awake()
    {
        _realMagazine = _magazineSize;
        _line = GetComponent<LineRenderer>();
    }

    protected override IEnumerator Fire(float angle)
    {
        if (_state.Equals(GUN_STATE.FIRE))
            yield break;

        _state = GUN_STATE.FIRE;

        _line.startWidth = 0.2f;
        _line.endWidth = 0.2f;

        float radianAngle = (angle + 90) * Mathf.Deg2Rad;
        Vector3 endPoint = Vector3.zero;
        endPoint.x = Mathf.Cos(radianAngle) * 16.5f;
        endPoint.y = Mathf.Sin(radianAngle) * 16.5f;
        endPoint.z = 0f;

        endPoint += transform.parent.position;
        float timer = 0f;
        while (true)
        {
            if (_line.startWidth >= 0.2f)
            {
                HitCheck(endPoint.normalized);
                Cartridge c = cg.GetCartridge();
                c.gameObject.SetActive(true);
                c.transform.localPosition = transform.parent.localPosition;
                c.Emission(angle);
                CameraShaker.instance.Shake();
            }

            if (timer >= 1f)
                break;

            _line.SetPosition(0, transform.parent.position);
            _line.SetPosition(1, endPoint);
            timer += Time.deltaTime;
            _line.startWidth = ALLerp.Lerp(_line.startWidth, 0f, timer);
            _line.endWidth = ALLerp.Lerp(_line.endWidth, 0f, timer);
            yield return null;
        }
        --_realMagazine;
        if (_realMagazine < 0)
        {
            UIManager.instance.Reload(_fireDelay);
            _realMagazine = _magazineSize;
            yield return new WaitForSeconds(_fireDelay);
        }
        else
            UIManager.instance.DecreaseGauge(_realMagazine, _fireDelay);
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
        _state = GUN_STATE.SLEEP;
    }

    public override void ChangeGun()
    {
        UIManager.instance.ChangeUI("LaserGun", _magazineSize);
        image.sprite = _playerImage;
        _realMagazine = _magazineSize;
        UIManager.instance.Reload(_fireDelay);
    }

    public void HitCheck(Vector3 direction)
    {
        RaycastHit2D[] hits = null;
        hits = Physics2D.RaycastAll(transform.parent.position, direction);

        for (int i = 0; i < hits.Length; ++i)
        {
            if (!hits[i].transform.CompareTag("Player") && !hits[i].transform.CompareTag("Edge"))
            {
                hits[i].transform.SendMessage("receiveDMG", (uint)_damage);
                ParticleSystem effect = EffectPool.instance.GetEffect();
                effect.transform.position = hits[i].transform.position;
                effect.Play();
            }
        }
    }
}
