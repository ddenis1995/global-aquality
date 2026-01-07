using UnityEngine;

public class BaseCharacter:MonoBehaviour
{
    public Stats Stats { get; private set; }
    public virtual void SetStats(Stats stats) => Stats = stats;

    public virtual void TakeDamage(int dmg)
    {
    }
}
