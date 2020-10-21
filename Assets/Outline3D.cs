using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Outline3D : MonoBehaviour
{

    [SerializeField]
    float _outlineThickness = .3f;

    [SerializeField]
    float _fadeTime = .3f;

    [SerializeField]
    Material _outlineMat;

    [SerializeField]
    GameObject _outlineTarget;


    GameObject _outlineObject;

    bool _outlineActive = false;


    // Start is called before the first frame update
    void Start()
    {

        _outlineObject = new GameObject(name + "OutlineObject");
        _outlineObject.SetActive(false);
        _outlineObject.transform.position = _outlineTarget.transform.position;
        _outlineObject.transform.rotation = _outlineTarget.transform.rotation;
        _outlineObject.transform.localScale *= 1 + Mathf.Pow(_outlineThickness, 3f);

        Mesh _outlineMesh = _outlineTarget.GetComponent<MeshFilter>().mesh;

        _outlineObject.AddComponent<MeshFilter>().sharedMesh = _outlineMesh;

        MeshRenderer renderer = _outlineObject.AddComponent<MeshRenderer>();

        renderer.material = _outlineMat;
        renderer.transform.parent = transform;
    }


    public void SetOutlineActive(Boolean active)
    {
        _outlineActive = active;
        _outlineObject.SetActive(active);
    }

    IEnumerator fadeAway (float seconds)
    {
        if (!_outlineActive)
        {
            for (float t = 0; t < _fadeTime; t += Time.deltaTime)
            {

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
