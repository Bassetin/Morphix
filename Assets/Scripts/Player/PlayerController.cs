using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [SerializeField] NormalForm normalForm;
    [SerializeField] StoneForm stoneForm;
    [SerializeField] LightForm lightForm;
    [SerializeField] ElasticForm elasticForm;
    [SerializeField] SlimeForm slimeForm;

    public PlayerBase currentForm;

    public event System.Action<PlayerBase> OnFormChanged;

    void Start(){
        ChangeForm(normalForm);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Alpha1)) ChangeForm(normalForm);
        if(Input.GetKeyDown(KeyCode.Alpha2)) ChangeForm(stoneForm);
        if(Input.GetKeyDown(KeyCode.Alpha3)) ChangeForm(lightForm);
        if(Input.GetKeyDown(KeyCode.Alpha4)) ChangeForm(elasticForm);
        if(Input.GetKeyDown(KeyCode.Alpha5)) ChangeForm(slimeForm);
    }

    void ChangeForm(PlayerBase newForm){
        if(currentForm != null){
            currentForm.enabled = false;
        }

        currentForm = newForm;
        currentForm.enabled = true;

        OnFormChanged?.Invoke(currentForm);


        StartCoroutine(TransformEffect());
    }



// MÉTODO DE ANIMAÇÃO
  IEnumerator TransformEffect()
{
    PlayerBase form = currentForm;
    SpriteRenderer sr = form.GetComponent<SpriteRenderer>();
    Vector3 originalScale = form.baseScale;
    Vector3 targetScale = new Vector3(
        originalScale.x * 1.5f,
        originalScale.y * 0.7f,
        1f
    );

    float time = 0f;
    float duration = 0.12f;
    Color baseColor = sr.color;
    Color glowColor = baseColor * 1.5f;
    glowColor.a = 1f;

    // FASE 1 — squash + glow
    while (time < duration)
    {
        form.SetVisualScale(Vector3.Lerp(originalScale, targetScale, time / duration));
        sr.color = Color.Lerp(baseColor, glowColor, time / duration);
        time += Time.deltaTime;
        yield return null;
    }

    time = 0f;

    // FASE 2 — volta ao normal
    while (time < duration)
    {
        form.SetVisualScale(Vector3.Lerp(targetScale, originalScale, time / duration));
        sr.color = Color.Lerp(glowColor, baseColor, time / duration);
        time += Time.deltaTime;
        yield return null;
    }

    form.SetVisualScale(originalScale);
    sr.color = baseColor;
}
}
