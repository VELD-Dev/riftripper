using FramebufferAttatchment = OpenTK.Graphics.OpenGL.FramebufferAttachment;

namespace RiftRipper.Utility;

internal class FramebufferRenderer : IDisposable  // Inspired by Replanetizer's FramebufferRenderer's code.
{
    public static uint MSAA_LEVEL
    {
        get { return Window.Singleton.Settings.MSAA_Level; }
        set { Window.Singleton.Settings.MSAA_Level = value; }
    }
    private int internalMsaaLevel;

    private bool disposed = false;

    private int targetTexture;
    private int typeTexture;
    public int outputTexture { get; set; }
    public int outputTypeTexture { get; set; }
    private int framebufferId;
    private int renderbufferId;
    private int outputFramebufferId;

    private Vector2i texelsSize;

    public FramebufferRenderer(int width, int height)
    {
        texelsSize = new Vector2i(width, height);

        AllocateAllResources();
    }

    private void AllocateAllResources()
    {
        internalMsaaLevel = (int)MSAA_LEVEL;

        targetTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2DMultisample, targetTexture);
        GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, (int)MSAA_LEVEL, PixelInternalFormat.Rgb, texelsSize.X, texelsSize.Y, true);

        renderbufferId = GL.GenRenderbuffer();
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbufferId);
        GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, (int)MSAA_LEVEL, RenderbufferStorage.DepthComponent, texelsSize.X, texelsSize.Y);

        typeTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2DMultisample, typeTexture);
        GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, (int)MSAA_LEVEL, PixelInternalFormat.R32i, texelsSize.X, texelsSize.Y, true);

        framebufferId = GL.GenRenderbuffer();
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferId);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, targetTexture, 0);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1, TextureTarget.Texture2DMultisample, typeTexture, 0);
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, renderbufferId);

        outputTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, outputTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb8, texelsSize.X, texelsSize.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, 0);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

        outputTypeTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, outputTypeTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R32i, texelsSize.X, texelsSize.Y, 0, PixelFormat.RedInteger, PixelType.Int, (IntPtr)0);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);

        outputFramebufferId = GL.GenFramebuffer();
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, outputFramebufferId);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, outputTexture, 0);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment1, TextureTarget.Texture2D, outputTypeTexture, 0);
    }

    public void RenderToTexture(Action renderFunction)
    {
        if (internalMsaaLevel != MSAA_LEVEL)
        {
            DeleteAllResources();
            AllocateAllResources();
        }

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferId);
        GL.Viewport(0, 0, texelsSize.X, texelsSize.Y);

        DrawBuffersEnum[] buffers = { DrawBuffersEnum.ColorAttachment0, DrawBuffersEnum.ColorAttachment1 };
        GL.DrawBuffers(2, buffers);

        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Less);

        GL.GenVertexArrays(1, out int vao);
        GL.BindVertexArray(vao);

        renderFunction();

        GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebufferId);
        GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, outputFramebufferId);
        GL.ReadBuffer(ReadBufferMode.ColorAttachment0);
        GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
        GL.BlitFramebuffer(0, 0, texelsSize.X, texelsSize.Y, 0, 0, texelsSize.X, texelsSize.Y, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);
        GL.ReadBuffer(ReadBufferMode.ColorAttachment1);
        GL.DrawBuffer(DrawBufferMode.ColorAttachment1);
        GL.BlitFramebuffer(0, 0, texelsSize.X, texelsSize.Y, 0, 0, texelsSize.X, texelsSize.Y, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        GL.DeleteVertexArray(vao);
    }

    public void ExposeFramebuffer(Action func)
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, outputFramebufferId);

        func();

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    private void DeleteAllResources()
    {
        GL.DeleteFramebuffer(framebufferId);
        GL.DeleteFramebuffer(outputFramebufferId);
        GL.DeleteRenderbuffer(renderbufferId);
        GL.DeleteTexture(targetTexture);
        GL.DeleteTexture(typeTexture);
        GL.DeleteTexture(outputTexture);
        GL.DeleteTexture(outputTypeTexture);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            DeleteAllResources();
        }

        disposed = true;
    }

    ~FramebufferRenderer()
    {
        Dispose(false);
    }
}
