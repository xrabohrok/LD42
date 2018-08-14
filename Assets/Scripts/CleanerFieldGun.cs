using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerFieldGun : MonoBehaviour, IGun
{

    public GameObject energyField;
    public float maxScale = 3;
    public float startScale = 1;
    public float growRatePerSecond = .02f;
    private Vector3 baseScale;

    private GameObject currentFieldInstance;
    private float currentScale = 1;

    private bool fieldActive;

    public void Fire()
    {
        fieldActive = true;
    }

    public void StoppedFire()
    {
        fieldActive = false;
    }

    public void Start()
    {
        currentFieldInstance = Instantiate(energyField, Vector3.zero, Quaternion.identity, this.transform);
        currentFieldInstance.transform.localScale = Vector3.one * startScale;
        currentFieldInstance.SetActive(false);

        currentScale = startScale;
        baseScale = currentFieldInstance.transform.localScale;
    }

    public void Update()
    {
        if (fieldActive)
        {
            if (currentScale < maxScale)
            {
                currentScale += growRatePerSecond * Time.deltaTime;
                currentFieldInstance.transform.localScale = baseScale * currentScale;
            }
        }
    }
}