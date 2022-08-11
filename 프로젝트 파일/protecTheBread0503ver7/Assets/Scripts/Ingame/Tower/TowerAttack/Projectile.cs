using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected GameObject effect;                        // ����Ʈ ������

    [SerializeField]
    private float moveSpeed;                            // ����ü �ӵ�
    private Vector3 moveDirection = Vector3.zero;       // ����ü �߻����
    private int damage;                                 // ����ü ���ݷ�
    private Transform target;                           // ��ǥ����

    public float MoveSpeed
    { get { return moveSpeed; } set { moveSpeed = value; } }
    public Vector3 MoveDirection
    { get { return moveDirection; } set { moveDirection = value; } }    
    public int Damage
    { get { return damage; } set { damage = value; } }
    public Transform Target
    { get { return target; } set { target = value; } }

    public virtual void SetUp(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    public virtual void KnightSetUp(Transform transform)
    {

    }

}
