using DevRowInteractive.SelectionManagement;
using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts
{
    public abstract class WorldObject : MonoBehaviour, ISelectable
    {
        public string Name;
        public Sprite Icon;
        private int formerLayer;
        private bool isSelected;
        
        public virtual void Awake()
        {
            formerLayer = gameObject.layer;
            EventManager.OnGameInitialize += Register;
        }

        public virtual void Select()
        {
            isSelected = true;
            SelectionHelpers.SetLayerRecursively(gameObject, 6, "DoNotOutline");
        }

        public virtual void DeSelect()
        {
            isSelected = false;
            SelectionHelpers.SetLayerRecursively(gameObject, formerLayer, "DoNotOutline");
        }

        public virtual void Hover()
        {
            if(isSelected)
                return;
            
            SelectionHelpers.SetLayerRecursively(gameObject, 6, "DoNotOutline");
        }

        public virtual void EndHover()
        {
            if(isSelected)
                return;
            
            SelectionHelpers.SetLayerRecursively(gameObject, formerLayer, "DoNotOutline");
        }

        public virtual void Reset()
        {
            
        }

        public GameObject GetGameObjectReference()
        {
            return gameObject;
        }

        public virtual bool IsMultiSelect() => false;
        public virtual void Register() => GameManager.Instance.SelectionManager.AddSelectableObject(gameObject);
        public virtual void UnRegister() => GameManager.Instance.PlayerSelectables.SelectableObjects.Remove(gameObject);
        public void OnDestroy() => UnRegister();

    }
}
