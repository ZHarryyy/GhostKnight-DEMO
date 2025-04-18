
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();//设置triggerCalled为true，在playerState.Enter中设置为false
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                    hit.GetComponent<Enemy>().getHitted = true;
                }

                AudioManager.instance.PlaySFX(35, null);

                player.fx.ScreenShake(player.fx.shakeNormalDamage);

                if (Inventory.instance != null)
                {
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null) weaponData.Effect(_target.transform);
                }
            }
            else if (hit.GetComponent<Chest>() != null)
            {
                Chest chest = hit.GetComponent<Chest>();
                if (!chest.isOpened)
                {
                    chest.StartShakeAndChange();
                    player.fx.ScreenShake(player.fx.shakeNormalDamage);
                }
            }
            else
            {
                AudioManager.instance.PlaySFX(34, null);
            }
        }
    }

    private void DownwardAttackTrigger()//小红帽
    {
        ContactFilter2D cf2D = new ContactFilter2D
        {
            useLayerMask = false,
            useTriggers = true
            //layerMask = LayerMask.NameToLayer("Enemy")
        };


        //获取与Collider2D相交的敌人的colliders
        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(player.downwardAttackCheck, cf2D, colliders) ;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                    hit.GetComponent<Enemy>().getHitted = true;
                }

                AudioManager.instance.PlaySFX(35, null);

                player.fx.ScreenShake(player.fx.shakeNormalDamage);

                if (Inventory.instance != null)
                {
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null) weaponData.Effect(_target.transform);
                }
            }
            else if (hit.GetComponent<Chest>() != null)
            {
                Chest chest = hit.GetComponent<Chest>();
                if (!chest.isOpened)
                {
                    chest.StartShakeAndChange();
                    player.fx.ScreenShake(player.fx.shakeNormalDamage);
                }
            }
            else
            {
                AudioManager.instance.PlaySFX(34, null);
            }
        }
    }

    private void HeavyAttack1Trigger()
    {
        ContactFilter2D cf2D = new ContactFilter2D
        {
            useLayerMask = false,
            useTriggers = true
            //layerMask = LayerMask.NameToLayer("Enemy")
        };

        //获取与Collider2D相交的敌人的colliders
        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(player.heavyAttackCheck1, cf2D, colliders);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                    hit.GetComponent<Enemy>().getHitted = true;
                }

                AudioManager.instance.PlaySFX(35, null);

                player.fx.ScreenShake(player.fx.shakeNormalDamage);

                if (Inventory.instance != null)
                {
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null) weaponData.Effect(_target.transform);
                }
            }
            else if (hit.GetComponent<Chest>() != null)
            {
                Chest chest = hit.GetComponent<Chest>();
                if (!chest.isOpened)
                {
                    chest.StartShakeAndChange();
                    player.fx.ScreenShake(player.fx.shakeNormalDamage);
                }
            }
            else
            {
                AudioManager.instance.PlaySFX(34, null);
            }
        }
    }

    private void HeavyAttack2Trigger()
    {
        ContactFilter2D cf2D = new ContactFilter2D
        {
            useLayerMask = false,
            useTriggers = true
            //layerMask = LayerMask.NameToLayer("Enemy")
        };

        //获取与Collider2D相交的敌人的colliders
        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(player.heavyAttackCheck2, cf2D, colliders);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                    hit.GetComponent<Enemy>().getHitted = true;
                }

                AudioManager.instance.PlaySFX(35, null);

                player.fx.ScreenShake(player.fx.shakeNormalDamage);

                if (Inventory.instance != null)
                {
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                    if (weaponData != null) weaponData.Effect(_target.transform);
                }
            }
            else if (hit.GetComponent<Chest>() != null)
            {
                Chest chest = hit.GetComponent<Chest>();
                if (!chest.isOpened)
                {
                    chest.StartShakeAndChange();
                    player.fx.ScreenShake(player.fx.shakeNormalDamage);
                }
            }
            else
            {
                AudioManager.instance.PlaySFX(34, null);
            }
        }
    }

    private void DownwardAttackChargedTrigger()//空中蓄力完毕才有霸体
    {
        player.stateMachine.currentState.DownwardAttackCharged();
    }

    private void HeavyAttackReleaseTrigger()
    {

    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }

    private void AnimationStop()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && !hit.GetComponent<Enemy>().isDead) StartCoroutine(StopAnimation());
        }
    }

    private void SuperArmorTrigger()//霸体触发和关闭
    {
        player.stats.isSuperArmor = !player.stats.isSuperArmor;
        if(player.stats.isSuperArmor)
        {
            Debug.Log("super");
        }
        else
        {
            Debug.Log("No_super");
        }
    }

    private IEnumerator StopAnimation()
    {
        float animationStopDuration = 0.15f;

        player.anim.enabled = false;

        yield return new WaitForSeconds(animationStopDuration);

        player.anim.enabled = true;
    }
}
