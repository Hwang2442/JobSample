using System.Linq;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class IJobParallelForDemo : MonoBehaviour
{
    private struct TestJob : IJobParallelFor
    {
        [ReadOnly] private NativeArray<float> a;
        [ReadOnly] private NativeArray<float> b;
        [ReadOnly] private NativeArray<float> c;

        public TestJob(NativeArray<float> a, NativeArray<float> b, NativeArray<float> c)
        {
            this.a = a; this.b = b; this.c = c;
        }

        public void Execute(int i)
        {
            float r = math.sqrt(math.exp(a[i]) * math.exp(b[i]) * math.exp10(c[i]));
        }
    }

    private NativeArray<float> a;
    private NativeArray<float> b;
    private NativeArray<float> c;

    public bool useJobs = false;

    private void Start()
    {
        a = new NativeArray<float>(Enumerable.Repeat(1f, 50000).ToArray(), Allocator.TempJob);
        b = new NativeArray<float>(Enumerable.Repeat(2f, 50000).ToArray(), Allocator.TempJob);
        c = new NativeArray<float>(Enumerable.Repeat(3f, 50000).ToArray(), Allocator.TempJob);
    }

    private void OnDestroy()
    {
        a.Dispose();
        b.Dispose();
        c.Dispose();
    }

    private void Update()
    {
        if (useJobs)
        {
            var job = new TestJob(a, b, c);
            var handle = job.Schedule(50000, 64);

            handle.Complete();
        }
        else
        {
            for (int i = 0; i < 50000; i++)
            {
                float r = math.sqrt(math.exp(a[i]) * math.exp(b[i]) * math.exp10(c[i]));
            }
        }
    }
}
