using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    [SerializeField]
    private CartridgeGenerator _gen;
    private float sibal = 0f;

    private void Update()
    {
        sibal += Time.deltaTime;
        if (sibal >= 0.2f && Input.GetKey(KeyCode.Space))
        {
            sibal = 0f;
            Cartridge c = _gen.GetCartridge();
            c.gameObject.SetActive(true);
            c.transform.position = transform.position;
            c.Emission(transform.rotation.eulerAngles.z);
        }
    }
}
