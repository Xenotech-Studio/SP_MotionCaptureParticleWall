using System;
using System.Collections.Generic;
using UnityEngine;


public class PortalsFX2_GrvityPoint_Arm: MonoBehaviour
{
    public Transform Target;
    public List<GameObject> TargetHands;
    public float followForce = 1;
    public float dragForce = 1;
    public float StopDistance = 0;
    public float maxInfluenceDistance = 0;
    public float lifeTimeReduce = 0.99f;
    public ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    private Vector3 lastTransform;
    public int isleft = 1;
    ParticleSystem.MainModule mainModule;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();


    void Start()
    {
        mainModule = ps.main;
        //lastTransform = Target.position;
    }

    void Update()
    {
        var maxParticles = mainModule.maxParticles;
        if (particles == null || particles.Length < maxParticles) //创建的particles不满粒子
        {
            particles = new ParticleSystem.Particle[maxParticles]; //创建满粒子？
        }

        //得到目前ps上的粒子数量
        int particleCount = ps.GetParticles(particles);
        var targetTransformedPosition = Target.position;

        if (lastTransform == Target.position)
        {
            for (int i = 0; i < particleCount; i++)
            {
                /*for (int j = 0; j < TargetHands.Count; j++)
                {
                    Vector3 targetDirection = particles[i].position - TargetHands[j].transform.position;
                    targetDirection.z = 0;
                    if (targetDirection.magnitude >= StopDistance &&
                        targetDirection.magnitude < maxInfluenceDistance &&
                        (TargetHands[j].transform.position - Target.transform.position).magnitude >
                        0.0001f) //如果在停止距离范围内且能移
                    {
                        particles[i].velocity *= 0.01f;
                    }
                    if (targetDirection.magnitude <StopDistance)
                    {
                        Vector3 force = Vector3.Normalize(targetDirection) * Time.deltaTime *
                            dragForce / targetDirection.magnitude;
                        force.z *= 0f;
                        particles[i].velocity += force;
                    }
                }*/

                var distanceToParticle = targetTransformedPosition - particles[i].position; //每个particles到目标的距禓
                distanceToParticle.z = 0;
                if (particles[i].velocity.magnitude < 0.2f &&
                    distanceToParticle.magnitude < 2 * maxInfluenceDistance) //如果在停止距离范围内且能移
                {
                    particles[i].position -= new Vector3(Time.deltaTime * followForce, 0, 0) * particles[i].velocity.y;
                }
            }
            ps.SetParticles(particles, particleCount);
            lastTransform = Target.position;
            return;
        }

        if (Target.position.x > lastTransform.x && isleft > 0 && Target.position.x - lastTransform.x > 0.2f)
            {
                isleft *= -1;
            }

            if (Target.position.x < lastTransform.x && isleft < 0 && lastTransform.x - Target.position.x > 0.2f)
            {
                isleft *= -1;
            }

            lastTransform = Target.position;


            targetTransformedPosition = Target.position-isleft*new Vector3(30f,0,0);
            for (int i = 0; i < particleCount; i++)
            {
                var distanceToParticle = targetTransformedPosition - particles[i].position; //每个particles到目标的距禓
                /*for (int j = 0; j < TargetHands.Count; j++)
                {
                    Vector3 targetDirection = particles[i].position - TargetHands[j].transform.position;
                    targetDirection.z = 0;
                    if (targetDirection.magnitude >= StopDistance &&
                        targetDirection.magnitude < maxInfluenceDistance &&
                        (TargetHands[j].transform.position - Target.transform.position).magnitude >
                        0.0001f) //如果在停止距离范围内且能移
                    {
                        particles[i].velocity *= 0.01f;
                    }
                    if (targetDirection.magnitude <2*StopDistance)
                    {
                        Vector3 force = Vector3.Normalize(targetDirection) * Time.deltaTime *
                            dragForce / targetDirection.magnitude;
                        force.z *= 0f;
                        particles[i].velocity += force;
                    }
                }*/
                if (distanceToParticle.magnitude > maxInfluenceDistance)
                {
                    continue;
                }

                if (distanceToParticle.magnitude >= StopDistance) //如果在停止距离范围内且能移
                {
                    particles[i].velocity *= 0.01f;
                }
                else
                {
                    particles[i].position -= new Vector3(Time.deltaTime * followForce, Time.deltaTime * distanceToParticle.y, 0.000000001f) * isleft /
                                             distanceToParticle.magnitude;
                }
            }
            ps.SetParticles(particles, particleCount);
        }

        /*private void OnParticleTrigger()
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
                                      dragForce/targetDirection.magnitude;
                        force.z *= 0f;
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
                                      dragForce/targetDirection.magnitude;
                        force.z *= 0f;
                        p.velocity += force;
                        inside[i] = p;
                    }
                }
            }
    
            for (int i = 0; i < numExit; i++)
            {
                ParticleSystem.Particle p =exit[i];
                p.velocity *= 0f;
                exit[i] = p;
            }
    
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside,inside);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter,enter);
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit,exit);
        }*/
    
}
