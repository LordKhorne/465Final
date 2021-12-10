using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material play1Mat;
    [SerializeField] private Material play2Mat;

    public float bulletSpeed = 10.0f;
    private int bounces = 0;

    public GameObject owner = null;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        var newDelta = direction * Time.deltaTime * bulletSpeed;
        rigidbody.MovePosition(transform.position + newDelta);

    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {

        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        bounces++;

        if (bounces > 3)
        {

            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && other.gameObject != owner)
        {
            other.gameObject.GetComponent<PlayerBase>().TakeDamage();
            Destroy(this.gameObject);
        }

    }

    public void SetOwner(int playerNumber, GameObject owner)
    {
        if (playerNumber == 1)
        {
            meshRenderer.material = play1Mat;

        }

        if (playerNumber == 2)
        {
            meshRenderer.material = play2Mat;

        }

        this.owner = owner;

    }

}
