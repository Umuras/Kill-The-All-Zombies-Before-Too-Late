using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;
    [SerializeField]
    private float clampAngle = 80f;



    private CinemachineInputProvider inputProvider;
    private Vector3 startingRotation;

    private void Start()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        if (startingRotation == null)
        {
            startingRotation = transform.localRotation.eulerAngles;
        }
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow != null)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation != null)
                {
                    Vector2 deltaInput = inputProvider.XYAxis.ToInputAction().ReadValue<Vector2>();
                    //InputSystem'inden gelen x ve y de�erlerini aktar�yoruz.
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    //Mouse�n a�a�� yukar� hareketini -80 ile +80 aras�nda k�s�tl�yoruz.
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    //Karakterin yerel rotasyonunun nas�l olac���n� ayarl�yoruz.
                    //x k�sm�na y'yi koymam�z�n sebebi mouse sa�a sola d�nd�r��m�zde y eksenindeki hareketi yapmak istiyoruz.
                    //y de de yukar� a�a�� hareket ettirdi�imizde x rotasyonundaki hareketi istiyoruz o y�zden bu �ekilde.
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
            }
        }
    }
}
