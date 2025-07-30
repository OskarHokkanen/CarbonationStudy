using UnityEngine;

public class ParticleSpawnSound : MonoBehaviour
{
    public AudioClip spawnSound;
    public float volume = 1f;

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private int previousParticleCount = 0;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        int aliveCount = ps.particleCount;

        // Resize if necessary
        if (particles == null || particles.Length < aliveCount)
        {
            particles = new ParticleSystem.Particle[aliveCount];
        }

        int count = ps.GetParticles(particles);

        if (count > previousParticleCount)
        {
            for (int i = previousParticleCount; i < count; i++)
            {
                Vector3 spawnPos = ps.simulationSpace == ParticleSystemSimulationSpace.World
                    ? particles[i].position
                    : transform.TransformPoint(particles[i].position);

                PlaySound(spawnPos);
            }
        }

        previousParticleCount = count;
    }


    void PlaySound(Vector3 position)
    {
        if (spawnSound == null) return;

        GameObject tempGO = new GameObject("TempBubbleSpawnSound");
        tempGO.transform.position = position;

        AudioSource source = tempGO.AddComponent<AudioSource>();
        source.clip = spawnSound;
        source.volume = volume;
        source.spatialBlend = 1f;
        source.minDistance = 1f;
        source.maxDistance = 20f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.Play();

        Destroy(tempGO, spawnSound.length + 0.1f);
    }
}