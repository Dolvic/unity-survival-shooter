﻿using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public int maxAmmo = 20;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f;
    public int currentAmmo { get; private set; }

    public void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        currentAmmo = maxAmmo;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && currentAmmo > 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    public bool AddAmmo()
    {
        bool ammoAdded = false;
        if (currentAmmo < 20)
        {
            currentAmmo = 20;
            ammoAdded = true;
        }
        return ammoAdded;
    }

    private void Shoot()
    {
        timer = 0f;
        currentAmmo--;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            var enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}