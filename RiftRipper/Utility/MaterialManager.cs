namespace RiftRipper.Utility;

public class MaterialManager
{
    public static Dictionary<string, int> shaders = new();

    public static int LoadMaterial(string name, string vertexShaderPath, string fragmentShaderPath)
    {
        if (shaders.ContainsKey(name)) return shaders.First(x => x.Key == name).Value;

        if(!File.Exists(vertexShaderPath) || !File.Exists(fragmentShaderPath))
        {
            var e = new FileNotFoundException($"One of the shader files was not found. See {name}.");
            ErrorHandler.Alert(e);
            return 0;
        }

        string vertexSource = File.ReadAllText(vertexShaderPath);
        string fragmentSource = File.ReadAllText(fragmentShaderPath);

        int vertexProgramId = GL.CreateShader(ShaderType.VertexShader);
        int fragmentProgramId = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(vertexProgramId, vertexSource);
        GL.CompileShader(vertexProgramId);

        GL.ShaderSource(fragmentProgramId, fragmentSource);
        GL.CompileShader(fragmentProgramId);

        GL.GetShader(vertexProgramId, ShaderParameter.CompileStatus, out int res);
        if(res != (int)All.True)
        {
            string infoLog = GL.GetShaderInfoLog(vertexProgramId);
            throw new Exception($"Error when compiling vertex shader at {vertexShaderPath}.\nError: {infoLog}");
        }

        GL.GetShader(fragmentProgramId, ShaderParameter.CompileStatus, out res);
        if(res != (int)All.True)
        {
            string infoLog = GL.GetShaderInfoLog(fragmentProgramId);
            throw new Exception($"Error when compiling fragment shader at {fragmentShaderPath}.\nError: {infoLog}");
        }

        int programId = GL.CreateProgram();
        GL.AttachShader(programId, vertexProgramId);
        GL.AttachShader(programId, fragmentProgramId);

        GL.LinkProgram(programId);

        GL.GetProgram(fragmentProgramId, GetProgramParameterName.LinkStatus, out res);
        if(res != (int)All.True)
        {
            string infoLog = GL.GetProgramInfoLog(programId);
            throw new Exception($"Error when linking program. Error: {infoLog}");
        }

        GL.DetachShader(programId, vertexProgramId);
        GL.DetachShader(programId, fragmentProgramId);

        GL.DeleteShader(vertexProgramId);
        GL.DeleteShader(fragmentProgramId);

        shaders.Add(name, programId);

        return programId;
    }
}
