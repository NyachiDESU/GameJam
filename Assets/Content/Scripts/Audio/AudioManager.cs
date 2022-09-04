using System.Collections;
using UnityEngine;

namespace GameJam
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _sourcePrefab;

        [SerializeField] private AudioClip _moveShelfClip;
        [SerializeField] private AudioClip _paperClip;
        [SerializeField] private AudioClip _keyUseClip;
        [SerializeField] private AudioClip _keyPickupClip;
        [SerializeField] private AudioClip _bookUseClip;
        [SerializeField] private AudioClip _bookPickupClip;
        [SerializeField] private AudioClip _bookDropClip;
        [SerializeField] private AudioClip _bookCloseClip;
        [SerializeField] private AudioClip _doorOpeningClip;

        private AudioClip[] _clips;

        private static AudioManager _instance;

        private void Awake()
        {
            _clips = new AudioClip[]
            {
                _moveShelfClip,
                _paperClip,
                _keyUseClip,
                _keyPickupClip,
                _bookUseClip,
                _bookPickupClip,
                _bookDropClip,
                _bookCloseClip,
                _doorOpeningClip
            };

            _instance = this;
        }

        public static void PlayClip(ClipType type, Vector3 position, float volume = 1)
        {
            AudioSource source = Instantiate(_instance._sourcePrefab, position, Quaternion.identity);
            source.volume = volume;
            source.PlayOneShot(_instance._clips[(int)type]);
            _instance.StartCoroutine(_instance.DestroySource(source.gameObject, _instance._clips[(int)type].length));
        }

        private IEnumerator DestroySource(GameObject gameObject, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }

    public enum ClipType
    {
        MoveShelf,
        Paper,
        KeyUse,
        KeyPickup,
        BookUse,
        BookPickup,
        BookDrop,
        BookClose,
        DoorOpening
    }
}
