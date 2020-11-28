using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Saved in Blit.cs
public class Blit : ScriptableRendererFeature
{

    public class BlitPass : ScriptableRenderPass
    {
        public enum RenderTarget
        {
            Color,
            RenderTexture,
        }

        public Material blitMaterial = null;
        public int blitShaderPassIndex = 0;
        public FilterMode filterMode { get; set; }

        private RenderTargetIdentifier source { get; set; }
        private RenderTargetHandle destination { get; set; }

        RenderTargetHandle m_TemporaryColorTexture;
        string m_ProfilerTag;

        public BlitPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.blitMaterial = blitMaterial;
            this.blitShaderPassIndex = blitShaderPassIndex;
            m_ProfilerTag = tag;
            m_TemporaryColorTexture.Init("_TemporaryColorTexture");
        }

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            blitMaterial.SetFloat("u_time", Time.fixedTime);
            CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            // Can't read and write to same color target, use a TemporaryRT
            if (destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc, filterMode);
                Blit(cmd, source, m_TemporaryColorTexture.Identifier(), blitMaterial, blitShaderPassIndex);
                Blit(cmd, m_TemporaryColorTexture.Identifier(), source);
            }
            else
            {
                Blit(cmd, source, destination.Identifier(), blitMaterial, blitShaderPassIndex);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (destination == RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(m_TemporaryColorTexture.id);
        }
    }

    [System.Serializable]
    public class BlitSettings
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

        public Shader shader;
        public float bend = 4f;
        public float scanlineSize1 = 200;
        public float scanlineSpeed1 = -10;
        public float scanlineSize2 = 20;
        public float scanlineSpeed2 = -3;
        public float scanlineAmount = 0.05f;
        public float vignetteSize = 1.9f;
        public float vignetteSmoothness = 0.6f;
        public float vignetteEdgeRound = 8f;
        public float noiseSize = 75f;
        public float noiseAmount = 0.05f;

        // Chromatic aberration amounts
        public Vector2 redOffset = new Vector2(0, -0.01f);
        public Vector2 blueOffset = Vector2.zero;
        public Vector2 greenOffset = new Vector2(0, 0.01f);
        public int blitMaterialPassIndex = -1;
        public Target destination = Target.Color;
        public string textureId = "_BlitPassTexture";
    }

    public enum Target
    {
        Color,
        Texture
    }

    public BlitSettings settings = new BlitSettings();
    RenderTargetHandle m_RenderTextureHandle;

    BlitPass blitPass;

    public override void Create()
    {
        Material blitMaterial = new Material(settings.shader);
        blitMaterial.SetFloat("u_bend", settings.bend);
        blitMaterial.SetFloat("u_scanline_size_1", settings.scanlineSize1);
        blitMaterial.SetFloat("u_scanline_speed_1", settings.scanlineSpeed1);
        blitMaterial.SetFloat("u_scanline_size_2", settings.scanlineSize2);
        blitMaterial.SetFloat("u_scanline_speed_2", settings.scanlineSpeed2);
        blitMaterial.SetFloat("u_scanline_amount", settings.scanlineAmount);
        blitMaterial.SetFloat("u_vignette_size", settings.vignetteSize);
        blitMaterial.SetFloat("u_vignette_smoothness", settings.vignetteSmoothness);
        blitMaterial.SetFloat("u_vignette_edge_round", settings.vignetteEdgeRound);
        blitMaterial.SetFloat("u_noise_size", settings.noiseSize);
        blitMaterial.SetFloat("u_noise_amount", settings.noiseAmount);
        blitMaterial.SetVector("u_red_offset", settings.redOffset);
        blitMaterial.SetVector("u_blue_offset", settings.blueOffset);
        blitMaterial.SetVector("u_green_offset", settings.greenOffset);
        var passIndex = blitMaterial != null ? blitMaterial.passCount - 1 : 1;
        settings.blitMaterialPassIndex = Mathf.Clamp(settings.blitMaterialPassIndex, -1, passIndex);
        blitPass = new BlitPass(settings.Event, blitMaterial, settings.blitMaterialPassIndex, name);
        m_RenderTextureHandle.Init(settings.textureId);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        var dest = (settings.destination == Target.Color) ? RenderTargetHandle.CameraTarget : m_RenderTextureHandle;

        blitPass.Setup(src, dest);
        renderer.EnqueuePass(blitPass);
    }
}