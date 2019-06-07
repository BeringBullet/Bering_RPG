using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] bool isRightHanded = true;

        public float WeaponRange => weaponRange;
        public float WeaponDamage => weaponDamage;
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab == null) return;
            Instantiate(equippedPrefab, isRightHanded ? rightHand: leftHand);
            if (animatorOverride == null) return;
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}
