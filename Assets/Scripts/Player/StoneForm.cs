using UnityEngine;

public class StoneForm : PlayerBase{

	// 🔥 NOVO — força do impacto
	[SerializeField] float slamForce = 20f;

	// 🔥 NOVO — partículas de impacto
	[SerializeField] GameObject impactParticles;

	[SerializeField] float returnSpeed = 6f;

	Vector3 originalScale;


	// 🔥 NOVO — controle de queda forte
	bool isSlamming;

	protected override void ApplyFormStats(){
		speed = 2f;
		jumpForce = 14f;
		gravityScale = 4f;
		rb.gravityScale = gravityScale;

		// MUDANÇA DE COR DA FORMA
		sr.color = Color.gray;

		//OUTLINE DA FORMA
		float outlineStrength = 0.4f;
		Color c = sr.color;
		outlineSR.color = new Color(
    	c.r * outlineStrength,
    	c.g * outlineStrength,
    	c.b * outlineStrength,
    	1f);

		baseScale = new Vector3(1.3f, 1.3f, 1f); // mais pesado

		// EFEITO TRAIL DA FORMA
		SetTrailFromSprite(0.1f, 0.6f, 0f);

		originalScale = Vector3.one;

	}

	protected override void OnDisable()
{
    base.OnDisable();
    isSlamming = false;
}

	// HABILIDADE ESPECIAL : IMPACTO FORTE
	public override void SpecialAbility(){
		if(!isGrounded){

			// 🔥 NOVO — marca que está dando slam
			isSlamming = true;

			rb.AddForce(Vector2.down * slamForce, ForceMode2D.Impulse);
		}
	}

	protected override void Update(){
		base.Update();

		if(Input.GetKeyDown(KeyCode.E)){
			SpecialAbility();
		}

		// 🔥 NOVO — efeito visual de queda 
		if(!isGrounded && rb.linearVelocity.y < -5f){
			// leve "esticada" pra dar sensação de peso caindo
			SetVisualScale(new Vector3(0.9f, 1.2f, 1f));
		}else{
			// volta ao normal
			SetVisualScale(Vector3.Lerp(currentVisualScale, originalScale, returnSpeed * Time.deltaTime));
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);

		// 🔥 NOVO — detectar impacto do slam
		if(isSlamming && collision.gameObject.CompareTag("Ground")){

			// 🔥 NOVO — spawn de partículas
			if(impactParticles != null){
				Instantiate(impactParticles, transform.position, Quaternion.identity);
			}

			// 🔥 NOVO — micro "travadinha" (peso)
			rb.linearVelocity = Vector2.zero;

			// 🔥 NOVO — pequeno squash no impacto
			transform.localScale = new Vector3(1.3f, 0.7f, 1f);

			// 🔥 reset
			isSlamming = false;
		}
	}
}