using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private int scoreMultiplayer;


    public int GetScoreMultiplier()
    {
        return scoreMultiplayer;
    }
}
