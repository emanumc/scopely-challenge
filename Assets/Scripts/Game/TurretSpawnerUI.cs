using System.Collections;
using UnityEngine;

public class TurretSpawnerUI : MonoBehaviour
{
    private class TurretOffer
    {
        public TurretType type;
        public int price;
        public GameObject gameObject;
        public Vector3? position;
    }

    [SerializeField] private TurretSpawner _turretSpawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LayerMask _allowedLayers;

    private TurretOffer _turretOffer;
    private Coroutine _movePreviewCoroutine;

    public void MovePreview(TurretType turretType, GameObject turretPreview, int price)
    {
        if (_turretOffer == null)
        {
            _turretOffer = new TurretOffer()
            {
                type = turretType,
                price = price,
                gameObject = turretPreview
            };

            var e = MovePreviewCoroutine();
            _movePreviewCoroutine = StartCoroutine(e);
        }
        else
        {
            StopCoroutine(_movePreviewCoroutine);
            _movePreviewCoroutine = null;

            HidePreview();
        }
    }

    private IEnumerator MovePreviewCoroutine()
    {
        var preview = _turretOffer.gameObject;
        preview.SetActive(true);

        while (preview.activeSelf)
        {
            _turretOffer.position = GetPreviewPosition();
            if (_turretOffer.position.HasValue)
            {
                preview.transform.position = _turretOffer.position.Value;
            }
            yield return null;
        }

        _movePreviewCoroutine = null;
    }

    public void PlaceTurret()
    {
        if (_turretOffer != null)
        {
            int price = _turretOffer.price;
            Vector3? position = _turretOffer.position;
            TurretType turretType = _turretOffer.type;

            if (_wallet.CanSubtract(price) && position.HasValue)
            {
                _turretSpawner.PlaceTurret(turretType, position.Value);
                _wallet.Subtract(price);

                HidePreview();
            }
        }
    }

    private void HidePreview()
    {
        if (_turretOffer != null)
        {
            _turretOffer.gameObject.SetActive(false);
            _turretOffer = null;
        }
    }

    public Vector3? GetPreviewPosition()
    {
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
