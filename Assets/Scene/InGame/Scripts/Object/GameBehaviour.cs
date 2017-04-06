using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour {

    private void OnDisable()
    {
        StopAllCoroutines();
        CancelInvoke();
    }

    private Rigidbody2D _rigid2D;
    public Rigidbody2D rigid2D { get { return _rigid2D ?? (_rigid2D = GetComponent<Rigidbody2D>()); } }

    private Image _image;
    public Image image { get { return _image ?? (_image = GetComponent<Image>()); } }
}
