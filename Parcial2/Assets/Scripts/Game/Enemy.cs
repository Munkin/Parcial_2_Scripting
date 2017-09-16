using UnityEngine;

namespace Parcial2.Game
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        protected enum Tier
        {
            Base = 0,
            Low = 1,
            Mid = 2,
            High = 3
        }

        [SerializeField]
        private Tier tier;

        [SerializeField]
        public Color newColor;

        [SerializeField]
        public SkinnedMeshRenderer bodyMesh;

        [SerializeField]
        private float speed;

        public int Atk { get; protected set; }
        public int HP { get; protected set; }

        private bool canMove = true;

        public void ReceiveDamage(int damage, bool collidedWithPlayer = false)
        {
            Debug.Log(string.Format("[{0}] received [{1}] damage pts, remaining [{2}] HP", name, damage, HP));

            if (collidedWithPlayer)
            {
                canMove = false;
            }

            HP -= damage;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            /*switch (tier)
            {
                case Tier.Base:
                    Atk = 5;
                    HP = 20;
                    break;

                case Tier.Low:
                    Atk = 10;
                    HP = 40;
                    break;

                case Tier.Mid:
                    Atk = 20;
                    HP = 80;
                    break;

                case Tier.High:
                    Atk = 40;
                    HP = 200;
                    break;
            }*/

            if (bodyMesh != null)
            {
                bodyMesh.material.color = newColor;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Player.Instance != null)
            {
                transform.LookAt(Player.Instance.transform);

                if (canMove)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
        }

        public void SetRandomParameters()
        {
            Atk = Random.Range(5, 40);
            HP = Random.Range(20, 200);
        }
    }
}