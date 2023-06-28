using UnityEngine;

namespace Gameplay.Web
{
    public interface IBullet
    {
        GameObject GameObject { get; }
        int Id { get; }
    }
}