using System;
using System.Collections.Generic;
using UnityEngine;


public class PortalsFX2_GrvityPoint : MonoBehaviour
{
    public Transform Target;
    public List<GameObject> TargetHands;
    public float followForce = 1;
    public float dragForce = 1;
    public float StopDistance = 0;
    public float maxInfluenceDistance = 0;
    public float lifeTimeReduce = 0.99f;
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    private Vector3 lastTransform;
    private int isleft=1;
    ParticleSystem.MainModule mainModule;
    List<ParticleSystem.Particle> enter=new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside=new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit=new List<ParticleSystem.Particle>();
    

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        mainModule = ps.main;
        lastTransform = Target.position;
    }

    void Update()//后更新是在更新后，保证update里动完了之后再获取的确定坐标
    {
        
        if (Target == null||lastTransform== Target.position) return;
        if (Target.position.x > lastTransform.x&&isleft>0)
        {
            isleft *=-1 ;
        }
        lastTransform= Target.position;
        var maxParticles = mainModule.maxParticles;
        if (particles == null || particles.Length < maxParticles)//创建的particles不满粒子
        {
            particles = new ParticleSystem.Particle[maxParticles];//创建满粒子？
        }
        int particleCount = ps.GetParticles(particles);//得到目前ps上的粒子数量
        var targetTransformedPosition = Target.position;
        
        for (int i = 0; i < particleCount; i++)
        {
            var distanceToParticle = targetTransformedPosition - particles[i].position;//每个particles到目标的距禓
            distanceToParticle.z = 0;
            if (distanceToParticle.magnitude > maxInfluenceDistance)
            {
                continue;
            }
            if (StopDistance > 0.001f && distanceToParticle.magnitude >= StopDistance)//如果在停止距离范围内且能移
            {
                /*particles[i].velocity*=0.2f;*/
            }
            else
            {
                particles[i].velocity -= new Vector3( Time.deltaTime * followForce,0, 0)*isleft;
                particles[i].remainingLifetime*=lifeTimeReduce;
                

            }
            
        }
        ps.SetParticles(particles, particleCount);
    }

    private void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numinside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside,inside);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit,exit);
        for (int i = 0; i < numEnter; i++)
        {
            for (int j = 0; j < TargetHands.Count; j++)
            {
                if (TargetHands[j].GetComponent<Collider>().bounds.Contains(enter[i].position))
                {
                    ParticleSystem.Particle p = enter[i];
                    Vector3 targetDirection= enter[i].position - TargetHands[j].transform.position;
                    targetDirection.z = 0;
                    Vector3 force=Vector3.Normalize(targetDirection) * Time.deltaTime *
                                  dragForce*2;
                    force.z *= 0.1f;
                    p.velocity += force;
                    enter[i] = p;
                }
            }
        }
        for (int i = 0; i < numinside; i++)
        {
            for (int j = 0; j < TargetHands.Count; j++)
            {
                if (TargetHands[j].GetComponent<Collider>().bounds.Contains(inside[i].position))
                {
                    ParticleSystem.Particle p =inside[i];
                    Vector3 targetDirection= inside[i].position - TargetHands[j].transform.position;
                    targetDirection.z = 0;
                    Vector3 force=Vector3.Normalize(targetDirection) * Time.deltaTime *
                                  dragForce*2;
                    force.z *= 0.1f;
                    p.velocity += force;
                    inside[i] = p;
                }
            }
        }

        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p =exit[i];
            p.velocity *= 0.1f;
            exit[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside,inside);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter,enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit,exit);
    }
}
