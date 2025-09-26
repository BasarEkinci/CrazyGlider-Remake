using _GameFolders.Scripts.Extensions;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private void Start()
        {
            GameEventManager.RaiseLevelStart();
        }
    }
}