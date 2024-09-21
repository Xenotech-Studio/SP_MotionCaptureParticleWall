using UnityEngine;


public class PortalsFX2_GrvityPoint : MonoBehaviour
{
    public Transform Target;//是人的圆柱体吗
    public float followForce = 1;
    public float dragForce = 1;
    public float StopDistance = 0;
    public float maxInfluenceDistance = 0;
    public float lifeTimeReduce = 0.99f;
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    private Vector3 lastTransform;
    private bool isleft;
    ParticleSystem.MainModule mainModule;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        mainModule = ps.main;
        lastTransform = Target.position;
    }

    void Update()//后更新是在更新后，保证update里动完了之后再获取的确定坐标
    {
        
        if (Target == null) return;
        if (lastTransform!= Target.position)
        {
            isleft = Target.position.x < lastTransform.x;
            lastTransform= Target.position;
        }
        var maxParticles = mainModule.maxParticles;
        if (particles == null || particles.Length < maxParticles)//创建的particles不满粒子
        {
            particles = new ParticleSystem.Particle[maxParticles];//创建满粒子？
        }
        int particleCount = ps.GetParticles(particles);//得到目前ps上的粒子数量

        var targetTransformedPosition = Vector3.zero;
        if (mainModule.simulationSpace == ParticleSystemSimulationSpace.Local)//获取ps的放缩空间
            targetTransformedPosition = transform.InverseTransformPoint(Target.position);//括号里变量相对于transform的坐标的值！
        if (mainModule.simulationSpace == ParticleSystemSimulationSpace.World)
            targetTransformedPosition = Target.position;
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
                particles[i].velocity=Vector3.zero;
                Vector3 orthogonalTargetTransformedPosition = Vector3.Cross(Vector3.back, -distanceToParticle).normalized;//目标位置的正交位置
                particles[i].velocity += orthogonalTargetTransformedPosition.normalized *  Time.deltaTime * Mathf.Sign((distanceToParticle.x)*dragForce);
                
            }
            else
            {
                if (isleft)
                {
                    if (distanceToParticle.x > 0)
                    {
                        var directionToTarget = Vector3.Normalize(distanceToParticle);//目标方向
                        var seekForce = directionToTarget* Time.deltaTime * followForce;//方向*单位时间力*力大小的速度
                        particles[i].velocity += seekForce;//速度上改变
                        particles[i].velocity -= new Vector3(0.01f,0,0)*Mathf.Sign((distanceToParticle.x));

                    }
                    else
                    {
                        Vector3 orthogonalTargetTransformedPosition = Vector3.Cross(Vector3.back, -distanceToParticle).normalized;//目标位置的正交位置
                        particles[i].velocity += orthogonalTargetTransformedPosition.normalized *  Time.deltaTime *dragForce;
                        particles[i].velocity+= new Vector3(0.05f,0,0)*Mathf.Sign((distanceToParticle.x));
                    }
                }
                else
                {
                    if (distanceToParticle.x < 0)
                    {
                        var directionToTarget = Vector3.Normalize(distanceToParticle);//目标方向
                        var seekForce = directionToTarget* Time.deltaTime * followForce;//方向*单位时间力*力大小的速度
                        particles[i].velocity += seekForce;//速度上改变
                        particles[i].velocity -= new Vector3(0.01f,0,0)*Mathf.Sign((distanceToParticle.x));

                    }
                    else
                    {
                        Vector3 orthogonalTargetTransformedPosition = Vector3.Cross(Vector3.back, -distanceToParticle).normalized;//目标位置的正交位置
                        particles[i].velocity += orthogonalTargetTransformedPosition.normalized *  Time.deltaTime *dragForce;
                        particles[i].velocity+= new Vector3(0.05f,0,0)*Mathf.Sign((distanceToParticle.x));
                    }
                }

                   
            }
        }

        ps.SetParticles(particles, particleCount);
    }
}
