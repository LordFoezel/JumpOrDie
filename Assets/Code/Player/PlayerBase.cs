using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBase : PlayerAbilities
{
    bool touchBottom = false;
    bool touchLeft = false;
    bool touchRight = false;
    bool isReady = false;
    bool isAlive = true;
    bool faceLeft = false;
    bool isInteracting = false;
#nullable enable
    GameObject? focusObject;
#nullable disable
    readonly float healthBarWidth = 200f;
    GameObject playerObject;
    Rigidbody2D rigidbody2D;
    GameObject camera;
    PlayerInput playerInput;
    RectTransform healthBarRect;
    Animator animator;
    public LevelBaseLoader levelLoader;
    public InventoryBase inventory;

    public void Tick()
    {
        PauseGame();
        if (!isReady) return;
        ControlePlayer();
    }

    #region <<< Health >>>

    public void getDamage(int damage)
    {
        if ((hitPoints - damage) <= 0)
        {
            PlayerDie();
            hitPoints = 0;
        }
        else hitPoints -= damage;
        healthBarRect.sizeDelta = new Vector2(UtilHealthBarPercent.getSizeOfHealthBar(hitPoints, hitPointsMax, healthBarWidth), 20f);
    }

    public void getHealing(int healing)
    {
        if ((hitPoints + healing) >= hitPointsMax) hitPoints = hitPointsMax;
        else hitPoints += healing;
        healthBarRect.sizeDelta = new Vector2(UtilHealthBarPercent.getSizeOfHealthBar(hitPoints, hitPointsMax, healthBarWidth), 20f);
    }

    public int GetHealth()
    {
        return hitPoints;
    }

    #endregion

    #region <<< PlayerController >>>

    private void ControlePlayer()
    {
        if (!isAlive) return;
        Interact();
        if (!isInteracting)
        {
            Move();
            Jump();
        }
    }

    private void PauseGame()
    {
        if (playerInput.actions.FindAction("Pause").ReadValue<float>() == 1) levelLoader.PauseGame();
    }

    private void Interact()
    {
        if (playerInput.actions.FindAction("Interaction").ReadValue<float>() == 1)
        {
            isInteracting = true;
            if (!focusObject) return;
            TrapSwitch trapSwitch = focusObject.GetComponent<TrapSwitch>();
            if (trapSwitch) trapSwitch.Switch();
        }
        else
        {
            isInteracting = false;
        }
    }

    private void PlayerDie()
    {
        animator.SetTrigger("die");
        isAlive = false;
        Debug.Log("Bevore");
        UtilWait.WaitSeconds(10);
        Debug.Log("after");
        animator.SetBool("isDead", true);
    }

    private void Move()
    {
        float move = playerInput.actions.FindAction("Move").ReadValue<float>();
        if (touchLeft && move < 0) move = 0;
        if (touchRight && move > 0) move = 0;
        rigidbody2D.velocity = new Vector2(move * moveSpeed, rigidbody2D.velocity.y);
        GameObject character = playerObject.transform.Find("Character").gameObject;
        if (move > 0)
        {
            if (faceLeft)
            {
                character.transform.localScale = new Vector3(character.transform.localScale.x * -1, character.transform.localScale.y, character.transform.localScale.z);
                faceLeft = false;
            }
        }
        if (move < 0)
        {
            if (!faceLeft)
            {
                character.transform.localScale = new Vector3(character.transform.localScale.x * -1, character.transform.localScale.y, character.transform.localScale.z);
                faceLeft = true;
            }
        }
        if (move != 0) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
    }

    private void Jump()
    {
        float jump = playerInput.actions.FindAction("Jump").ReadValue<float>();
        if (jump <= 0) return;
        if (touchBottom) animator.SetBool("isJumping", false);
        if (!touchBottom) return;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 1 * jumpingHeight);
        touchBottom = false;
        animator.SetTrigger("jump");
        animator.SetBool("isJumping", true);
    }

    #endregion

    #region <<< Loaders >>>

    public void InitPlayer(int coins = 0, bool canCollectCoins = true)
    {
        LoadPlayer();
        LoadRigitBody2D();
        LoadCamera();
        LoadColliders();
        LoadInputActions();
        LoadCanvas();
        LoadInventory(coins, canCollectCoins);
        isReady = true;
    }

    private void LoadInventory(int coins, bool canCollectCoins)
    {
        inventory = new InventoryBase(coins, canCollectCoins);
    }

    public InventoryBase GetInventory()
    {
        return inventory;
    }

    private void LoadCanvas()
    {
        GameObject healthCanvasObject = new GameObject("HealthCanvas");
        Canvas healthCanvas = healthCanvasObject.AddComponent<Canvas>();
        healthCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject healthBarBackground = new GameObject("HealthBarBackground");
        healthBarBackground.transform.parent = healthCanvas.transform;
        Image healthBarImageBackground = healthBarBackground.AddComponent<Image>();
        healthBarImageBackground.color = Color.gray;
        RectTransform healthBarRectBackground = healthBarImageBackground.GetComponent<RectTransform>();
        healthBarRectBackground.localPosition = new Vector3(65f, 30f, 0f);
        healthBarRectBackground.sizeDelta = new Vector2(healthBarWidth, 20f);
        healthBarRectBackground.anchorMin = new Vector2(0f, 0f);
        healthBarRectBackground.anchorMax = new Vector2(0f, 0f);
        healthBarRectBackground.pivot = new Vector2(0f, 0f);

        GameObject healthBar = new GameObject("HealthBar");
        healthBar.transform.parent = healthCanvas.transform;
        Image healthBarImage = healthBar.AddComponent<Image>();
        healthBarImage.color = Color.red;
        healthBarRect = healthBarImage.GetComponent<RectTransform>();
        healthBarRect.localPosition = new Vector3(65f, 30f, 0f);
        healthBarRect.sizeDelta = new Vector2(UtilHealthBarPercent.getSizeOfHealthBar(hitPoints, hitPointsMax, healthBarWidth), 20f);
        healthBarRect.anchorMin = new Vector2(0f, 0f);
        healthBarRect.anchorMax = new Vector2(0f, 0f);
        healthBarRect.pivot = new Vector2(0f, 0f);
    }

    private void LoadPlayer()
    {
        GameObject prefab = Resources.Load<GameObject>(filename);
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        playerObject = levelLoader.PrefabInstantiate(prefab);
        playerObject.AddComponent<PlayerState>().SetId(id);
        Transform playerStart = GameObject.FindGameObjectWithTag("PlayerStart").transform;
        playerObject.transform.position = new Vector3(playerStart.position.x, playerStart.position.y, 0);
        animator = playerObject.transform.Find("Character").gameObject.GetComponent<Animator>();
    }

    private void LoadRigitBody2D()
    {
        rigidbody2D = playerObject.AddComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;
        rigidbody2D.gravityScale = 5;
    }

    private void LoadCamera()
    {
        camera = UtilCamera.CreateCamera(playerObject);
    }

    private void LoadInputActions()
    {
        playerInput = playerObject.AddComponent<PlayerInput>();
        playerInput.defaultActionMap = "Game";
        InputMaster iputMaster = new InputMaster();
        playerInput.actions = iputMaster.asset;
        playerInput.ActivateInput();
    }

    private void LoadColliders()
    {
        GameObject hitbox = playerObject.transform.Find("Hitbox").gameObject;
        UtilColliderEvents hitboxCollider = hitbox.AddComponent<UtilColliderEvents>();
        hitboxCollider.OnColliderEnterEvent += HandleCollisionHitboxEnter;
        hitboxCollider.OnColliderExitEvent += HandleCollisionHitboxExit;

        GameObject leftCollider = playerObject.transform.Find("Left").gameObject;
        UtilColliderEvents leftPlayerCollider = leftCollider.AddComponent<UtilColliderEvents>();
        leftPlayerCollider.OnCollisionEnterEvent += HandleCollisionLeftEnter;
        leftPlayerCollider.OnCollisionExitEvent += HandleCollisionLeftExit;

        GameObject rightCollider = playerObject.transform.Find("Right").gameObject;
        UtilColliderEvents rightPlayerCollider = rightCollider.AddComponent<UtilColliderEvents>();
        rightPlayerCollider.OnCollisionEnterEvent += HandleCollisionRightEnter;
        rightPlayerCollider.OnCollisionExitEvent += HandleCollisionRightExit;

        GameObject bottomCollider = playerObject.transform.Find("Bottom").gameObject;
        UtilColliderEvents bottomPlayerCollider = bottomCollider.AddComponent<UtilColliderEvents>();
        bottomPlayerCollider.OnCollisionStayEvent += HandleCollisionBottom;
    }

    #endregion

    #region  <<< Helpers >>>

    private void HandleCollisionBottom(GameObject gameObject, Collision2D collision)
    {
        if (collision.gameObject)
        {
            touchBottom = true;
            animator.SetBool("isFlying", false);
            animator.SetBool("isJumping", false);
        }
        else
        {
            touchBottom = false;
            animator.SetBool("isFlying", true);
        }
    }

    private void HandleCollisionHitboxEnter(GameObject gameObject, Collider2D Collider)
    {
        focusObject = Collider.gameObject;
    }

    private void HandleCollisionHitboxExit(GameObject gameObject, Collider2D Collider)
    {
        focusObject = null;
    }

    private void HandleCollisionLeftEnter(GameObject gameObject, Collision2D collision)
    {
        touchLeft = true;
    }

    private void HandleCollisionLeftExit(GameObject gameObject, Collision2D collision)
    {
        touchLeft = false;
    }

    private void HandleCollisionRightEnter(GameObject gameObject, Collision2D collision)
    {
        touchRight = true;
    }

    private void HandleCollisionRightExit(GameObject gameObject, Collision2D collision)
    {
        touchRight = false;
    }

    #endregion
}
