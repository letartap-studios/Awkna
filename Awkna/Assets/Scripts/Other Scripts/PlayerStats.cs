using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton

    private static PlayerStats _instance;

    public static PlayerStats Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    //private static PlayerStats instance;
    //public static PlayerStats Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //            instance = FindObjectOfType<PlayerStats>();
    //        return instance;
    //    }
    //}

    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
    #endregion

    #region Variables and Getters

    [SerializeField]
    private float health = 3f;
    [SerializeField]
    private float maxHealth = 3f;
    [SerializeField]
    private float maxTotalHealth = 6f;
    [SerializeField]
    private int bombsNumber = 3;
    [SerializeField]
    private int gemNumber = 0;
    [SerializeField]
    private float ropeMaxCastDistance = 5f;
    
    /// <summary>
    /// The current health of the player.
    /// </summary>
    public float Health { get { return health; } }
    /// <summary>
    /// The current maximum health the player can have.
    /// Can be increased if the player collects a heart.
    /// </summary>
    public float MaxHealth { get { return maxHealth; } }
    /// <summary>
    /// The maximum health the player can gather.
    /// If he reached the the maximum, he can no longer increase his life by collecting hearts. 
    /// </summary>
    public float MaxTotalHealth { get { return maxTotalHealth; } }
    /// <summary>
    /// The current number of bombs.
    /// </summary>
    public int BombsNumber { get { return bombsNumber; } }
    /// <summary>
    /// The current number of gems.
    /// </summary>
    public int GemNumber { get { return gemNumber; } }
    /// <summary>
    /// The maximum distance at whitch the player cand fire the grappling hook.
    /// </summary>
    public float RopeMaxDistance { get { return ropeMaxCastDistance; } }

    #endregion

    #region Functions
    /// <summary>
    /// Reset the stats to the initial stats.
    /// </summary>
    public void ResetStats()
    {
        health = 3f;
        maxHealth = 3f;
        maxTotalHealth = 6f;
        bombsNumber = 3;
        gemNumber = 0;
        ropeMaxCastDistance = 5f;
    }

    /// <summary>
    /// Heal the player by an amount.
    /// </summary>
    /// <param name="amount">Heal the player by this amount.</param>
    public void Heal(float amount)
    {
        this.health += amount;
        ClampHealth();
    }
    /// <summary>
    /// The player takes damage by an amount,
    /// play a TakingDamage animation , the camera shakes, and the rope gets cut.
    /// </summary>
    /// <param name="dmg">The amount of damage the player takes.</param>
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        FindObjectOfType<AudioManager>().Play("damagetaken");//play sound

        PlayerController.Instance.Animator.SetTrigger("getDamaged");

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeController>().Shake();

        GameObject.FindWithTag("Player").GetComponent<RopeSystem>().ResetRope();

        ClampHealth();
    }
    /// <summary>
    /// Add one health to the players stats.
    /// </summary>
    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health += 1;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }
    private void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
    /// <summary>
    /// Add a bomb to the players inventory.
    /// </summary>
    public void AddBomb()   // When the player finds a bomb add it to the inventory.
    {
        bombsNumber++;
    }
    /// <summary>
    /// Remove a bomb from the players inventory (for when the player uses a bomb).
    /// </summary>
    public void LoseBomb()
    {
        bombsNumber--;
    }
    /// <summary>
    /// Add a gem to player inventory (for when the player finds a gem).
    /// </summary>
    public void AddGem()
    {
        gemNumber++;
    }
    /// <summary>
    /// Remove gems from the players inventory. Show a dialogue box if he doesn't have enough gems.
    /// </summary>
    /// <param name="gemsToPay">Number of gems to pay.</param>
    public void PayGems(int gemsToPay)
    {
        if (gemsToPay > gemNumber)
        {
            /* Dialogue box here that says "Not enough gems!" */
            return;
        }
        else
        {
            gemNumber -= gemsToPay;
        }
    }
    /// <summary>
    /// Increase the ropes length.
    /// </summary>
    /// <param name="x">How much the rope increases.</param>
    public void AddRope(float x)
    {
        ropeMaxCastDistance += x;
    }
    #endregion
}
