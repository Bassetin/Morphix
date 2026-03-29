using UnityEngine;

public class NormalForm : PlayerBase {

	protected override void ApplyFormStats(){
		speed = 5f;
		jumpForce = 6f;
		gravityScale = 1f;
		rb.gravityScale = gravityScale;

		//MUDANÇA DA COR DA FORMA
		sr.color = Color.white;

		//OUTLINE DA FORMA
		float outlineStrength = 0.4f;
		Color c = sr.color;
		outlineSR.color = new Color(
    	c.r * outlineStrength,
    	c.g * outlineStrength,
    	c.b * outlineStrength,
    	1f);

		//EFEITO TRAIL
		SetTrailFromSprite(0.2f, 0.4f, 0f);
	}

	//NÃO TEM HABILIDADE ESPECIAL
	public override void SpecialAbility(){}
}