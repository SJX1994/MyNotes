using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;

public class SJX_CustomRenderPassFeature : ScriptableRendererFeature
{
    public class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier SJX_injectedCameraSource;
        private Material SJX_injectedMat;
        private bool SJX_active;
        private LayerMask SJX_layerMask;
        private RenderTargetHandle SJX_injectedTemp;

        private DrawingSettings SJX_drawingSetting;
        private FilteringSettings SJX_FilteringSetting;
        private BuiltinRenderTextureType type;
        public CustomRenderPass(Material SJX_injectedMat, bool SJX_active, int SJX_layerMask)
        {
            this.SJX_injectedMat = SJX_injectedMat;
            this.SJX_active = SJX_active;
            this.SJX_layerMask = SJX_layerMask;
            SJX_injectedTemp.Init("_SJX_tempTexture");
        }

        // This method is called before executing the render pass. 
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in an performance manner.
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (SJX_active)
            {

                // int myLayerIndexVariable = 12;
                // int myLayerMaskVariable = 12 << myLayerIndexVariable;
                // RenderQueueRange myRenderQueueRange = RenderQueueRange.transparent;

                CommandBuffer cmdBuffer = CommandBufferPool.Get("SJX_injected");
                // SJX_drawingSetting = new DrawingSettings();
                // SJX_FilteringSetting = new FilteringSettings(myRenderQueueRange, myLayerMaskVariable);

                //SJX_FilteringSetting.layerMask = SJX_layerMask;

                cmdBuffer.Clear();
                //context.DrawRenderers(renderingData.cullResults, ref SJX_drawingSetting, ref SJX_FilteringSetting);
                //创建渲染缓存
                cmdBuffer.GetTemporaryRT(SJX_injectedTemp.id, renderingData.cameraData.cameraTargetDescriptor);
                //创建屏幕渲染(渲染命令，相机缓存,差异化相机缓存，材质球)
                Blit(cmdBuffer, SJX_injectedCameraSource, SJX_injectedTemp.Identifier(), SJX_injectedMat);
                Blit(cmdBuffer, SJX_injectedTemp.Identifier(), SJX_injectedCameraSource);




                //提交屏幕渲染

                context.ExecuteCommandBuffer(cmdBuffer);


                CommandBufferPool.Release(cmdBuffer);
            }

        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }


    [System.Serializable]
    public class SJX_LWRP_setting
    {
        public Material material = null;
        public bool active = true;
        public LayerMask LayerMask;

    }
    public SJX_LWRP_setting SJX_settings = new SJX_LWRP_setting();

    CustomRenderPass m_ScriptablePass;
    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(SJX_settings.material, SJX_settings.active, SJX_settings.LayerMask);

        // Configures where the render pass should be injected.
        // m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.SJX_injectedCameraSource = renderer.cameraColorTarget;

        renderer.EnqueuePass(m_ScriptablePass);
    }
}


