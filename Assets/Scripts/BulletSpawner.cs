using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletSpawner : MonoBehaviour
{

    
    [SerializeField] 
    private GameObject _bullet;

    [SerializeField] 
    private float _bulletVelocity = 22f;

    public UnityEvent<int> _bulletReduction;
    
    public int _numberBullets = 20;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (_numberBullets>0)
            {
                Shooting();
                _numberBullets--;
                _bulletReduction.Invoke(_numberBullets);
            }
            


        }
    }

    private void Shooting()
    {
        var bullet = Instantiate(_bullet, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletVelocity;
        
    }
}
