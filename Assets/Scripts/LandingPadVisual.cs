using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreMultiplayerTextMesh;


    private void Awake()
    {
        LandingPad landingPad = GetComponent<LandingPad>();
        scoreMultiplayerTextMesh.text = "x" + landingPad.GetScoreMultiplier();

    }
}
