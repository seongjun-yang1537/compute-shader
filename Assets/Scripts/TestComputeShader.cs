using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

public class TestComputeShader : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture texRead;
    public RenderTexture texWrite;

    [Button("CreateRandom")]
    public void CreateRandom()
    {

    }

    [Button("CelluarAutomata")]
    public void ProcessCelluarAutomata()
    {
        float[] numbers = new float[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        ComputeBuffer computeBuffer = new ComputeBuffer(numbers.Length, sizeof(float));
        computeBuffer.SetData(numbers);

        int kernelId = computeShader.FindKernel("Mul2");
        computeShader.SetBuffer(kernelId, "Result", computeBuffer);
        computeShader.Dispatch(kernelId, 1, 1, 1);

        float[] result = new float[numbers.Length];
        computeBuffer.GetData(result);

        Debug.Log(string.Join(",", result));

        computeBuffer.Release();
    }

    private void SwapRenderTexture()
    {
        var temp = texRead;
        texRead = texWrite;
        texWrite = temp;
    }
}
