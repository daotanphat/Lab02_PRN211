using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float lifeTime;
	float direction;
	float timer;
	bool hit;
	BoxCollider2D boxCollider;
	Animator animator;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (hit) return;
		float movementSpeed = speed * Time.deltaTime * direction;
		transform.Translate(movementSpeed, 0, 0);

		timer += Time.deltaTime;
		if (timer > lifeTime) gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		hit = true;
		boxCollider.enabled = false;
		animator.SetTrigger("explode");
		collision.gameObject.SetActive(false);
	}

	public void setDirection(float _direction)
	{
		timer = 0;
		direction = _direction;
		gameObject.SetActive(true);
		hit = false;
		boxCollider.enabled = true;

		float localScaleX = transform.localScale.x;
		if (Mathf.Sign(localScaleX) != _direction)
		{
			localScaleX = -localScaleX;
		}

		transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
	}
}
