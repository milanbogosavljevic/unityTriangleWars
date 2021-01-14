using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/LevelInfo")]
public class LevelsScriptableObj : ScriptableObject
{
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float enemyLineMoveSpeed;
    [SerializeField] private int playerAmmo;
    [SerializeField] private Vector2[] enemiesPositions;
    [SerializeField] private float[] enemiesMoveSpeed;
    [SerializeField] private bool[] enemiesCanShoot;
    [SerializeField] private int[] enemiesShootingInterval;
    [SerializeField] private int[] enemiesLevel;
    [SerializeField] private float[] enemiesBulletSpeed;
    [SerializeField] private Sprite[] enemiesSkin;

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public Vector2[] GetEnemiesPosition()
    {
        return enemiesPositions;
    }

    public float[] GetEnemiesMoveSpeed()
    {
        return enemiesMoveSpeed;
    }

    public bool[] GetEnemiesCanShoot()
    {
        return enemiesCanShoot;
    }

    public int[] GetEnemiesShootingInterval()
    {
        return enemiesShootingInterval;
    }

    public int[] GetEnemiesLevel()
    {
        return enemiesLevel;
    }

    public float[] GetEnemiesBulletSpeed()
    {
        return enemiesBulletSpeed;
    }

    public Sprite[] GetEnemiesSkin()
    {
        return enemiesSkin;
    }

    public float GetEnemyLineMoveSpeed()
    {
        return enemyLineMoveSpeed;
    }

    public int GetPlayerAmmo()
    {
        return playerAmmo;
    }
}
