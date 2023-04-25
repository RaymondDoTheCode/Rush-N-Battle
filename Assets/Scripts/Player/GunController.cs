using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;
    public int damage = 40;
    public float range = 100f;
    public TextMeshProUGUI ammoCounter;

    // Variables that change throughout the code
    bool canShoot;
    int CurrentAmmoInClip;
    int ammoInReserve;

    // Muzzle flash
    public Image muzzleFlashImage;
    public Sprite[] flashes;

    // Aiming
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1;
    Vector2 currentRotation;
    public float weaponSwayAmount = 10;

    private void Start()
    {
        CurrentAmmoInClip = clipSize;
        ammoInReserve = reservedAmmoCapacity;
        canShoot = true;
    }

    private void Update()
    {
        DetermineAim();
        DetermineRotation();
        if (Input.GetMouseButton(0) && canShoot && CurrentAmmoInClip > 0)
        {
            canShoot = false;
            CurrentAmmoInClip--;
            StartCoroutine(ShootGun());
        }
        else if (Input.GetKeyDown(KeyCode.R) && CurrentAmmoInClip < clipSize && ammoInReserve > 0)
        {
            int amountNeeded = clipSize - CurrentAmmoInClip;
            if (amountNeeded >= ammoInReserve)
            {
                CurrentAmmoInClip += ammoInReserve;
                ammoInReserve -= amountNeeded;
            }
            else
            {
                CurrentAmmoInClip = clipSize;
                ammoInReserve -= amountNeeded;
            }
            if (ammoInReserve < 0)
            {
                ammoInReserve = 0;
            }
        }
        ammoCounter.text = CurrentAmmoInClip + "/" + ammoInReserve;
    }

    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        currentRotation += mouseAxis;

        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;
    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetMouseButton(1))
        {
            target = aimingLocalPosition;
        }
        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    void RayCastForEnemy()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("Enemy")))
        {
            try
            {
                //Debug.Log(hit.transform.name);
                TurtleShell target = hit.transform.GetComponent<TurtleShell>();
                Slime target2 = hit.transform.GetComponent<Slime>();
                Party target3 = hit.transform.GetComponent<Party>();
                Bomb target4 = hit.transform.GetComponent<Bomb>();
                BOSS target5 = hit.transform.GetComponent<BOSS>();
                MidBoss target6 = hit.transform.GetComponent<MidBoss>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (target2 != null)
                {
                    target2.TakeDamage(damage);
                }
                if (target3 != null)
                {
                    target3.TakeDamage(damage);
                }
                if (target4 != null)
                {
                    target4.TakeDamage(damage);
                }
                if (target5 != null)
                {
                    target5.TakeDamage(damage);
                }
                if (target6 != null)
                {
                    target6.TakeDamage(damage);
                }
            }
            catch
            {

            }
        }
    }

    public void RestoreAmmo(int ammo)
    {
        ammoInReserve += ammo;
    }

    public void IncreaseDamage(int dmg)
    {
        damage += dmg;
    }

    public void IncreaseRange(int rng)
    {
        range += rng;
    }

    public void GodMode() // developer script
    {
        damage = 10000;
        CurrentAmmoInClip = 500;
        clipSize = 500;
        ammoInReserve = 10000;
        range = 1000;
    }


    IEnumerator ShootGun()
    {
        AudioManage.instance.Play("GunSound");
        // Adds some backwards recoil
        transform.localPosition -= Vector3.forward * 0.1f;
        StartCoroutine(MuzzleFlash());
        RayCastForEnemy();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}