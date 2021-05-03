using UnityEngine;

namespace Pong
{
    public interface IInputProvider
    {
        bool IsTouched { get; }
        Vector2 TouchPosition { get; }
    }
}