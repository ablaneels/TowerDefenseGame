using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject canvas;
    public GameObject shop;
    public Node Node { get; set; }

    private void Awake() {
        if (Instance != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void CloseWeaponShopPanel()
    {
        shop.SetActive(false);
    }

    public void OpenWeaponShopPanel()
    {
        shop.SetActive(true);
    }

    public void SetNode(Node node)
    {
        Node = node;
    }
}
