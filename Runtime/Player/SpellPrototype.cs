using UnityEngine;

namespace Redeev.PrototypingTools
{
    public class SpellPrototype : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] GameObject particle;

        private void Awake()
        {
            Invoke(nameof(Destroy), 3f);
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime * transform.forward;
        }

        private void Destroy()
        {
            Destroy(Instantiate(particle, transform.position, Quaternion.identity), 1f);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            CancelInvoke();
            Destroy();
        }
    }
}