using System.Collections.Generic;
using CodeBase.Gameplay.Character.Enemy;

namespace CodeBase.Services.Providers
{
    public class EnemyProvider : IEnemyProvider
    {
        public List<Enemy> Enemies { get; set; } = new();
    }

    public interface IEnemyProvider
    {
        List<Enemy> Enemies { get; }

    }
}