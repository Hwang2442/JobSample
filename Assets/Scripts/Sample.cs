using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class Sample : MonoBehaviour
{
    private struct TestJob : IJob
    {
        public int i;

        public void Execute()
        {
            Debug.Log(i);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        var firstJob = new TestJob() { i = 1 };
        var secondJob = new TestJob() { i = 2 };

        var firstJobHandle = firstJob.Schedule();
        var secondJobHandle = secondJob.Schedule(firstJobHandle);

        
    }
}
