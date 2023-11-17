using UnityEngine;

public interface IWeapon
{
    Character Owner { get; set; }

    void Attack(Transform player, Transform target);
}
