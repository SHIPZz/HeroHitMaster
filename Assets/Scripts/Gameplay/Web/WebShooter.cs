using Zenject;

namespace Gameplay.Web
{
    public class WebShooter : Weapon.Weapon, IInitializable
    {
        public void Initialize()
        {
            BulletMovement = new WebMovement();
        }
    }
}