using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

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
#if ENABLE_INPUT_SYSTEM

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
#else
                if (Input.GetKeyDown(KeyCode.Space))
#endif
            {
                animations.Attack(1);
            }
        }
    }
}
