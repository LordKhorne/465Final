using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBall : MonoBehaviour
{

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material p1p1;
    [SerializeField] private Material p1p2;
    [SerializeField] private Material p2p1;
    [SerializeField] private Material p2p2;

    [SerializeField] private GameObject portal;

    public float bulletSpeed = 10.0f;

    private int portalNum;
    private int playerNum;
    public GameObject owner = null;
    public GameObject linkedPort = null;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var newDelta = direction * Time.deltaTime * bulletSpeed;
        GetComponent<Rigidbody>().MovePosition(transform.position + newDelta);

    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
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

    }

    private void OnCollisionEnter(Collision collision)
    {

        var temp = Instantiate(portal, this.gameObject.transform.position + ((this.gameObject.transform.position - GameObject.Find("Centerpoint").transform.position).normalized * 0.2f), Quaternion.identity);
        temp.gameObject.GetComponent<Portal>().SetOwner(playerNum, owner, linkedPort, portalNum);

        Destroy(this.gameObject);   

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Portal"))
        {

            Destroy(this.gameObject);

        }

    }



}
