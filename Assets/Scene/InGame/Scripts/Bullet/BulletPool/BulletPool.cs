using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;
using System;

public class BulletPool : ALComponentSingleton<BulletPool> {

    [SerializeField]
    private int _initCount = 50;

    [SerializeField]
    private string _path = string.Empty;

    [SerializeField]
    private string _relfectionPath = string.Empty;

    private Transform _bulletParent;
    private List<BulletBehaviour> _bulletList = new List<BulletBehaviour>();

    private void Awake()
    {
        _bulletParent = GameObject.Find("MainCanvas").transform;
        InitPool();
    }

    private void InitPool()
    {
        bool temp = false;
        string tempPath = string.Empty;
        for (int i = 0; i < _initCount; ++i)
        {
            if (temp)
                tempPath = _path;
            else
                tempPath = _relfectionPath;
            temp = !temp;
            _bulletList.Add(CreateBullet(tempPath));
        }
    }

    private BulletBehaviour CreateBullet(string path)
    {
        BulletBehaviour temp = null;
        temp = Resources.Load<BulletBehaviour>(path);
        temp = Instantiate(temp, Vector3.zero, Quaternion.identity, _bulletParent);
        temp.transform.localScale = Vector3.one;
        temp.gameObject.SetActive(false);
        return temp;
    }

    public BulletBehaviour GetBullet()
    {
        for (int i = 0; i < _bulletList.Count; ++i)
        {
            if (!_bulletList[i].gameObject.activeSelf && _bulletList[i].GetType().Name.Equals("Bullet"))
                return _bulletList[i];
        }

        BulletBehaviour temp = CreateBullet(_path);
        _bulletList.Add(temp);
        return temp;
    }

    public BulletBehaviour GetRelfectionBullet()
    {
        for (int i = 0; i < _bulletList.Count; ++i)
        {
            if (!_bulletList[i].gameObject.activeSelf && _bulletList[i].GetType().Name.Equals("ReflectionBullet"))
                return _bulletList[i];
        }

        BulletBehaviour temp = CreateBullet(_relfectionPath);
        _bulletList.Add(temp);
        return temp;
    }
}
