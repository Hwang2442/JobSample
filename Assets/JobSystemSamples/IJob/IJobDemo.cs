using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

public class IJobDemo : MonoBehaviour
{
    private struct TestJob : IJob
    {
        [ReadOnly] public float a;
        [ReadOnly] public float b;
        [ReadOnly] public float c;

        public TestJob(float a, float b, float c)
        {
            this.a = a; this.b = b; this.c = c;
        }

        public void Execute()
        {
            for (int i = 0; i < 50000; i++)
            {
                float r = math.sqrt(math.exp(a) * math.exp(b) * math.exp10(c));
            }
        }
    }

    private float a = 1;
    private float b = 2;
    private float c = 3;

    public bool useJobs = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (useJobs)
        {
            var job = new TestJob(a, b, c);
            var handle = job.Schedule();

            handle.Complete();
        }
        else
        {
            for (int i = 0; i < 50000; i++)
            {
                float r = math.sqrt(math.exp(a) * math.exp(b) * math.exp10(c));
            }
        }
    }
}
