using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/LevelInfo")]
public class LevelsScriptableObj : ScriptableObject
{
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float enemyLineMoveSpeed;
    [SerializeField] private int playerAmmo;
    [SerializeField] private float[] enemiesYPositionFromTop;
    [SerializeField] private float[] enemiesMoveSpeed;
    [SerializeField] private bool[] enemiesCanShoot;
    [SerializeField] private float[] enemiesShootingInterval;
    [SerializeField] private float[] enemiesMoveLineBy;
    [SerializeField] private float[] enemiesBulletSpeed;
    [SerializeField] private Sprite[] enemiesSkin;
    [SerializeField] private string[] startingMoveDirection;
    [SerializeField] private int[] enemiesPoints;

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float[] GetEnemiesYPositionFromTop()
    {
        return enemiesYPositionFromTop;
    }

    public float[] GetEnemiesMoveSpeed()
    {
        return enemiesMoveSpeed;
    }

    public bool[] GetEnemiesCanShoot()
    {
        return enemiesCanShoot;
    }

    public float[] GetEnemiesShootingInterval()
    {
        return enemiesShootingInterval;
    }

    public float[] GetEnemiesMoveLineBy()
    {
        return enemiesMoveLineBy;
    }

    public float[] GetEnemiesBulletSpeed()
    {
        return enemiesBulletSpeed;
    }

    public Sprite[] GetEnemiesSkin()
    {
        return enemiesSkin;
    }

    public string[] GetEnemiesStartingDirection()
    {
        return startingMoveDirection;
    }

    public float GetEnemyLineMoveSpeed()
    {
        return enemyLineMoveSpeed;
    }

    public int GetPlayerAmmo()
    {
        return playerAmmo;
    }

    public int[] GetEnemiesPoints()
    {
        return enemiesPoints;
    }
}
