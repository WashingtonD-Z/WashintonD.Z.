using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "GunScriptObj", menuName = "Guns/Gun", order = 0)]

public class GunScriptObj : ScriptableObject 
{
    //public ImpactType impactType;
    public Transform playerCamera;
    public GunType type;
    public string gunName;
    public GameObject modelPrefab;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;

    public DamageConfig damageConfig;
    public ShootingConfigScriptObj shootConfig;
    public BulletTrailConfig trailConfig;
    public AmmoConfig ammoConfig;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject model;
    private float lastShot;
    private ParticleSystem pSystem;
    private ObjectPool<TrailRenderer> trailPool;

    public void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        playerCamera = Camera.main.transform;
        lastShot = 0;
        trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        model = Instantiate(modelPrefab);
        model.transform.SetParent(Parent, false);
        model.transform.localPosition = spawnPoint;
        model.transform.localRotation = Quaternion.Euler(spawnRotation);

        pSystem = model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
        if (Time.time > shootConfig.fireRate + lastShot)
        {
            if (!ammoConfig.CheckIfCanShoot())
            {
                return;
            }
            lastShot = Time.time;
            pSystem.Play();
            Vector3 shotDirection = playerCamera.transform.forward + new Vector3( Random.Range(-shootConfig.spread.x, shootConfig.spread.x), Random.Range(-shootConfig.spread.y, shootConfig.spread.y), Random.Range(-shootConfig.spread.z, shootConfig.spread.z));
            shotDirection.Normalize();
            if (Physics.Raycast(playerCamera.position, shotDirection, out RaycastHit hit, float.MaxValue, shootConfig.hitMask))
            {
                activeMonoBehaviour.StartCoroutine (PlayTrail(pSystem.transform.position, hit.point, hit));
            }
            else
            {
                activeMonoBehaviour.StartCoroutine (PlayTrail(pSystem.transform.position, pSystem.transform.position + (shotDirection * trailConfig.missDistance), new RaycastHit()));
            }
        }
    }

    private IEnumerator PlayTrail(Vector3 Startpoint, Vector3 Endpoint, RaycastHit Hit)
    {
        TrailRenderer instance = trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = Startpoint;
        yield return null;

        instance.emitting = true;

        float distance = Vector3.Distance(Startpoint, Endpoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(Startpoint, Endpoint, Mathf.Clamp01(1 - (remainingDistance/distance)));
            remainingDistance -= trailConfig.simulationSpeed * Time.deltaTime;

            yield return null;
        } 

        instance.transform.position = Endpoint;

        if (Hit.collider != null)
        {
        //    SurfaceManager.Instance.HandleImpact (Hit.transform.gameObject, Endpoint, Hit.normal, impactType, 0);

            if (Hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damageConfig.GetDamage(distance));
            }
        }
        
        yield return new WaitForSeconds(trailConfig.duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        trailPool.Release(instance);
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = trailConfig.color;
        trail.material = trailConfig.material;
        trail.widthCurve = trailConfig.widthCurve;
        trail.time = trailConfig.duration;
        trail.minVertexDistance = trailConfig.minVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    public GameObject Reload(GameObject mag)
    {
        return ammoConfig.ReloadMag(mag); 
    }
}