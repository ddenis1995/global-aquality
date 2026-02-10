using UnityEngine;

namespace _Project.Scripts.Weapons.Projectiles
{
    public class PistolBullet : Projectile
    {
        [SerializeField] GameObject _sparkFX;
        protected override void OnLaunch() { /* Muzzle flash? Straight fly. */ }
        protected override void OnHit(Collider target)
        {
            // Sparks particle
            //ParticleSystem sparks = GetComponentInChildren<ParticleSystem>();
            if (_sparkFX) Instantiate(_sparkFX, transform.position, transform.rotation);
        }
    }
}