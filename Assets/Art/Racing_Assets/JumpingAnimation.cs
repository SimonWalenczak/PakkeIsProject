using UnityEngine;
using DG.Tweening;

public class JumpingAnimation : MonoBehaviour
{
    public float jumpHeight = 2f; // Hauteur du saut
    public float jumpDuration = 1f; // Dur�e du saut
    public float squashAndStretchScale = 0.8f; // Facteur d'�chelle pour le "squash and stretch"

    void Start()
    {
        // Appeler la fonction pour d�marrer l'animation en boucle
        StartJumpAnimation();
    }

    void StartJumpAnimation()
    {
        // Utiliser DOTween pour cr�er une s�quence d'animation en boucle
        Sequence jumpSequence = DOTween.Sequence();

        // Ajouter une s�quence de saut � la s�quence principale
        jumpSequence.Append(transform.DOMoveY(jumpHeight, jumpDuration / 2).SetEase(Ease.OutQuad));
        jumpSequence.Append(transform.DOMoveY(0, jumpDuration / 2).SetEase(Ease.InQuad));

        // Ajouter l'effet de "squash and stretch" � l'arriv�e
        jumpSequence.Append(transform.DOScaleY(squashAndStretchScale, 0.1f));
        jumpSequence.Append(transform.DOScaleY(1f, 0.1f));

        // R�p�ter la s�quence ind�finiment
        jumpSequence.SetLoops(-1);

        // Optionnel : ajouter d'autres actions � la s�quence si n�cessaire
        // jumpSequence.Append(...);

        // Optionnel : sp�cifier un d�lai avant le d�but de la premi�re r�p�tition
        // jumpSequence.SetDelay(1f);
    }
}
