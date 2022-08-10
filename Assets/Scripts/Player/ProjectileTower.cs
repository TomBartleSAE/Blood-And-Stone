using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEditor;
using UnityEngine;

public class ProjectileTower : DamageTower
{
    public Transform projectile;

    private bool isLaunching;
    private Vector3 point1, point2, point3;
    [Range(0f,1f)] public float curveTime;
    public Vector3 targetArea;
    public float airTime = 1f;
    public float arcHeight = 5f;
    private Vector3 startingPosition;
    public ParticleSystem impactParticle;
    public float areaOfEffect = 1f;
    public LayerMask enemyLayer;

    private void Start()
    {
        startingPosition = projectile.localPosition;
    }

    public override void Attack()
    {
        base.Attack();

        targetArea = target.transform.position;
        
        point1 = projectile.position;
        point2 = new Vector3((projectile.position.x + targetArea.x) / 2f, arcHeight, (projectile.position.z + targetArea.z) / 2f);
        point3 = targetArea;
        
        isLaunching = true;
        
        Invoke(nameof(SplashDamage), airTime);
    }

    public override void Update()
    {
        base.Update();

        if (isLaunching)
        {
            curveTime += Time.deltaTime * (1f / airTime);
            projectile.position = CalculateCurvePoint(curveTime);

            if (curveTime >= 1f)
            {
                // Play impact particle
                isLaunching = false;
                curveTime = 0;
                
                projectile.gameObject.SetActive(false);
                
                impactParticle.transform.position = targetArea;
                impactParticle.Play();
                
                Invoke(nameof(ResetProjectile), 3f);
            }
        }
    }

    public void SplashDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(targetArea, areaOfEffect, enemyLayer, QueryTriggerInteraction.Ignore);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Health>().ChangeHealth(-damage, gameObject);
        }
    }

    public void ResetProjectile()
    {
        projectile.localPosition = startingPosition;
        projectile.gameObject.SetActive(true);
    }
    
    public Vector3 CalculateCurvePoint(float time)
    {
        // Calculates the lerp for each control point, then lerps between those to get final curve point
        Vector3 a = Vector3.Lerp(point1, point2, time);
        Vector3 b = Vector3.Lerp(point2, point3, time);
        return Vector3.Lerp(a, b, time);
    }
}
