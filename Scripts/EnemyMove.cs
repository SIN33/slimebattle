using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]


public class EnemyMove : MonoBehaviour
{
    [SerializeField] public PlayerController _playerController;
    [SerializeField] private LayerMask raycastLayerMask;
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;
    //private EnemyStatus _status;
    // Start is called before the first frame update
    //void Start()
    //{
    //    _agent = GetComponent<NavMeshAgent>();
    //    _status = GetComponent<EnemyStatus>();
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    _agent.destination = _playerController.transform.position; //foward to player
        
    //}

    
    public void OnDetectObject(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            _agent.destination = collider.transform.position;
            var positionDiff = collider.transform.position - transform.position; //calc player enemy diff
            var distance = positionDiff.magnitude;

            var direction = positionDiff.normalized; // direction player,

            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);

            if(hitCount == 0)
            {
                //プレイヤーにはcolliderの設定はおこなっていない、そのためRaycastはヒットしないため、0の場合はプレイヤー敵の障害物がないこと
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else 
            {
                _agent.isStopped = true;
            }
        }
        //if(!_status.IsMovable)
        //{
        //    _agent.isStopped = true;
        //    return;
        //}
    }
}
