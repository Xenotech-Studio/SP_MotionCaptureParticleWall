using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


public class PortalsFX2_GrvityPoint : MonoBehaviour
{
    public Transform Target;
    public List<GameObject> TargetHands;
    public float followForce = 1;
    public float dragForce = 1;
    public float nearDistance = 0;
    public float StopDistance = 0;
    public float handDistance = 0;
    public float maxInfluenceDistance = 0;
    public float lifeTimeReduce = 0.99f;
    public ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    private Vector3 lastTransform;
    public float isleft = 1;
    ParticleSystem.MainModule mainModule;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    public float accumulatedTime;

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

        if (Mathf.Abs(Target.position.x - lastTransform.x)<1f)
        {
            for (int i = 0; i < particleCount; i++)
            {
                /*for (int j = 0; j < TargetHands.Count; j++)
                {
                    Vector3 targetDirection = particles[i].position - TargetHands[j].transform.position;
                    targetDirection.z = 0;
                    if (targetDirection.magnitude >= handDistance &&
                        targetDirection.magnitude < maxInfluenceDistance &&
                        (TargetHands[j].transform.position - Target.transform.position).magnitude >
                        0.3f) //如果在停止距离范围内且能移
                    {
                       particles[i].velocity *= 0.95f;
                    }
                    if (targetDirection.magnitude <handDistance&&
                        (TargetHands[j].transform.position - Target.transform.position).magnitude >
                        0.5f)
                    {
                        Vector3 force = Vector3.Normalize(targetDirection) * Time.deltaTime *
                            dragForce*0.1f;
                        force.z *= 0f;
                        particles[i].position += force;
                        particles[i].rotation3D += new Vector3(0,0,10f*Time.deltaTime);
                        particles[i].angularVelocity3D += new Vector3(0,300f*Time.deltaTime,300f*Time.deltaTime);
                        particles[i].remainingLifetime *= lifeTimeReduce;
                    }
                }

                var distanceToParticle = targetTransformedPosition - particles[i].position; //每个particles到目标的距禓
                distanceToParticle.z = 0;
                if (particles[i].velocity.magnitude < 0.2f &&
                    distanceToParticle.magnitude < 2 * maxInfluenceDistance) //如果在停止距离范围内且能移
                {
                    /*particles[i].velocity += new Vector3(Time.deltaTime * followForce,
                                                 0, 0) * Mathf.Sign(-distanceToParticle.x);#1#
                }*/
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

        
        for (int i = 0; i < particleCount; i++)
        {
            var distanceToParticle = targetTransformedPosition - particles[i].position; //每个particles到目标的距禓
            for (int j = 0; j < TargetHands.Count; j++)
            {
                /*Vector3 targetDirection = particles[i].position - TargetHands[j].transform.position;
                targetDirection.z = 0;
                if (targetDirection.magnitude >= StopDistance &&
                    targetDirection.magnitude < maxInfluenceDistance &&
                    (TargetHands[j].transform.position - Target.transform.position).magnitude >
                    0.3f) //如果在停止距离范围内且能移
                {
                    particles[i].velocity *= 0.95f;
                }
                if (targetDirection.magnitude <handDistance&&
                    (TargetHands[j].transform.position - Target.transform.position).magnitude >
                    0.5f)
                {
                    Vector3 force = Vector3.Normalize(targetDirection) * Time.deltaTime *
                        dragForce*0.1f;
                    force.z *= 0f;
                    particles[i].position += force;
                    particles[i].rotation3D += new Vector3(0,0,10f*Time.deltaTime);
                    particles[i].angularVelocity3D += new Vector3(0,300f*Time.deltaTime,300f*Time.deltaTime);
                    particles[i].remainingLifetime *= lifeTimeReduce;
                }*/
            }
            if (distanceToParticle.magnitude > maxInfluenceDistance)
            {
                continue;
            }

            if (distanceToParticle.magnitude >= StopDistance) //如果在停止距离范围内且能移
            {
                particles[i].velocity *= 0.9f;
                /*particles[i].position += new Vector3(0,0 ,100f) * isleft /
                                         distanceToParticle.magnitude;*/
            }
           
            else if(distanceToParticle.magnitude<=StopDistance)
            { 
                 //particles[i].rotation += 270f*Time.deltaTime;//原地打转，稍微带点波动
                 particles[i].rotation3D += new Vector3(0,60*Time.deltaTime,0);//朝横、竖、原地转
                 particles[i].position += new Vector3(-isleft, Mathf.Sign(distanceToParticle.y)/10,0 )*Time.deltaTime*followForce;//z轴没用，x平点也行，y轴先随便用自带的吸引,y吸了，力还挺大
                 particles[i].velocity += new Vector3(-isleft, Mathf.Sign(distanceToParticle.y),0 )*Time.deltaTime*followForce*5;
                 particles[i].remainingLifetime *= lifeTimeReduce;
                // particles[i].velocity -= new Vector3((Time.deltaTime * followForce*2f+Random.Range(2f,3f) )* isleft, Time.deltaTime * distanceToParticle.y*4*Random.Range(-5f,2f), 0)*0.1f;
                
              /* particles[i].startSize *= Random.Range(1.01f,1.2f);
               
                
                
               */
              accumulatedTime += Time.deltaTime;
            }
            if(distanceToParticle.magnitude<=nearDistance&&Mathf.Sign(distanceToParticle.x)==Mathf.Sign(isleft))
            {
                //particles[i].velocity *= -1f;
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
            
            
            
            particles[i].position -= new Vector3((followForce + Random.Range(2f, 3f)) * isleft*Time.deltaTime ,
                                              Time.deltaTime * distanceToParticle.y * +Mathf.Sin(5*accumulatedTime)*1000,
                                              0)/distanceToParticle.magnitude ;
                particles[i].velocity -= new Vector3((Time.deltaTime * followForce*2f+Random.Range(2f,3f) )* isleft, Time.deltaTime * distanceToParticle.y*4*Random.Range(-5f,2f), 0)*0.1f;
                particles[i].rotation += 10f*Time.deltaTime;
              /* particles[i].startSize *= Random.Range(1.01f,1.2f);
               particles[i].rotation3D += new Vector3(0,0,10f*Time.deltaTime);
                particles[i].angularVelocity3D += new Vector3(0,20f*Time.deltaTime,20f*Time.deltaTime);
                particles[i].remainingLifetime *= lifeTimeReduce;
                accumulatedTime += Time.deltaTime;*/
            
            
            
            
        /*  for (int j = 0; j < TargetHands.Count; j++)
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
