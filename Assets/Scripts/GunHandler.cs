using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class GunHandler : NetworkBehaviour
{
    private NetworkVariable<bool> shooting = new NetworkVariable<bool>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone }, false);
    private GameObject bullets;
    private ParticleSystem.EmissionModule bulletsEM;

    void Start()
    {
        bullets = this.transform.Find("BulletParticles").gameObject;
        Debug.Log("bullets: ");
        Debug.Log(bullets);
        bulletsEM = bullets.GetComponent<ParticleSystem>().emission;
        ListenChanges();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        bulletsEM.rateOverTime = shooting.Value ? 10f : 0f;
    }

    public void StartShoot()
    {
        shooting.Value = true;
    }
    public void StopShoot()
    {
        shooting.Value = false;
    }

    void ListenChanges()
    {
        shooting.OnValueChanged += valueChanged;
    }

    void valueChanged(bool prevF, bool newF)
    {
        Debug.Log("myFloat went from " + prevF + " to " + newF);
    }
}
