using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pong
{
    public sealed class InputProvider : IInputProvider
    {
        public bool IsTouched => Input.GetMouseButton(0) && !IsPointerOverUIObject();
        public Vector2 TouchPosition => Input.mousePosition;
        
        ///Because built-in method doesn't work correctly on mobile
        private bool IsPointerOverUIObject() 
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}