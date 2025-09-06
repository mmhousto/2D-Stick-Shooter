using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    private GetPlayerInput _playerInput;
    public GameObject bulletPrefab;
    public GameObject rightFacingDirection;
    public GameObject leftFacingDirection;
    public GameObject bulletRightSpawn;
    public GameObject bulletLeftSpawn;
    public SpriteRenderer leftWeapon;
    public SpriteRenderer rightWeapon;

    private AudioSource playerSoundEffects;
    private BulletPool bulletPool;
    private Animator rightWeaponAnim;
    private Animator leftWeaponAnim;

    private Vector3 posSpawnLocation = new Vector3(0.48f, 0.16f, 0);
    private Vector3 negSpawnLocation = new Vector3(0.48f, -0.16f, 0);
    private float shotStrength = 25f;
    private static float fireTime = 0.35f;
    private float leftTime;
    private float rightTime;
    private bool canFire;
    private bool canLeftFire;

    // Start is called before the first frame update
    void Start()
    {
        playerSoundEffects = GetComponent<AudioSource>();
        bulletPool = BulletPool.instance;

        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;

        _playerInput = GetComponent<GetPlayerInput>();
        rightWeaponAnim = rightWeapon.GetComponent<Animator>();
        leftWeaponAnim = leftWeapon.GetComponent<Animator>();
        canFire = true;
        canLeftFire = true;
        leftTime = 0;
        rightTime = 0;
        fireTime = 0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.isPaused) return;
        RotateWeapons();

        CheckWeaponDirection();

        CheckIfLeftFiring();
        CheckIfFiring();
    }

    void RotateWeapons()
    {
        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;
        if (_playerInput.look == Vector2.zero)
            rightFacingDirection.transform.rotation = leftFacingDirection.transform.rotation;
        else
            rightFacingDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromVector(_playerInput.look)));

        if (_playerInput.move != Vector2.zero)
            leftFacingDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromVector(_playerInput.move)));
    }

    void CheckWeaponDirection()
    {
        if (!IsOwner && MainManager.players == MainManager.Players.Coop) return;
        if (leftFacingDirection.transform.localRotation.z >= .75f && leftWeapon.flipY == false)
        {
            leftWeapon.flipY = true;
            bulletLeftSpawn.transform.localPosition = negSpawnLocation;
        }
        else if (leftFacingDirection.transform.localRotation.z < .75 && leftWeapon.flipY == true)
        {
            leftWeapon.flipY = false;
            bulletLeftSpawn.transform.localPosition = posSpawnLocation;
        }

        if (rightFacingDirection.transform.localRotation.z >= .75 && rightWeapon.flipY == false)
        {
            rightWeapon.flipY = true;
            bulletRightSpawn.transform.localPosition = negSpawnLocation;
        }
        else if (rightFacingDirection.transform.localRotation.z < .75 && rightWeapon.flipY == true)
        {
            rightWeapon.flipY = false;
            bulletRightSpawn.transform.localPosition = posSpawnLocation;
        }
    }

    void CheckIfLeftFiring()
    {
        if (leftTime <= 0)
        {
            leftTime = 0;
            canLeftFire = true;
        } else
        {
            leftTime -= Time.deltaTime;
            canLeftFire = false;
        }

        if (IsOwner && _playerInput.isLeftFiring == true && canLeftFire == true)
        {
            leftWeaponAnim.SetTrigger("Fire");
            Invoke(nameof(FireLeftProjectileRpc), 0.1f);
            canLeftFire = false;
            leftTime = fireTime;
        }
        else if (MainManager.players == MainManager.Players.Solo && _playerInput.isLeftFiring == true && canLeftFire == true)
        {
            leftWeaponAnim.SetTrigger("Fire");
            Invoke(nameof(FireLeftProjectile), 0.1f);
            canLeftFire = false;
            leftTime = fireTime;
        }
    }

    void CheckIfFiring()
    {
        if (rightTime <= 0)
        {
            rightTime = 0;
            canFire = true;
        }
        else
        {
            rightTime -= Time.deltaTime;
            canFire = false;
        }

        if (IsOwner && _playerInput.isFiring == true && canFire == true)
        {
            rightWeaponAnim.SetTrigger("Fire");
            Invoke(nameof(FireProjectileRpc), 0.1f);
            canFire = false;
            rightTime = fireTime;
        }
        else if (MainManager.players == MainManager.Players.Solo && _playerInput.isFiring == true && canFire == true)
        {
            rightWeaponAnim.SetTrigger("Fire");
            Invoke(nameof(FireProjectile), 0.1f);
            canFire = false;
            rightTime = fireTime;
        }
    }

    void FireProjectile()
    {
        playerSoundEffects.Play();
        var clone = bulletPool.bulletPool.Get();
        clone.transform.position = bulletRightSpawn.transform.position;
        clone.transform.rotation = bulletRightSpawn.transform.rotation;
        //GameObject clone = Instantiate(bulletPrefab, bulletRightSpawn.transform.position, rightFacingDirection.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * shotStrength, ForceMode2D.Impulse);
    }

    void FireLeftProjectile()
    {
        playerSoundEffects.Play();
        var clone = bulletPool.bulletPool.Get();
        clone.transform.position = bulletLeftSpawn.transform.position;
        clone.transform.rotation = bulletLeftSpawn.transform.rotation;
        //GameObject clone = Instantiate(bulletPrefab, bulletLeftSpawn.transform.position, leftFacingDirection.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * shotStrength, ForceMode2D.Impulse);
    }

    [Rpc(SendTo.Everyone)]
    void FireProjectileRpc()
    {
        playerSoundEffects = GetComponent<AudioSource>();
        if(playerSoundEffects != null)
            playerSoundEffects.Play();

        var clone = bulletPool.bulletPool.Get();
        clone.transform.position = bulletRightSpawn.transform.position;
        clone.transform.rotation = bulletRightSpawn.transform.rotation;
        //GameObject clone = Instantiate(bulletPrefab, bulletRightSpawn.transform.position, rightFacingDirection.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * shotStrength, ForceMode2D.Impulse);
    }

    [Rpc(SendTo.Everyone)]
    void FireLeftProjectileRpc()
    {
        playerSoundEffects = GetComponent<AudioSource>();
        if (playerSoundEffects != null) 
            playerSoundEffects.Play();

        var clone = bulletPool.bulletPool.Get();
        clone.transform.position = bulletLeftSpawn.transform.position;
        clone.transform.rotation = bulletLeftSpawn.transform.rotation;
        //GameObject clone = Instantiate(bulletPrefab, bulletLeftSpawn.transform.position, leftFacingDirection.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * shotStrength, ForceMode2D.Impulse);
    }

    public float GetAngleFromVector(Vector2 vector)
    { 
        vector = vector.normalized;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        return (angle < 0) ? angle + 360 : angle;
    }

    public static void IncreaseFireRate()
    {
        fireTime = fireTime - fireTime * 0.1f;
        Time.timeScale = 1f;
    }
}
