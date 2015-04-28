using NXT;
using NXT.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace NXT
{
    public class AnimatorMonitor : MonoBehaviour
    {
        [Serializable]
        private class AnimatorStateData
        {
            [SerializeField]
            private string m_Name;
            [SerializeField]
            private float m_TransitionDuration;
            public string Name
            {
                get
                {
                    return this.m_Name;
                }
            }
            public float TransitionDuration
            {
                get
                {
                    return this.m_TransitionDuration;
                }
            }
            public AnimatorStateData(string name, float transitionDuration)
            {
                this.m_Name = name;
                this.m_TransitionDuration = transitionDuration;
            }
        }
        [SerializeField]
        private string m_LowerBodyMainSubstate = "Grounded";
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_IdleState = new AnimatorMonitor.AnimatorStateData("Standing Idle", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_MovingState = new AnimatorMonitor.AnimatorStateData("Standing Movement", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_MovingAimState = new AnimatorMonitor.AnimatorStateData("Standing Aim Movement", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_AimState = new AnimatorMonitor.AnimatorStateData("Aim", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_EquipState = new AnimatorMonitor.AnimatorStateData("Equip", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_UnequipState = new AnimatorMonitor.AnimatorStateData("Unequip", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_ItemUseState = new AnimatorMonitor.AnimatorStateData("Attack", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_ItemReloadState = new AnimatorMonitor.AnimatorStateData("Reload", 0.1f);
        [SerializeField]
        private AnimatorMonitor.AnimatorStateData m_QuickUseState = new AnimatorMonitor.AnimatorStateData("Grenade", 0.1f);
        public static int HorizontalInputHash = Animator.StringToHash("Horizontal Input");
        public static int VerticalInputHash = Animator.StringToHash("Vertical Input");
        public static int PitchHash = Animator.StringToHash("Pitch");
        public static int YawHash = Animator.StringToHash("Yaw");
        public static int StateHash = Animator.StringToHash("State");
        public static int IntDataHash = Animator.StringToHash("Int Data");
        public static int FloatDataHash = Animator.StringToHash("Float Data");
        private SharedProperty<string> m_ItemName;
        private SharedProperty<string> m_ItemLowerAnimatorUseState;
        private Ability[] m_Abilities;
        private Ability[] m_ActiveAbility = new Ability[2];
        private Dictionary<string, int> m_StateNamesHash = new Dictionary<string, int>();
        private GameObject m_GameObject;
        private Animator m_Animator;
        private RigidbodyCharacterController m_Controller;
        public string ItemName
        {
            get
            {
                return this.m_ItemName.Get();
            }
        }
        public string ItemLowerAnimatorUseState
        {
            get
            {
                return this.m_ItemLowerAnimatorUseState.Get();
            }
        }
        private void Awake()
        {
            this.m_GameObject = base.gameObject;
            this.m_Animator = base.GetComponent<Animator>();
            this.m_Controller = base.GetComponent<RigidbodyCharacterController>();
            this.m_Abilities = this.m_Controller.Abilities;
            EventHandler.RegisterEvent(this.m_GameObject, "OnRespawn", new Action(this.OnRespawn));
            EventHandler.RegisterEvent(this.m_GameObject, "OnInventoryInitialized", new Action(this.OnInventoryInitialized));
        }
        private void OnEnable()
        {
            EventHandler.RegisterEvent<bool>(this.m_GameObject, "OnItemUse", new Action<bool>(this.OnUse));
            EventHandler.RegisterEvent(this.m_GameObject, "OnItemReload", new Action(this.OnReload));
            EventHandler.RegisterEvent<bool>(this.m_GameObject, "OnInventoryItemEquipping", new Action<bool>(this.OnItemEquipping));
        }
        private void OnDisable()
        {
            EventHandler.UnregisterEvent<bool>(this.m_GameObject, "OnItemUse", new Action<bool>(this.OnUse));
            EventHandler.UnregisterEvent(this.m_GameObject, "OnItemReload", new Action(this.OnReload));
            EventHandler.UnregisterEvent<bool>(this.m_GameObject, "OnInventoryItemEquipping", new Action<bool>(this.OnItemEquipping));
        }
        private void OnInventoryInitialized()
        {
            SharedManager.InitializeSharedFields(this.m_GameObject, this);
            this.PlayDefaultStates();
            EventHandler.UnregisterEvent(this.m_GameObject, "OnInventoryInitialized", new Action(this.OnInventoryInitialized));
        }
        private void PlayDefaultStates()
        {
            if (this.m_Controller.Aim)
            {
                this.m_Animator.Play(this.GetStateNameHash(this.m_LowerBodyMainSubstate + "." + this.m_MovingAimState.Name), 0);
                this.m_Animator.Play(this.GetStateNameHash(this.m_ItemName.Get() + "." + this.m_AimState.Name), 1);
            }
            else
            {
                this.m_Animator.Play(this.GetStateNameHash(this.m_LowerBodyMainSubstate + "." + this.m_IdleState.Name), 0);
                this.m_Animator.Play(this.GetStateNameHash(this.m_ItemName.Get() + "." + this.m_IdleState.Name), 1);
            }
        }
        public void DetermineStates()
        {
            this.DetermineStates(true);
        }
        public void DetermineStates(bool checkAbilities)
        {
            bool lowerBodyStart = this.DetermineLowerBodyState(checkAbilities);
            this.DetermineUpperBodyState(checkAbilities, lowerBodyStart);
        }
        private bool DetermineLowerBodyState(bool checkAbilities)
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive)
                {
                    string destinationState = this.m_Abilities[i].GetDestinationState(0);
                    if (!checkAbilities || !this.m_Abilities[i].HasAnimatorControl(0))
                    {
                        return this.ChangeAnimatorStates(this.m_Abilities[i], 0, destinationState, this.m_Abilities[i].TransitionDuration, 0f);
                    }
                    if (!string.IsNullOrEmpty(destinationState))
                    {
                        return false;
                    }
                }
            }
            if (!this.m_Controller.Moving)
            {
                return this.ChangeAnimatorStates(null, 0, this.m_LowerBodyMainSubstate + "." + this.m_IdleState.Name, this.m_IdleState.TransitionDuration, 0f);
            }
            if (this.m_Controller.Aim)
            {
                return this.ChangeAnimatorStates(null, 0, this.m_LowerBodyMainSubstate + "." + this.m_MovingAimState.Name, this.m_MovingAimState.TransitionDuration, 0f);
            }
            return this.ChangeAnimatorStates(null, 0, this.m_LowerBodyMainSubstate + "." + this.m_MovingState.Name, this.m_MovingState.TransitionDuration, 0f);
        }
        public bool DetermineUpperBodyState(bool checkAbilities, bool lowerBodyStart)
        {
            if (checkAbilities)
            {
                for (int i = 0; i < this.m_Abilities.Length; i++)
                {
                    if (this.m_Abilities[i].IsActive && this.m_Abilities[i].HasAnimatorControl(1))
                    {
                        return false;
                    }
                }
            }
            if (this.m_Controller.Aim)
            {
                return this.ChangeAnimatorStates(null, 1, this.m_ItemName.Get() + "." + this.m_AimState.Name, this.m_AimState.TransitionDuration, 0f);
            }
            float normalizedTime = ((!lowerBodyStart) ? this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime : 0f) % 1f;
            for (int j = 0; j < this.m_Abilities.Length; j++)
            {
                if (this.m_Abilities[j].IsActive)
                {
                    string destinationState = this.m_Abilities[j].GetDestinationState(1);
                    return this.ChangeAnimatorStates(this.m_Abilities[j], 1, destinationState, this.m_Abilities[j].TransitionDuration, normalizedTime);
                }
            }
            if (this.m_Controller.Moving)
            {
                return this.ChangeAnimatorStates(null, 1, this.m_ItemName.Get() + "." + this.m_MovingState.Name, this.m_MovingState.TransitionDuration, normalizedTime);
            }
            return this.ChangeAnimatorStates(null, 1, this.m_ItemName.Get() + "." + this.m_IdleState.Name, this.m_IdleState.TransitionDuration, normalizedTime);
        }
        private bool ChangeAnimatorStates(Ability ability, int layer, string destinationState, float transitionDuration)
        {
            return (layer != 1 || !this.m_Controller.Aim) && this.ChangeAnimatorStates(ability, layer, destinationState, transitionDuration, 0f);
        }
        public bool ChangeAnimatorStates(Ability ability, int layer, string destinationState, float transitionDuration, float normalizedTime)
        {
            int num = (!this.m_Animator.IsInTransition(layer)) ? this.m_Animator.GetCurrentAnimatorStateInfo(layer).fullPathHash : this.m_Animator.GetNextAnimatorStateInfo(layer).fullPathHash;
            if (!string.IsNullOrEmpty(destinationState) && num != Animator.StringToHash(destinationState))
            {
                float num2 = transitionDuration / this.m_Animator.GetCurrentAnimatorStateInfo(layer).length;
;
                if (float.IsInfinity(num2))
                {
                    num2 = 0f;
                }
                this.m_Animator.CrossFade(this.GetStateNameHash(destinationState), num2, layer, normalizedTime);
                this.m_ActiveAbility[layer] = ability;
                return true;
            }
            return false;
        }
        private int GetStateNameHash(string stateName)
        {
            int num;
            if (this.m_StateNamesHash.TryGetValue(stateName, out num))
            {
                return num;
            }
            num = Animator.StringToHash(stateName);
            this.m_StateNamesHash.Add(stateName, num);
            return num;
        }
        public void ExecuteEvent(string eventName)
        {
            EventHandler.ExecuteEvent(this.m_GameObject, eventName);
        }
        public void ExecuteEventCheckUpperState(string eventName)
        {
            EventHandler.ExecuteEvent(this.m_GameObject, eventName);
            this.DetermineUpperBodyState(true, false);
        }
        public void FootDown(int footIndex)
        {
            EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnAnimatorFootDown", footIndex == 1);
        }
        private void StartAim()
        {
            if (this.m_Animator.IsInTransition(1))
            {
                return;
            }
            EventHandler.ExecuteEvent(this.m_GameObject, "OnAnimatorAiming");
        }
        public void ItemUsed(int primaryItem)
        {
            EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnAnimatorItemUsed", primaryItem == 1);
            this.DetermineStates();
        }
        public void AlignWithCover(int align)
        {
            EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnAnimatorAlignWithCover", align == 1);
        }
        public void PopFromCover(int popped)
        {
            EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnAnimatorPopFromCover", popped == 1);
        }
        private void OnItemEquipping(bool equip)
        {
            if (equip)
            {
                this.ChangeAnimatorStates(null, 1, this.m_EquipState.Name, this.m_EquipState.TransitionDuration, 0f);
            }
            else
            {
                this.ChangeAnimatorStates(null, 1, this.m_UnequipState.Name, this.m_UnequipState.TransitionDuration, 0f);
            }
        }
        private void OnUse(bool primaryItem)
        {
            if (primaryItem)
            {
                if (!string.IsNullOrEmpty(this.m_ItemLowerAnimatorUseState.Get()))
                {
                    this.ChangeAnimatorStates(null, 0, this.m_LowerBodyMainSubstate + "." + this.m_ItemLowerAnimatorUseState.Get(), 0.1f, 0f);
                }
                this.ChangeAnimatorStates(null, 1, this.m_ItemName.Get() + "." + this.m_ItemUseState.Name, this.m_ItemUseState.TransitionDuration, 0f);
            }
            else
            {
                this.ChangeAnimatorStates(null, 1, this.m_QuickUseState.Name, this.m_QuickUseState.TransitionDuration, 0f);
            }
        }
        private void OnReload()
        {
            this.ChangeAnimatorStates(null, 1, this.m_ItemName.Get() + "." + this.m_ItemReloadState.Name, this.m_ItemReloadState.TransitionDuration, 0f);
        }
        private void OnRespawn()
        {
            this.PlayDefaultStates();
        }
    }
}
