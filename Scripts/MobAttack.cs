using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IMobStatus))]

public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider;

    private IMobStatus _status;

    // Start is called before the first frame update
    private void Start()
    {
        _status = GetComponent<IMobStatus>();
    }

    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();
    }

    //invoke whne into range
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    public void OnHitAttack(Collider collider)
    {
        var targetEnmey = collider.GetComponent<IMobStatus>();
        if (targetEnmey == null) return;

        targetEnmey.Damage(1);
    }

    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
