using UnityEngine;

public class LightForm : PlayerBase{

	[SerializeField] float glideGravity = 0.2f; // 🔥 NOVO — gravidade durante planar
	[SerializeField] float returnSpeed = 6f;
	Vector3 originalScale;


	bool isGliding; // 🔥 NOVO

	protected override void ApplyFormStats(){
		speed = 8f;
		jumpForce = 6f;
		gravityScale = 0.5f;
		rb.gravityScale = gravityScale;

		// MUDANÇA DA COR DO PERSONAGEM
		sr.color = Color.cyan;

		//OUTLINE DA FORMA
		float outlineStrength = 0.4f;
		Color c = sr.color;
		outlineSR.color = new Color(
    	c.r * outlineStrength,
    	c.g * outlineStrength,
    	c.b * outlineStrength,
    	1f);

		originalScale = Vector3.one;
		

		// EFEITO TRAIL
		SetTrailFromSprite(0.7f, 0.3f, 0f);
		trail.minVertexDistance = 0.05f;
	}

	// 🔥 AGORA NÃO USAMOS MAIS COMO "impulso"
	public override void SpecialAbility(){}

	protected override void Update(){
		base.Update();

		// 🔥 SEGURAR tecla para planar (tipo Tails)
		if(Input.GetKey(KeyCode.E) && !isGrounded && rb.linearVelocity.y <= 0)
		{
			StartGlide();
		}
		else
		{
			StopGlide();
		}
	}

	// 🔥 INICIAR PLANAR
	void StartGlide()
	{
		isGliding = true;

		// reduz gravidade
		rb.gravityScale = glideGravity;

		// limita velocidade de queda (sensação de flutuar)
		if(rb.linearVelocity.y < -2f)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, -2f);
		}

		// 🔥 leve “esticada” visual
		transform.localScale = new Vector3(0.9f, 1.1f, 1f);
	}

	// 🔥 PARAR PLANAR
	void StopGlide()
	{
		isGliding = false;

		rb.gravityScale = gravityScale;

		// volta ao normal suavemente
		SetVisualScale(Vector3.Lerp(currentVisualScale, 
			originalScale, returnSpeed * Time.deltaTime));
	}
}