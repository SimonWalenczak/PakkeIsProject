using UnityEngine;
using DG.Tweening;

public class JumpingAnimation : MonoBehaviour
{
    public float jumpHeight = 2f; // Hauteur du saut
    public float jumpDuration = 1f; // Durée du saut
    public float squashAndStretchScale = 0.8f; // Facteur d'échelle pour le "squash and stretch"

    void Start()
    {
        // Appeler la fonction pour démarrer l'animation en boucle
        StartJumpAnimation();
    }

    void StartJumpAnimation()
    {
        // Utiliser DOTween pour créer une séquence d'animation en boucle
        Sequence jumpSequence = DOTween.Sequence();

        // Ajouter une séquence de saut à la séquence principale
        jumpSequence.Append(transform.DOMoveY(jumpHeight, jumpDuration / 2).SetEase(Ease.OutQuad));
        jumpSequence.Append(transform.DOMoveY(0, jumpDuration / 2).SetEase(Ease.InQuad));

        // Ajouter l'effet de "squash and stretch" à l'arrivée
        jumpSequence.Append(transform.DOScaleY(squashAndStretchScale, 0.1f));
        jumpSequence.Append(transform.DOScaleY(1f, 0.1f));

        // Répéter la séquence indéfiniment
        jumpSequence.SetLoops(-1);

        // Optionnel : ajouter d'autres actions à la séquence si nécessaire
        // jumpSequence.Append(...);

        // Optionnel : spécifier un délai avant le début de la première répétition
        // jumpSequence.SetDelay(1f);
    }
}
