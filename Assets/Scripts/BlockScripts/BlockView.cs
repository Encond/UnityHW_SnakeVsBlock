using TMPro;
using UnityEngine;

namespace BlockScripts
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;
        private Block _block;

        private void Awake() => _block = GetComponent<Block>();

        private void OnEnable() => _block.FillingProgress += OnFillingProgress;

        private void OnDisable() => _block.FillingProgress -= OnFillingProgress;

        private void OnFillingProgress(int progressValue) => _view.text = progressValue.ToString();
    }
}