using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour {

    [SerializeField]
    private Text _text;

    [SerializeField]
    private float _delay;

    private void Awake()
    {
        StartCoroutine("Animation");
    }

    private IEnumerator Animation()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=200>:D</size>");
        _text.text = builder.ToString();
        yield return new WaitForSeconds(_delay);
        builder.Append("<size=200>:D</size>");
        _text.text = builder.ToString();
        yield return new WaitForSeconds(_delay);
        builder.Append("<size=200>:D</size>");
        _text.text = builder.ToString();
        yield return new WaitForSeconds(_delay);
        builder.AppendLine();
        builder.Append("<size=100>Copyright(c)2017 Team :D:D:D All rights reserved.</size>");
        _text.text = builder.ToString();
        yield return new WaitForSeconds(_delay * 2);
        Blind.instance.ReplaceScene("Main");
    }
}
