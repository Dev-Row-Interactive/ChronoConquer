using UnityEngine;

namespace DevRowInteractive.SelectionManagement
{
    public interface ISelectable
    {
        /// <summary>
        /// If the object is hovered & then selected
        /// </summary>
        void Select();
        /// <summary>
        /// If the object is deselected
        /// </summary>
        void DeSelect();
        /// <summary>
        /// If the object is hovered over
        /// </summary>
        void Hover();
        /// <summary>
        /// If the object is not hovered over anymore
        /// </summary>
        void EndHover();

        /// <summary>
        /// Handy method to return a reference to itself as Interfaces are not able to reference it's gameObject
        /// </summary>
        /// <returns></returns>
        void Reset();
        GameObject GetGameObjectReference();
        /// <summary>
        /// Set true if object should be able to be selected by RectangleSelection
        /// </summary>
        /// <returns></returns>
        bool IsMultiSelect();
    }
}