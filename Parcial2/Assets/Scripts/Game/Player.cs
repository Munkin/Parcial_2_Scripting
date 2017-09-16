using UnityEngine;
using UnityEngine.UI;

namespace Parcial2.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Player : MonoBehaviour
    {
        public delegate void OnPlayerKilled();

        public event OnPlayerKilled onPlayerKilled;

        public Button botonDisparo;

        public Button botonDisparo2;

        private static Player instance;

        [SerializeField]
        private ParticleSystem onKilledPS;

        [SerializeField]
        private Bullet bulletBase;

        private PlayerProfile playerProfile;

        private int hp;
        private int atk;

        public static Player Instance
        {
            get
            {
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public int CurrentCurrency
        {
            get
            {
                return playerProfile.Currency;
            }
        }

        public void SubstractCurrency(int currencyToSubstract)
        {
            UpdateCurrency(currencyToSubstract * -1);
        }

        public void UpdateCurrency(int currencyAdd)
        {
            playerProfile.UpdateCurrency(currencyAdd);
        }

        private void Awake()
        {
            hp = 300;
            atk = 5;

            playerProfile = new PlayerProfile(5000);
            instance = this;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                DamageEnemy(enemy);
                Destroy(other.gameObject);
            }
        }

        private void DamageEnemy(Enemy enemy)
        {
            Debug.Log(string.Format("Damaging enemy with [{0}] Atk points", atk));

            hp -= enemy.Atk;

            if (hp <= 0)
            {
                DestroyPlayer();

                if (onPlayerKilled != null)
                {
                    onPlayerKilled();
                }
            }
        }

        private void DestroyPlayer()
        {
            if (onKilledPS != null)
            {
                (Instantiate(onKilledPS, transform.position, transform.rotation) as ParticleSystem).Play();
            }

            Destroy(this);
        }

        // Use this for initialization
        private void Start()
        {
        }

        private void Update()
        {
           /* if (Input.GetKeyDown(KeyCode.Space))
            {
               ShootBullet();
            }*/
        }

        public void ShootBullet(int tipo)
        {
        
        botonDisparo.interactable = false;
        botonDisparo2.interactable = false;

             Vector3 lookAtLocation = Vector3.zero;
                //Debug.DrawRay(transform.position, Vector3.forward * 5F, Color.green, 5F);

                Collider[] otherColliders = Physics.OverlapSphere(transform.position, 10F);

                for (int i = 0; i < otherColliders.Length; i++)
                {
                    if (otherColliders[i].gameObject == gameObject)
                    {
                        continue;
                    }
                    else
                    {
                        Enemy enemy = otherColliders[i].GetComponent<Enemy>();

                        if (enemy != null)
                        {
                            lookAtLocation = enemy.transform.position;
                            break;
                        }
                    }
                }

                if (lookAtLocation != Vector3.zero)
                {
                    transform.LookAt(lookAtLocation);
                }

                
                Bullet bulletInstance = Instantiate(bulletBase, transform.position + new Vector3(0F, 1F, 0F), transform.rotation);
                
                if(tipo == 0)
                bulletInstance.OnDisolve += ActivateButton;

                if(tipo == 1)
                bulletInstance.OnDisolve += ActivarBotonesDemora;
                
                bulletInstance.SetParams(50, 100, this.gameObject, tipo);
                bulletInstance.Toss();
        }

        public void ActivateButton()
        {
            botonDisparo.interactable = true;
            botonDisparo2.interactable = true;
        }

        public void ActivarBotonesDemora()
        {
            Invoke("ActivateButton",1);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 10);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector3.forward * 10F);
        }
    }
}