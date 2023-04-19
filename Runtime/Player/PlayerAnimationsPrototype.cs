using UnityEngine;

namespace Redeev.PrototypingTools
{
    public class PlayerAnimationsPrototype : MonoBehaviour
    {
        private Animator animator;
        private RuntimeAnimatorController defaultController;

        private static readonly string NO_MOVE_TAG = "NoMove";

        private static readonly int _attack = Animator.StringToHash("Attack");
        private static readonly int _random = Animator.StringToHash("Random");
        private static readonly int _attackID = Animator.StringToHash("AttackID");
        private static readonly int _attackSpeed = Animator.StringToHash("AttackSpeed");
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _inputMovement = Animator.StringToHash("InputMovement");
        private static readonly int _hit = Animator.StringToHash("Hit");
        private static readonly int _isChanneling = Animator.StringToHash("isChanneling");
        private static readonly int _isFalling = Animator.StringToHash("isFalling");
        private static readonly int _landing = Animator.StringToHash("Landing");
        private static readonly int _dead = Animator.StringToHash("Dead");
        private static readonly int _revive = Animator.StringToHash("Revive");
        private static readonly int _interact = Animator.StringToHash("Interact");
        private static readonly int _pray = Animator.StringToHash("Pray");
        private static readonly int _stopPray = Animator.StringToHash("StopPray");
        private static readonly int _unequip1H = Animator.StringToHash("Unequip1H");
        private static readonly int _unequip2H = Animator.StringToHash("Unequip2H");
        private static readonly int _equip1H = Animator.StringToHash("Equip1H");
        private static readonly int _equip2H = Animator.StringToHash("Equip2H");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            defaultController = animator.runtimeAnimatorController;
        }

        public void Attack(int attackID)
        {
            animator.SetInteger(_random, Random.Range(0, 2));
            animator.SetInteger(_attackID, attackID);
            animator.SetTrigger(_attack);
        }

        public void ResetAttack()
        {
            animator.ResetTrigger(_attack);
        }

        public void SetSpeed(float value)
        {
            animator.SetFloat(_speed, value);
        }

        public void SetMovement(bool value)
        {
            animator.SetBool(_inputMovement, value);
        }

        public void SetAttackSpeed(float value)
        {
            animator.SetFloat(_attackSpeed, value);
        }

        public void StartChanneling(int attackID)
        {
            animator.SetInteger(_attackID, attackID);
            animator.SetBool(_isChanneling, true);
        }

        public void StopChanneling()
        {
            animator.SetBool(_isChanneling, false);
        }

        public void StartPray()
        {
            animator.ResetTrigger(_stopPray);
            animator.SetTrigger(_pray);
        }

        public void StopPray()
        {
            animator.SetTrigger(_stopPray);
        }

        public void Interact()
        {
            animator.SetTrigger(_interact);
        }

        public void Die()
        {
            animator.SetBool(_dead, true);
        }

        public void Falling(bool value)
        {
            animator.SetBool(_isFalling, value);
        }

        public void Landing()
        {
            animator.SetTrigger(_landing);
        }

        public void Revive()
        {
            animator.SetBool(_dead, false);
            animator.SetTrigger(_revive);
        }

        public void Hit()
        {
            animator.SetTrigger(_hit);
        }

        public void Equip1H()
        {
            animator.SetTrigger(_equip1H);
        }

        public void Equip2H()
        {
            animator.SetTrigger(_equip2H);
        }

        public void Unequip1H()
        {
            animator.SetTrigger(_unequip1H);
        }

        public void Unequip2H()
        {
            animator.SetTrigger(_unequip2H);
        }

        public int GetActiveLayer()
        {
            int count = animator.layerCount;
            for (int i = 1; i < count; i++)
            {
                if (animator.GetLayerWeight(i) == 1) return i;
            }
            return 0;
        }

        public bool IsNoMoveState()
        {
            return animator ? animator.GetCurrentAnimatorStateInfo(0).IsTag(NO_MOVE_TAG) : false;
        }

        #region Controller

        public void SetController(RuntimeAnimatorController controller)
        {
            animator.runtimeAnimatorController = controller;
        }

        public void ResetController()
        {
            AnimatorOverrideController controller = new AnimatorOverrideController(defaultController);
            SetController(controller);
        }

        #endregion
    }
}