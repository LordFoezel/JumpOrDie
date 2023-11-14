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

    public void Tick()
    {
        if (!isReady) return;
        if (!isAlive) return;
        ControlePlayer();
    }

    #region <<< Health >>>

    public void getDamage(int damage)
    {
        if ((hitPoints - damage) <= 0)
        {
            isAlive = false;
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

    #endregion

    #region <<< PlayerController >>>

    private void ControlePlayer()
    {
        PauseGame();
        Interact();
        if (!isInteracting)
        {
            Move();
            Jump();
        }
    }

    private void PauseGame()
    {
        // if (playerInput.actions.FindAction("Pause").ReadValue<float>() != 1) return;
        if (Input.GetKeyDown(KeyCode.Escape)) levelLoader.PauseGame();
    }

    private void Interact()
    {
        // if (playerInput.actions.FindAction("Interaction").ReadValue<float>() != 1)
        // {
        //     isInteracting = false;
        //     return;
        // }
        if (Input.GetKey(KeyCode.F))
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

    private void Move()
    {
        // float move = playerInput.actions.FindAction("Move").ReadValue<float>();
        float move = Input.GetAxis("Horizontal");
        if (touchLeft && move < 0) move = 0;
        if (touchRight && move > 0) move = 0;
        rigidbody2D.velocity = new Vector2(move * moveSpeed, rigidbody2D.velocity.y);
        GameObject character = playerObject.transform.Find("Character").gameObject;
        if (move > 0) character.transform.rotation = new Quaternion(character.transform.rotation.x, 0, character.transform.rotation.z, character.transform.rotation.w);
        if (move < 0) character.transform.rotation = new Quaternion(character.transform.rotation.x, 180, character.transform.rotation.z, character.transform.rotation.w);
        if (move != 0) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
    }

    private void Jump()
    {
        // float jump = playerInput.actions.FindAction("Jump").ReadValue<float>();
        // if (jump <= 0) return;
        bool jump = Input.GetKey(KeyCode.Space);
        if(!jump) return;
        if (touchBottom) animator.SetBool("isJumping", false);
        if (!touchBottom) return;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 1 * jumpingHeight);
        touchBottom = false;
        animator.SetTrigger("jump");
        animator.SetBool("isJumping", true);
    }

    #endregion

    #region <<< Loaders >>>

    public void InitPlayer()
    {
        LoadPlayer();
        LoadRigitBody2D();
        LoadCamera();
        LoadColliders();
        // LoadInputActions();
        LoadCanvas();
        isReady = true;
    }

    private void LoadCanvas()
    {
        GameObject healthCanvasObject = new GameObject("HealthCanvas");
        Canvas healthCanvas = healthCanvasObject.AddComponent<Canvas>();
        healthCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject healthBarBackground = new GameObject("HealthBarBackground");
        healthBarBackground.transform.parent = healthCanvas.transform;
        Image healthBarImageBackground = healthBarBackground.AddComponent<Image>();
        healthBarImageBackground.color = Color.black;
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

    // private void LoadInputActions()
    // {
    //     playerInput = playerObject.AddComponent<PlayerInput>();
    //     playerInput.defaultActionMap = "Game";
    //     InputMaster iputMaster = new InputMaster();
    //     playerInput.actions = iputMaster.asset;
    //     playerInput.ActivateInput();
    // }

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
