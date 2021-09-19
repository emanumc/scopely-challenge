using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = string.Concat("Coins: ", _wallet.Value);
    }
}
