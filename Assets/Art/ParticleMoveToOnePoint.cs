using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMoveToOnePoint : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public Transform targetPoint;
    ParticleSystem.Particle[] particles;

    public AnimationCurve affectiveRadiusCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    public float maxAffectiveRadius = 1;
    
    public AnimationCurve forceAmongTimeCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float maxForce = 1;
    
    public float timer = 0;
    public float Duration = 1;
    public float disappearDistance;
    public float baseTime=5;
    [Range(1,1.6f)]
    public float timeFactor;
    public float allTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        allTime = 0;
    }

    private void OnEnable()
    {
        timer = 0;
        allTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        MoveToTargetPoint();
       
        
    }
    
    public void MoveToTargetPoint()
    {
        var maxParticles = ParticleSystem.main.maxParticles;
        if (particles == null || particles.Length < maxParticles) //创建的particles不满粒子
        {
            particles = new ParticleSystem.Particle[maxParticles]; //创建满粒子？
        }
        
        float affectiveRadius = timeFactor*timeFactor*maxAffectiveRadius * affectiveRadiusCurve.Evaluate(timer / Duration);
        float forceAmongTime = maxForce * forceAmongTimeCurve.Evaluate(timer / Duration);
        
        int particleCount = ParticleSystem.GetParticles(particles);
        for (int i = 0; i < particleCount; i++)
        {
            float distanceFromCenter = Vector3.Distance(particles[i].position, transform.position);
            if (Math.Abs(distanceFromCenter) < disappearDistance)
            {
                particles[i].startSize = 0;
                
            }
            if (distanceFromCenter > affectiveRadius)
            {
                // apply force to move twaords the target point
                particles[i].velocity += (targetPoint.position - particles[i].position).normalized * forceAmongTime * Time.deltaTime*Vector3.Distance(targetPoint.position,transform.position);
               
            }
            
            
        }
        
        ParticleSystem.SetParticles(particles, particleCount);
    }
    
    public void UpdateTimer()
    {
        if (timer < Duration)
        {
            timer += Time.deltaTime;
            allTime += Time.deltaTime;
        }
    }
    
    
}
