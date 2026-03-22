using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    public void Init(Parameters player)
    {
       GameObject modelo = Instantiate(player.modelPrefab, transform);
        //restablecer rotacion
        player.modelPrefab.transform.localPosition = Vector3.zero;
        player.modelPrefab.transform.localRotation = Quaternion.identity;
    }
}
