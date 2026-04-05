using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField]
    private AudioClip _hitSfx;

    private AudioSource _audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitSfx()
    {
        _audioSource.pitch = 1f + Random.Range(-0.2f, 0.2f);
        _audioSource.PlayOneShot(_hitSfx);
    }
}
