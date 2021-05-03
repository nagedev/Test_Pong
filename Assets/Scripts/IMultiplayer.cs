using UnityEngine;

namespace Pong
{
    public interface IMultiplayer
    {
        void RegisterSyncedObject(GameObject obj);
    }
}