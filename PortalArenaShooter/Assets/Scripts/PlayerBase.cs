using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject portalBall;
    public GameObject portal1;
    public GameObject portal2;

    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Material play1Mat;
    [SerializeField] private Material play2Mat;
    public int health = 3;
    private bool player1 = true;
    private bool canShoot = true;
    private bool canShootPortal = true;
    private bool canTakeDamage = true;

    private Vector3 direction;

    private UIM UIM = null;
    private GameManager GM = null;

    // Start is called before the first frame update
    void Start()
    {

        UIM = GameObject.Find("Canvas").GetComponent<UIM>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {

        Turn();
        Move();
        ShootPortalBall();
        Shoot();

    }


    public void SetPlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            meshRenderer.material = play1Mat;
            //this.playerInput.SwitchCurrentControlScheme("Player1");
            player1 = true;
        }

        if (playerNumber == 2)
        {
            meshRenderer.material = play2Mat;
            //this.playerInput.SwitchCurrentControlScheme("Player2");
            player1 = false;
        }
    }

    private void Shoot()
    {

        if (player1)
        {

            if (Input.GetKey(KeyCode.Space) && canShoot)
            {

                GameObject temp = Instantiate(bullet, transform.position + (transform.up.normalized * 0.8f), Quaternion.identity);
                temp.GetComponent<Bullet>().SetDirection(transform.up);
                temp.GetComponent<Bullet>().SetOwner(1, this.gameObject);
                StartCoroutine(Shooting());

            }
            

        }
        else
        {

            if (Input.GetKey(KeyCode.L) && canShoot)
            {

                GameObject temp = Instantiate(bullet, transform.position + (transform.up.normalized * 0.8f), Quaternion.identity);
                temp.GetComponent<Bullet>().SetDirection(transform.up);
                temp.GetComponent<Bullet>().SetOwner(2, this.gameObject);
                StartCoroutine(Shooting());

            }

        }

    }

    private void ShootPortalBall()
    {

        if (player1)
        {

            if (Input.GetKey(KeyCode.V) && canShootPortal)
            {

                Destroy(portal1.gameObject);
                GameObject temp = Instantiate(portalBall, transform.position + (transform.up.normalized * 0.8f), Quaternion.identity);
                temp.GetComponent<PortalBall>().SetDirection(transform.up);
                temp.GetComponent<PortalBall>().SetOwner(1, this.gameObject, portal2, 1);
                StartCoroutine(ShootingPortal());

            }

            if (Input.GetKey(KeyCode.B) && canShootPortal)
            {

                Destroy(portal2.gameObject);
                GameObject temp = Instantiate(portalBall, transform.position + (transform.up.normalized * 0.8f), Quaternion.identity);
                temp.GetComponent<PortalBall>().SetDirection(transform.up);
                temp.GetComponent<PortalBall>().SetOwner(1, this.gameObject, portal1, 2);
                StartCoroutine(ShootingPortal());

            }


        }
        else
        {

            if (Input.GetKey(KeyCode.J) && canShootPortal)
            {

                Destroy(portal1.gameObject);
                GameObject temp = Instantiate(portalBall, transform.position + (transform.up.normalized * 0.8f), Quaternion.identity);
                temp.GetComponent<PortalBall>().SetDirection(transform.up);
                temp.GetComponent<PortalBall>().SetOwner(2, this.gameObject, portal2, 1);
                StartCoroutine(ShootingPortal());

            }

            if (Input.GetKey(KeyCode.K) && canShootPortal)
            {

                Destroy(portal2.gameObject);
                GameObject temp = Instantiate(portalBall, transform.position + (transform.up.normalized * 0.8f), Quaternion.identity);
                temp.GetComponent<PortalBall>().SetDirection(transform.up);
                temp.GetComponent<PortalBall>().SetOwner(2, this.gameObject, portal1, 2);
                StartCoroutine(ShootingPortal());

            }

        }

    }

    private void Turn()
    {


        if (player1)
        {

            if(Input.GetKey(KeyCode.A)) {

                transform.Rotate(Vector3.forward, 1.0f);

            }
            if (Input.GetKey(KeyCode.D)) {

                transform.Rotate(Vector3.forward, -1.0f);

            }

        } else
        {

            if (Input.GetKey(KeyCode.LeftArrow)) {

                transform.Rotate(Vector3.forward, 1.0f);

            }
            if (Input.GetKey(KeyCode.RightArrow)) {

                transform.Rotate(Vector3.forward, -1.0f);

            }


        }

    }

    private void Move()
    {


        if (player1)
        {

            if (Input.GetKey(KeyCode.W))
            {

                transform.Translate(Vector3.up * speed * Time.deltaTime);

            }

            if (Input.GetKey(KeyCode.S))
            {

                transform.Translate(Vector3.down * speed * Time.deltaTime);

            }

        }
        else
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {

                transform.Translate(Vector3.up * speed * Time.deltaTime);

            }

            if (Input.GetKey(KeyCode.DownArrow))
            {

                transform.Translate(Vector3.down * speed * Time.deltaTime);

            }

        }

    }

    public void TakeDamage()
    {
        if(canTakeDamage)
        {

            StartCoroutine(GracePeriod());
            health--;

            if (player1) UIM.UpdateLives(1, health);
            else UIM.UpdateLives(2, health);

        }
        

        if(health <= 0)
        {

            Destroy(this.gameObject);

        }

    }

    public void SetPortal(int portalIndex, GameObject portal)
    {

        if (portalIndex == 1)
        {

            portal1 = portal;
            if (portal2 != null)
            {

                portal2.GetComponent<Portal>().SetLink(portal1);

            }

        } else
        {

            portal2 = portal;
            if (portal1 != null)
            {

                portal1.GetComponent<Portal>().SetLink(portal2);

            }

        }

    }

    IEnumerator Shooting()
    {

        canShoot = false;

        yield return new WaitForSeconds(0.3f);

        canShoot = true;
    }

    IEnumerator ShootingPortal()
    {

        canShoot = false;
        canShootPortal = false;

        yield return new WaitForSeconds(1.5f);

        canShoot = true;
        canShootPortal = true;
    }

    IEnumerator GracePeriod()
    {

        canTakeDamage = false;

        yield return new WaitForSeconds(0.5f);

        canTakeDamage = true;


    }


}
