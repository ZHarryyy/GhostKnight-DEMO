using System.Collections;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float blinkDuration = 0.1f;
    private float minAlpha = 0.4f;
    private float maxAlpha = 1f;
    private float elapsedTime;
    private bool isFadingOut;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    protected override void Update()
    {
        base.Update();

        InvincibleBlinkHandle();

        //if (isInvincible)
        //{
        //    elapsedTime += Time.deltaTime;

        //    if (elapsedTime >= blinkDuration)
        //    {
        //        if (isFadingOut)
        //        {
        //            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, maxAlpha);
        //            isFadingOut = false;
        //        }
        //        else
        //        {
        //            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, minAlpha);
        //            isFadingOut = true;
        //        }

        //        elapsedTime = 0f;
        //    }
        //}
        //else
        //{
        //    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, maxAlpha);
        //}
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();

        player.Die();

        if (player.canTriggerBlackhole) player.stats.isDead = false;

        GameManager.instance.lostSoulAmount = PlayerManager.instance.currency;
        PlayerManager.instance.currency = 0;

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    public override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        if (!isIgnited && !isChilled && !isShocked)
        {
            player.SetupKnockbackPower(new Vector2(10, 6));
            AudioManager.instance.PlaySFX(36, null);
            player.fx.ScreenShake(player.fx.shakeHighDamage);
        }

        player.stats.MakeInvincible(true);
        InvincibleBlinkHandle();

        if (Inventory.instance != null)
        {
            ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

            if (currentArmor != null) currentArmor.Effect(player.transform);
        }

        StartCoroutine(DisableInvincibilityAfterDuration());
    }

    private void InvincibleBlinkHandle()
    {
        if (isInvincible)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= blinkDuration)
            {
                if (isFadingOut)
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, maxAlpha);
                    isFadingOut = false;
                }
                else
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, minAlpha);
                    isFadingOut = true;
                }

                elapsedTime = 0f;
            }
        }
        else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, maxAlpha);
        }
    }

    private IEnumerator DisableInvincibilityAfterDuration()
    {
        yield return new WaitForSeconds(1.5f);

        player.stats.MakeInvincible(false);
    }

    public override void OnEvasion()
    {
        player.skill.dodge.CreateMirageOnDodge();
    }

    public override void InvincibaleDoDamage(CharacterStats _targetStats)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _targetStats.GetComponent<Collider2D>(), true);
        //base.InvincibaleDoDamage(_targetStats);
        StartCoroutine(ReenableCollisionAfterDelay(_targetStats.GetComponent<Collider2D>()));
    }

    private IEnumerator ReenableCollisionAfterDelay(Collider2D targetCollider)
    {
        yield return new WaitForSeconds(1f);

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), targetCollider, false);
    }

    public void CloneDoDamage(CharacterStats _targetStats, float _multiplier)
    {
        if (TargetCanAvoidAttack(_targetStats)) return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (_multiplier > 0) totalDamage = Mathf.RoundToInt(totalDamage * _multiplier);

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);

        DoMagicDamage(_targetStats);
    }
}
