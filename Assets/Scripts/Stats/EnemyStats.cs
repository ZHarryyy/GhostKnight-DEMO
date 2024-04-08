using System.Collections;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;
    public Stat soulsDropAmount;

    [Header("Level details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = .4f;

    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(100);
        ApplyLevelModifiers();

        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);

        Modify(soulsDropAmount);
    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();

        enemy.Die();

        PlayerManager.instance.currency += soulsDropAmount.GetValue();

        myDropSystem.GenerateDrop();

        Destroy(gameObject, 5f);
    }


    public override void InvincibaleDoDamage(CharacterStats _targetStats)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _targetStats.GetComponent<Collider2D>(), true);
        StartCoroutine(ReenableCollisionAfterDelay(_targetStats.GetComponent<Collider2D>()));
    }

    private IEnumerator ReenableCollisionAfterDelay(Collider2D targetCollider)
    {
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), targetCollider, false);
    }
}
