using System;
using System.Collections;
using UnityEngine;

namespace GameJam
{
    public class Lamp : MonoBehaviour
    {
        [SerializeField] private AudioClip _buzz;
        [SerializeField] private AudioClip _flash;
        [SerializeField] private AudioSource _source;

        [Space]

        [SerializeField] private GameObject[] _activeObjects;
        [SerializeField] private float _step;
        [SerializeField] private float _stepRandom;

        private void Start()
        {
            StartCoroutine(WaitForChance());
        }

        private IEnumerator WaitForChance()
        {
            float waitTime = _step + UnityEngine.Random.Range(-_stepRandom, _stepRandom);
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(Buzz(() => { StartCoroutine(WaitForChance()); }));
        }

        private IEnumerator Buzz(Action onBuzzEnded = null)
        {
            SetClip(_flash);

            float time = 0;
            int countOfFlashings = UnityEngine.Random.Range(2, 5);
            int currentCount = 0;

            float flashOffTime = UnityEngine.Random.Range(0.15f, 0.35f);
            float flashOnTime = UnityEngine.Random.Range(0.1f, 0.35f);

            while (currentCount < countOfFlashings && time <= _flash.length)
            {
                yield return new WaitForSeconds(flashOnTime);
                SwitchObjects(false);
                yield return new WaitForSeconds(flashOffTime);
                SwitchObjects(true);
                time += flashOffTime + flashOnTime;
                currentCount++;
            }

            SetClip(_buzz);
            onBuzzEnded?.Invoke();
        }

        private void SwitchObjects(bool state)
        {
            for (int i = 0; i < _activeObjects.Length; i++)
                _activeObjects[i].SetActive(state);
        }

        private void SetClip(AudioClip clip)
        {
            _source.Pause();
            _source.clip = clip;
            _source.Play();
        }
    }
}
