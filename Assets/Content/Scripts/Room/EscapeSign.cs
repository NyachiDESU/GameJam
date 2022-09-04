using UnityEngine;
using TMPro;

namespace GameJam
{
    public class EscapeSign : MonoBehaviour
    {
        [SerializeField] private TMP_Text _escapeText;

        private static EscapeSign _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static void ChangeEscapeTextColor()
        {
            _instance._escapeText.color = Color.green;
        }
    }
}
