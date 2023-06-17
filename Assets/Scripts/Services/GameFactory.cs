using UnityEngine;

public class GameFactory
{
    private readonly Player.Factory _playerFactory;
    private readonly PlayerCameraFollower.Factory _cameraFollowerFactory;

    public GameFactory(Player.Factory playerFactory, PlayerCameraFollower.Factory cameraFollowerFactory)
    {
        _playerFactory = playerFactory;
        _cameraFollowerFactory = cameraFollowerFactory;
    }

    public PlayerCameraFollower CreateCameraFollower(Player player) =>
        _cameraFollowerFactory.Create(player);

    public Player CreatePlayer(Vector3 at) => 
        _playerFactory.Create(at);
}