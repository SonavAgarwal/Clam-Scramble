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

    private ItemHandler itemHandler;

    void Start()
    {
        itemHandler = GetComponent<ItemHandler>();
        bullets = this.transform.Find("BulletParticles").gameObject;
        Debug.Log("bullets: ");
        Debug.Log(bullets);
        bulletsEM = bullets.GetComponent<ParticleSystem>().emission;
        //ListenChanges();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool doShoot = itemHandler.IsHeld() && shooting.Value;
        bulletsEM.rateOverTime = doShoot ? 10f : 0f; 
        if (doShoot)
        {
            var player = itemHandler.GetPlayer();
            //var kbMotion = new KnockbackMotion();
            var kbMotion = gameObject.AddComponent<KnockbackMotion>();
            kbMotion.SetKnockbackMotion(this.transform.forward * -5f);
            player.GetComponent<MotionHandler>().AddMotion(kbMotion);
        }
    }

    public void StartAttack()
    {
        shooting.Value = true;
    }
    public void StopAttack()
    {
        shooting.Value = false;
    }

    //void ListenChanges()
    //{
    //    shooting.OnValueChanged += valueChanged;
    //}

    //void valueChanged(bool prevF, bool newF)
    //{
    //    Debug.Log("myFloat went from " + prevF + " to " + newF);
    //}
}
