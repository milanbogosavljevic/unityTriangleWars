using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/LevelInfo")]
public class LevelsScriptableObj : ScriptableObject
{
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float enemyLineMoveSpeed;
    [SerializeField] private int playerAmmo;
    [SerializeField] private bool hasHelpItems;
    [SerializeField] private int playerAmmoToIncrease;
    [SerializeField] private int pauseLineFor;
    [SerializeField] private int pauseEnemiesFor;
    [SerializeField] private int numberOfMeteors;
    [SerializeField] private float meteorsReleaseInterval;
    [SerializeField] private float meteorsMoveSpeed;
    [SerializeField] private float meteorsMoveLineBy;
    [SerializeField] private int meteorPoints;
    [SerializeField] private int[] releaseHelpItemIntervalFromTo;
    [SerializeField] private float[] enemiesYPositionFromTop;
    [SerializeField] private float[] enemiesMoveSpeed;
    [SerializeField] private bool[] enemiesCanShoot;
    [SerializeField] private float[] enemiesShootingInterval;
    [SerializeField] private float[] enemiesMoveLineBy;
    [SerializeField] private float[] enemiesBulletSpeed;
    [SerializeField] private Sprite[] enemiesSkin;
    [SerializeField] private string[] startingMoveDirection;
    [SerializeField] private int[] enemiesPoints;
    [SerializeField] private bool[] enemiesPingPongMove;
    [SerializeField] private bool[] enemiesAlphaAnimation;

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

    public bool[] GetEnemiesPingPongMove()
    {
        return enemiesPingPongMove;
    }

    public bool[] GetEnemiesAlphaAnimation()
    {
        return enemiesAlphaAnimation;
    }

    public int GetPlayerAmmoToIncrease()
    {
        return playerAmmoToIncrease;
    }

    public int GetPauseLineFor()
    {
        return pauseLineFor;
    }

    public int GetPauseEnemiesFor()
    {
        return pauseEnemiesFor;
    }

    public int[] GetReleaseHelpItemsInterval()
    {
        return releaseHelpItemIntervalFromTo;
    }

    public int GetNumberOfMeteors()
    {
        return numberOfMeteors;
    }

    public float GetMeteorsReleaseInterval()
    {
        return meteorsReleaseInterval;
    }

    public float GetMeteorsSpeed()
    {
        return meteorsMoveSpeed;
    }

    public float GetMeteorMoveLineBy()
    {
        return meteorsMoveLineBy;
    }

    public int GetMeteorPoints()
    {
        return meteorPoints;
    }

    public bool HasHelpItems()
    {
        return hasHelpItems;
    }
}
