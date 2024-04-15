using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerInput _playerInput;
    public GameObject bulletPrefab;
    public GameObject rightFacingDirection;
    public GameObject leftFacingDirection;
    public GameObject bulletRightSpawn;
    public GameObject bulletLeftSpawn;
    public SpriteRenderer leftWeapon;
    public SpriteRenderer rightWeapon;

    private float shotStrength = 25f;
    private float fireTime = 0.35f;
    private float leftTime;
    private float rightTime;
    private bool canFire;
    private bool canLeftFire;

    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        canFire = true;
        canLeftFire = true;
        leftTime = 0;
        rightTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rightFacingDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromVector(_playerInput.look)));
        leftFacingDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngleFromVector(_playerInput.move)));

        CheckWeaponDirection();

        CheckIfLeftFiring();
        CheckIfFiring();
    }

    void CheckWeaponDirection()
    {
        if (leftFacingDirection.transform.localRotation.z >= .75f && leftWeapon.flipY == false)
        {
            leftWeapon.flipY = true;
        }
        else if (leftFacingDirection.transform.localRotation.z < .75 && leftWeapon.flipY == true)
        {
            leftWeapon.flipY = false;
        }

        if(rightFacingDirection.transform.localRotation.z >= .75 && rightWeapon.flipY == false)
        {
            rightWeapon.flipY = true;
        }else if (rightFacingDirection.transform.localRotation.z < .75 && rightWeapon.flipY == true)
        {
            rightWeapon.flipY = false;
        }
    }

    void CheckIfLeftFiring()
    {
        if(leftTime <= 0)
        {
            leftTime = 0;
            canLeftFire = true;
        }else
        {
            leftTime -= Time.deltaTime;
            canLeftFire = false;
        }

        if (_playerInput.isLeftFiring == true && canLeftFire == true)
        {
            FireLeftProjectile();
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

        if (_playerInput.isFiring == true && canFire == true)
        {
            FireProjectile();
            canFire = false;
            rightTime = fireTime;
        }
    }

    void FireProjectile()
    {
        GameObject clone = Instantiate(bulletPrefab, bulletRightSpawn.transform.position, rightFacingDirection.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * shotStrength, ForceMode2D.Impulse);
    }

    void FireLeftProjectile()
    {
        GameObject clone = Instantiate(bulletPrefab, bulletLeftSpawn.transform.position, leftFacingDirection.transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * shotStrength, ForceMode2D.Impulse);
    }

    public float GetAngleFromVector(Vector2 vector)
    { 
        vector = vector.normalized;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        return (angle < 0) ? angle + 360 : angle;
    }
}
