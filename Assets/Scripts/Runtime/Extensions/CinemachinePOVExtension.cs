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
                    //InputSystem'inden gelen x ve y deðerlerini aktarýyoruz.
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                    //Mouseýn aþaðý yukarý hareketini -80 ile +80 arasýnda kýsýtlýyoruz.
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    //Karakterin yerel rotasyonunun nasýl olacýðýný ayarlýyoruz.
                    //x kýsmýna y'yi koymamýzýn sebebi mouse saða sola döndürðümüzde y eksenindeki hareketi yapmak istiyoruz.
                    //y de de yukarý aþaðý hareket ettirdiðimizde x rotasyonundaki hareketi istiyoruz o yüzden bu þekilde.
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
            }
        }
    }
}
