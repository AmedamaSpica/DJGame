using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    protected float HP = 10;

    [SerializeField]
    GameObject TargetObject;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    int MaxCount = -200;

    [SerializeField]
    float bulletSpeed = 10f;

    protected int timeCount;
    protected float maxHP;
   
    private void Start()
    {
        maxHP = HP;
        timeCount = 0;
    }
    void FixedUpdate()
    {
        if (HP > 0)
            timeCount++;

        if(timeCount >= MaxCount)
        {
            timeCount = 0;
            EnemyShot();
        }       
    }
    // Update is called once per frame
    void EnemyShot()
    {
        int BulletCount = 5;

        for (int i = 0; i < BulletCount; i++)
        {
            GameObject Newbullet = Instantiate(bullet, this.transform.position + Vector3.up, Quaternion.identity);
            Rigidbody bulletRB = Newbullet.GetComponent<Rigidbody>();
            ShotRandom(Newbullet, bulletRB);         
        }

        for (int i = 0; i < BulletCount-2; i++)
        {
            GameObject Newbullet = Instantiate(bullet, this.transform.position + Vector3.up, Quaternion.identity);
            Rigidbody bulletRB = Newbullet.GetComponent<Rigidbody>();
            ShotPlayer(Newbullet, bulletRB);
        }
    }

    void ShotPlayer(GameObject _bullet, Rigidbody _rigidbody)
    {
        // �^�[�Q�b�g�̕���������
        Vector3 targetDirection = (TargetObject.transform.position - _bullet.transform.position).normalized;

        // Y ���̃����_����]��K�p
        Quaternion randomYRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(-30f, 30f), 0f);
        Quaternion finalRotation = Quaternion.LookRotation(targetDirection) * randomYRotation;

        // ��]��K�p
        _bullet.transform.rotation = finalRotation;

        // �͂������Ĕ���
        _rigidbody.AddForce(_bullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }
    void ShotRandom(GameObject _bullet, Rigidbody _rigidbody)
    {
        float randomYRotation = UnityEngine.Random.Range(0f, 360f);
        _bullet.transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);

        _rigidbody.AddForce(_bullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
    }
}
