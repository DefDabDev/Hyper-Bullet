using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;

public class BulletPool : ALSingletonComponent<BulletPool> {

    private void Awake()
    {
        _bulletParent = GameObject.Find("MainCanvas").transform;
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < _initCount; ++i)
        {
            _bulletList.Add(CreateBullet());
        }
    }

    private Bullet CreateBullet()
    {
        Bullet temp = null;
        temp = Resources.Load<Bullet>(_path);
        temp = Instantiate(temp, Vector3.zero, Quaternion.identity, _bulletParent) as Bullet;
        temp.transform.localScale = Vector3.one;
        temp.gameObject.SetActive(false);
        return temp;
    }

    public Bullet GetBullet()
    {
        for (int i = 0; i < _bulletList.Count; ++i)
        {
            if (!_bulletList[i].gameObject.activeSelf)
                return _bulletList[i];
        }

        Bullet temp = CreateBullet();
        _bulletList.Add(temp);
        return temp;
    }

    [SerializeField]
    private int _initCount = 50;

    [SerializeField]
    private string _path = string.Empty;

    private Transform _bulletParent;
    private List<Bullet> _bulletList = new List<Bullet>();
}
