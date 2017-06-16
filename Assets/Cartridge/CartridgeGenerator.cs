using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeGenerator : MonoBehaviour {

    [SerializeField]
    private Cartridge _cartridge;

    [SerializeField]
    private int _count;

    private List<Cartridge> _cartridges = new List<Cartridge>();
    private Transform _parent = null;

	private void Start ()
    {
        _parent = FindObjectOfType<Canvas>().transform;
        for (int i = 0; i < _count; ++i)
        {
            Cartridge cartridge = Instantiate(_cartridge, _parent);
            cartridge.gameObject.SetActive(false);
            _cartridges.Add(cartridge);
        }
    }

    public Cartridge GetCartridge()
    {
        for (int i = 0; i < _cartridges.Count; ++i)
        {
            if (!_cartridges[i].gameObject.activeSelf)
                return _cartridges[i];
        }

        Cartridge cartridge = Instantiate(_cartridge, _parent);
        cartridge.gameObject.SetActive(false);
        _cartridges.Add(cartridge);
        return cartridge;
    }
}
