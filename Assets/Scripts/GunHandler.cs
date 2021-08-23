using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class GunHandler : NetworkBehaviour
{
    private NetworkVariableBool shooting = new NetworkVariableBool(false);
    private GameObject bullets;
    private ParticleSystem.EmissionModule bulletsEM;

    void Start()
    {
        bullets = this.transform.Find("BulletParticles").gameObject;
        Debug.Log("bullets: ");
        Debug.Log(bullets);
        bulletsEM = bullets.GetComponent<ParticleSystem>().emission;
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
}
