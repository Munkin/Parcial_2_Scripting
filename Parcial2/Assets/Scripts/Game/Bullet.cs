using UnityEngine;
using System;

namespace Parcial2.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody myRigidBody;
        private float speed;
        private int damage;

        public event Action OnDisolve;

        private float tiempoPasado;

        private float tiempoParaDestruir;

        private GameObject instigator;

        private int typeOfBullet;

        public void SetParams(float bulletSpeed, int bulletDamage, GameObject instanceInstigator, int miTipo)
        {
            instigator = instanceInstigator;
            speed = bulletSpeed;
            damage = bulletDamage;
            typeOfBullet = miTipo;

        }

        public void Toss()
        {
            myRigidBody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (typeOfBullet == 0)
            {
                if (other.gameObject == Player.Instance.gameObject)
                {
                    //Collided with player
                }
                else
                {
                    Enemy enemy = other.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.ReceiveDamage(damage);
                    }
                }

                if (instigator != other.gameObject)
                {
                    if(OnDisolve!=null)
                        OnDisolve();
                    Destroy(gameObject); 
                }
            }

            if(typeOfBullet == 1)
            {
               if (other.gameObject == Player.Instance.gameObject)
                {
                    //Collided with player
                }
                else
                {
                    Enemy enemy = other.GetComponent<Enemy>();

                    if (enemy != null)
                    {

                    Collider[] otherColliders = Physics.OverlapSphere(transform.position, 5F);

                    for (int i = 0; i < otherColliders.Length; i++)
                    {

                    if (otherColliders[i].gameObject == gameObject)
                    {
                        continue;
                    }
                    else
                    {
                        Enemy enemy2 = otherColliders[i].GetComponent<Enemy>();

                        if (enemy2 != null)
                        {
                            enemy2.ReceiveDamage(damage);
                            break;
                        }
                    }
                }
                        
                    }
                }

                if (instigator != other.gameObject)
                {
                    if(OnDisolve!=null)
                        OnDisolve();
                    Destroy(gameObject); 
                } 
            }
        }

        // Use this for initialization
        private void Awake()
        {
            myRigidBody = GetComponent<Rigidbody>();
            tiempoParaDestruir = 1f;
        }

        void Update()
        {
            if(tiempoPasado<tiempoParaDestruir)
            tiempoPasado += Time.deltaTime;
            else
            {
                OnDisolve();
                Destroy(gameObject);
            }
            
        }

        private void OnDestroy()
        {
            myRigidBody = null;
        }
    } 
}