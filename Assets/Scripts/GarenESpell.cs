using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarenESpell : MonoBehaviour
{
    public float coolDown;
    [SerializeField] private float maxCoolDown = 5f;
    public float timerSpell;

    [SerializeField] private float maxTimerSpell = 3f;

    [SerializeField] private float rotateAmount = 5f;

    public bool isActived;

    private Quaternion _baseRotation;

    [SerializeField] private ParticleSystem[] eSpellVfx;

    // Start is called before the first frame update
    void Start()
    {
        timerSpell = maxTimerSpell;
        _baseRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        coolDown = Mathf.Clamp(coolDown, 0, Mathf.Infinity);

        if (Input.GetKey(KeyCode.E) && coolDown <= 0)
        {
            isActived = true;
            coolDown = maxCoolDown;
        }

        if (isActived && timerSpell > 0)
        {
            GarenEAbility();
            foreach (var vfx in eSpellVfx)
            {
              
                vfx.Play();
            }
        }

        if (timerSpell <= 0)
        {
            foreach (var vfx in eSpellVfx)
            {
                vfx.Stop();
                
            }

            isActived = false;
            timerSpell = maxTimerSpell;
            transform.rotation = _baseRotation;
        }
    }

    void GarenEAbility()
    {
        timerSpell -= Time.deltaTime;
        timerSpell = Mathf.Clamp(timerSpell, 0, Mathf.Infinity);
        transform.Rotate(new Vector3(0, -rotateAmount * Time.deltaTime, 0), Space.Self);
    }
}