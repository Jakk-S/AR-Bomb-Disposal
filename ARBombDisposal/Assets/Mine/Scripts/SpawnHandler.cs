using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager arImageManager;

    [SerializeField] private GameObject bombPrefab;

    private GameObject currentBomb;

    private void OnEnable() => arImageManager.trackablesChanged.AddListener(OnTrackedImageChanged);
    private void OnDisable() => arImageManager.trackablesChanged.RemoveListener(OnTrackedImageChanged);

    private void OnTrackedImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage image in args.added)
        {
            SpawnBomb(image);
        }
    }

    private void SpawnBomb(ARTrackedImage trackedImage)
    {
        if (currentBomb != null) return;

        currentBomb = Instantiate(bombPrefab, trackedImage.transform);

        currentBomb.transform.localPosition = Vector3.zero;
        currentBomb.transform.localRotation = Quaternion.identity;


    }
}
