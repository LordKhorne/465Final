using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material p1p1;
    [SerializeField] private Material p1p2;
    [SerializeField] private Material p2p1;
    [SerializeField] private Material p2p2;
    private int playerNum;
    private int portalNum;
    public GameObject owner = null;
    public GameObject linkedPort = null;
    private bool usable = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOwner(int playerNumber, GameObject owner, GameObject linkedPort, int portalNum)
    {
        if (playerNumber == 1 && portalNum == 1)
        {
            meshRenderer.material = p1p1;

        }

        if (playerNumber == 1 && portalNum == 2)
        {
            meshRenderer.material = p1p2;

        }

        if (playerNumber == 2 && portalNum == 1)
        {
            meshRenderer.material = p2p1;

        }

        if (playerNumber == 2 && portalNum == 2)
        {
            meshRenderer.material = p2p2;

        }

        this.playerNum = playerNumber;
        this.owner = owner;
        this.portalNum = portalNum;
        this.linkedPort = linkedPort;

        owner.GetComponent<PlayerBase>().SetPortal(portalNum, this.gameObject);

    }

    public void SetLink(GameObject linkedPort)
    {

        this.linkedPort = linkedPort;

    }

    public void StartCooldown()
    {

        StartCoroutine(PortalCooldown());

    }

    private void OnTriggerEnter(Collider collision)
    {

        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) && linkedPort != null && usable)
        {

            linkedPort.GetComponent<Portal>().StartCooldown();
            collision.transform.position = linkedPort.transform.position + ((linkedPort.transform.position - GameObject.Find("Centerpoint").transform.position).normalized * -0.8f);
            StartCoroutine(PortalCooldown());

            if (collision.gameObject.CompareTag("Bullet"))
            {

                //collision.gameObject.GetComponent<Bullet>().SetDirection(linkedPort.transform.forward);

            }

        }

    }

    IEnumerator PortalCooldown()
    {

        usable = false;

        yield return new WaitForSeconds(0.5f);

        usable = true;

    }


}
