using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Game independent Power Up logic supporting 2D and 3D modes.
/// When collected, a Power Up has visuals switched off, but the Power Up gameobject exists until it is time for it to expire
/// Subclasses of this must:
/// 1. Implement PowerUpPayload()
/// 2. Optionally Implement PowerUpHasExpired() to remove what was given in the payload
/// 3. Call PowerUpHasExpired() when the power up has expired or tick ExpiresImmediately in inspector
/// </summary>
public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public string powerUpExplanation;
    public string powerUpQuote;
    [Tooltip("Tick true for power ups that are instant use, eg a health addition that has no delay before expiring")]
    public bool expiresImmediately;
    public GameObject specialEffect;
    public AudioClip soundEffect;

    /// <summary>
    /// It is handy to keep a reference to the player that collected us
    /// </summary>
    protected PlayerController playerController;

    protected SpriteRenderer spriteRenderer;

    protected enum PowerUpState
    {
        InAttractMode,
        IsCollected,
        IsExpiring
    }

    protected PowerUpState powerUpState;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        powerUpState = PowerUpState.InAttractMode;
    }

    /// <summary>
    /// 2D support
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        PowerUpCollected(other.gameObject);
    }

    /// <summary>
    /// 3D support
    /// </summary>
    protected virtual void Update()
    {
        PowerUpCollected(other.gameObject);
    }

    protected virtual void PowerUpCollected(GameObject gameObjectCollectingPowerUp)
    {
        // We only care if we've been collected by the player
        if (gameObjectCollectingPowerUp.tag != "Player")
        {
            return;
        }

        // We only care if we've not been collected before
        if (powerUpState == PowerUpState.IsCollected || powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsCollected;

        // We must have been collected by a player, store handle to player for later use      
        playerController = gameObjectCollectingPowerUp.GetComponent<PlayerController>();

        // We move the power up game object to be under the player that collect it, this isn't essential for functionality 
        // presented so far, but it is neater in the gameObject hierarchy
        gameObject.transform.parent = playerController.gameObject.transform;
        gameObject.transform.position = playerController.gameObject.transform.position;

        // Collection effects
        PowerUpEffects();

        // Payload      
        PowerUpPayload();

        // Send message to any listeners
        //foreach (GameObject go in EventSystemListeners.main.listeners)
        //{
        //    ExecuteEvents.Execute<IPowerUpEvents>(go, null, (x, y) => x.OnPowerUpCollected(this, playerController));
        //}

        // Now the power up visuals can go away
        spriteRenderer.enabled = false;
    }

    protected virtual void PowerUpEffects()
    {
        if (specialEffect != null)
        {
            Instantiate(specialEffect, transform.position, transform.rotation, transform);
        }

        //if (soundEffect != null)
        //{
        //    MainGameController.main.PlaySound(soundEffect);
        //}
    }

    protected virtual void PowerUpPayload()
    {
        Debug.Log("Power Up collected, issuing payload for: " + gameObject.name);

        // If we're instant use we also expire self immediately
        if (expiresImmediately)
        {
            PowerUpHasExpired();
        }
    }

    protected virtual void PowerUpHasExpired()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsExpiring;

        // Send message to any listeners
        //foreach (GameObject go in EventSystemListeners.main.listeners)
        //{
        //    ExecuteEvents.Execute<IPowerUpEvents>(go, null, (x, y) => x.OnPowerUpExpired(this, playerController));
        //}
        Debug.Log("Power Up has expired, removing after a delay for: " + gameObject.name);
        DestroySelfAfterDelay();
    }

    protected virtual void DestroySelfAfterDelay()
    {
        // Arbitrary delay of some seconds to allow particle, audio is all done
        // TODO could tighten this and inspect the sfx? Hard to know how many, as subclasses could have spawned their own
        Destroy(gameObject, 10f);
    }
}

