using UnityEngine;

public class ElasticForm : PlayerBase{

	[SerializeField] float elasticBounciness = 1.5f;
	[SerializeField] float superJump = 18f;

	// 🔥 NOVO — parâmetros de squash & stretch
	[SerializeField] float squashX = 1.3f;
	[SerializeField] float squashY = 0.7f;

	[SerializeField] float stretchX = 0.7f;
	[SerializeField] float stretchY = 1.3f;

	[SerializeField] float returnSpeed = 10f;

	Vector3 originalScale; // 🔥 NOVO

	protected override void ApplyFormStats(){
		speed = 6f;
		jumpForce = 12f;
		gravityScale = 1.5f;
		rb.gravityScale = gravityScale;

		// MUDANÇA NA COR DA FORMA
		sr.color = Color.yellow;

		//OUTLINE DA FORMA
		float outlineStrength = 0.4f;
		Color c = sr.color;
		outlineSR.color = new Color(
    	c.r * outlineStrength,
    	c.g * outlineStrength,
    	c.b * outlineStrength,
    	1f);

		baseScale = new Vector3(1f, 1.2f, 1f); // mais alto

		// EFEITO TRAIL DA FORMA
		SetTrailFromSprite(0.4f, 0.5f, 0f);
		trail.widthMultiplier = 1.2f;

		// 🔥 NOVO — guardar escala original
		originalScale = Vector3.one;
	}

	// HABILIDADE ESPECIAL : SUPER PULO
	public override void SpecialAbility(){
		if(isGrounded){

			// squash antes do pulo
			SetVisualScale(new Vector3(squashX, squashY, 1f));

			rb.AddForce(Vector2.up * superJump, ForceMode2D.Impulse);
		}
	}

	// HABILIDADE PASSIVA : QUICA NA PAREDE
	protected override void OnCollisionEnter2D(Collision2D collision){

		base.OnCollisionEnter2D(collision);

		if(collision.gameObject.CompareTag("Wall")){
			Vector2 reflection = Vector2.Reflect(rb.linearVelocity, collision.contacts[0].normal);
			rb.linearVelocity = reflection * elasticBounciness;

			// 🔥 NOVO — squash ao bater na parede
			SetVisualScale(new Vector3(1.4f, 0.6f, 1f));
		}
	}

	protected override void Update(){
		base.Update();

		if(Input.GetKeyDown(KeyCode.E)){
			SpecialAbility();
		}

		// stretch no ar (subindo)
		if (!isGrounded && rb.linearVelocity.y > 0.1f)
    		SetVisualScale(new Vector3(stretchX, stretchY, 1f));

		// voltar ao normal suavemente
		SetVisualScale(Vector3.Lerp(currentVisualScale, 
			originalScale, returnSpeed * Time.deltaTime));
	}
}