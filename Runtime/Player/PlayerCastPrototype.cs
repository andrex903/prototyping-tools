using UnityEngine;

namespace Redeev.PrototypingTools
{
    public class PlayerCastPrototype : MonoBehaviour
    {
        [SerializeField] GameObject spellPrefab;
        [SerializeField] Transform castPosition;

        private PlayerAnimationsPrototype animations;

        private void Awake()
        {
            animations = GetComponent<PlayerAnimationsPrototype>();
        }

        public void CastEvent(AnimationEvent _)
        {
            Instantiate(spellPrefab, castPosition.position, transform.rotation);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) animations.Attack(1);
        }
    }
}
