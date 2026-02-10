using System;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.Weapons;
using UnityEngine;

namespace _Project.Scripts
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer = 1 << 6; // Enemy layer
        [SerializeField] private Collider[] overlapBuffer = new Collider[128]; // Pre-alloc! Tweak size

        public Vector3 _targetPosition { get; private set; }

        public Transform FindTarget(Vector3 fromPos, float range, TargetingTypes targetType)
        {
            Transform target = null;
            if (targetType == TargetingTypes.Emitting)
            {
                target = transform;
            }
            else
            {
                int hitCount = Physics.OverlapSphereNonAlloc(fromPos, range, overlapBuffer, targetLayer);
        
                
                switch (targetType)
                {
                    case TargetingTypes.Closest:
                        float closestDistSq = float.MaxValue;
                        for (int i = 0; i < hitCount; i++)
                        {
                            // Skip self/triggers if needed
                            if (overlapBuffer[i].transform == transform) continue;
            
                            Vector3 delta = overlapBuffer[i].transform.position - fromPos;
                            float distSq = delta.sqrMagnitude;
            
                            if (distSq < closestDistSq)
                            {
                                closestDistSq = distSq;
                                target = overlapBuffer[i].transform;
                            }
                        }
                        break;
                    
                    case TargetingTypes.Furthest:

                        float furthestDistSq = 0f;
                        
                        for (int i = 0; i < hitCount; i++)
                        {
                            // Skip self/triggers if needed
                            if (overlapBuffer[i].transform == transform) continue;
            
                            Vector3 delta = overlapBuffer[i].transform.position - fromPos;
                            float distSq = delta.sqrMagnitude;
            
                            if (distSq > furthestDistSq)
                            {
                                furthestDistSq = distSq;
                                target = overlapBuffer[i].transform;
                            }
                        }
                        break;
                    case TargetingTypes.Healthiest:
                        float mostHealth = 0f;
                        
                        for (int i = 0; i < hitCount; i++)
                        {
                            // Skip self/triggers if needed
                            if (overlapBuffer[i].transform == transform) continue;
                            float hp = overlapBuffer[i].GetComponent<BasicEnemyScript>().GetHealth();
            
                            if (hp > mostHealth)
                            {
                                mostHealth = hp;
                                target = overlapBuffer[i].transform;
                            }
                        }
                        break;
                    case TargetingTypes.MostDamaged:
                        float leastHealth = 0f;
                        
                        for (int i = 0; i < hitCount; i++)
                        {
                            // Skip self/triggers if needed
                            if (overlapBuffer[i].transform == transform) continue;
                            float hp = overlapBuffer[i].GetComponent<BasicEnemyScript>().GetHealth();
            
                            if (hp < leastHealth)
                            {
                                leastHealth = hp;
                                target = overlapBuffer[i].transform;
                            }
                        }
                        break;
                }
            }

            _targetPosition = target.position;
            return target;
        }
    }
}