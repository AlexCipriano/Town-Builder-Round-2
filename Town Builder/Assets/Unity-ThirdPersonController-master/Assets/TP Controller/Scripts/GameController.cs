using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int targetFrameRate = 60;

    protected virtual void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
		
}
