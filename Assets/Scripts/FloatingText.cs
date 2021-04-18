using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public GameObject floatingTextGameObject;
    public TextMesh floatingTextTextMesh;
    void Start()
    {
        HideFloatingText();
    }

    
    public void ShowFloatingtext(string floatingTextContent)
    {
        this.gameObject.SetActive(true);
        floatingTextTextMesh.text = floatingTextContent;
    }

    public void HideFloatingText()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.rotation.z * (-1.0f));
    }
}
