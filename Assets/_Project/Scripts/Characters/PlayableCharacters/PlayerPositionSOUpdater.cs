using UnityEngine;

public class PlayerPositionSOUpdater : MonoBehaviour
{
    public PlayerPositionSO PlayerPositionSO;
    
    void Update()
    {
        PlayerPositionSO.Value = this.transform.position;
    }
}
