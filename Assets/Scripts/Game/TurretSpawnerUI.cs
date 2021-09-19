using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSpawnerUI : MonoBehaviour
{
    [SerializeField] private TurretSpawner _turretSpawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LayerMask _allowedLayers;

    private GameObject _turretPreview;
    private Coroutine _movePreviewCoroutine;

    public void MovePreview(TurretType turretType, GameObject turretPreview, int price)
    {
        if (_turretPreview == null)
        {
            var e = MovePreviewCoroutine(turretType, turretPreview, price);
            _movePreviewCoroutine = StartCoroutine(e);
        }
        else
        {
            StopCoroutine(_movePreviewCoroutine);
            _movePreviewCoroutine = null;

            HidePreview();
        }
    }

    private IEnumerator MovePreviewCoroutine(TurretType turretType, GameObject turretPreview, int price)
    {
        _turretPreview = turretPreview;
        _turretPreview.SetActive(true);

        while (_turretPreview.activeSelf)
        {
            // Get mouse position over terrain
            Vector3? previewPos = GetPreviewPosition();
            if (previewPos.HasValue)
            {
                _turretPreview.transform.position = previewPos.Value;
            }

            // On Mouse Click
            if (Input.GetMouseButtonDown(0) &&
                _wallet.CanSubtract(price) &&
                previewPos.HasValue)
            {
                _turretSpawner.PlaceTurret(turretType, previewPos.Value);
                _wallet.Subtract(price);
                break;
            }

            yield return null;
        }

        _movePreviewCoroutine = null;

        HidePreview();
    }

    private void HidePreview()
    {
        _turretPreview.SetActive(false);
        _turretPreview = null;
    }

    public Vector3? GetPreviewPosition()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }

        Vector3? previewPosition = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (var hit in hits)
        {
            int goLayer = hit.collider.gameObject.layer;
            if (_allowedLayers.Contains(goLayer))
            {
                previewPosition = hit.point;
            }
            else
            {
                previewPosition = null;
                break;
            }
        }
        return previewPosition;
    }
}
