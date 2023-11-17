using UnityEngine;

public class ItemWeapon : MonoBehaviour
{
    [SerializeField] private WeaponType type;
    [SerializeField] private GameObject modelPrefab;

    public WeaponType Type => type;

    void Awake()
    {
        modelPrefab = Instantiate(modelPrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
        modelPrefab.transform.localScale *= 2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.CHARACTER_TAG))
        {
            Character character = Cache.GenCharacter(other);
            character.ChangeWeapon(Type);
            OnDespawn();
        }
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
