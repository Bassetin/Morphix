using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerFormUI : MonoBehaviour
{
    public Image formIcon;
    public TextMeshProUGUI formText;

    public PlayerController player;

    void Start()
    {
        player.OnFormChanged += UpdateUI;

        if (player.currentForm != null)
            UpdateUI(player.currentForm);
    }

    void UpdateUI(PlayerBase form)
    {
        formText.text = GetFormName(form);

        if (form is NormalForm)
            formIcon.color = Color.white;

        else if (form is LightForm)
            formIcon.color = Color.cyan;

        else if (form is SlimeForm)
            formIcon.color = Color.green;

        else if (form is StoneForm)
            formIcon.color = Color.gray;

        else if (form is ElasticForm)
            formIcon.color = Color.yellow;

        Animate();
    }

    string GetFormName(PlayerBase form)
    {
        if (form is NormalForm) return "Normal";
        if (form is StoneForm) return "Pedra";
        if (form is LightForm) return "Leve";
        if (form is ElasticForm) return "Elástico";
        if (form is SlimeForm) return "Slime";


        return "Desconhecido";
    }

    void Animate()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleEffect());
    }

    IEnumerator ScaleEffect()
    {
        Vector3 startScale = Vector3.one * 1.2f;
        Vector3 endScale = Vector3.one;

        float duration = 0.2f;
        float time = 0f;

        transform.localScale = startScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            yield return null;
        }

        transform.localScale = endScale;
    }
}