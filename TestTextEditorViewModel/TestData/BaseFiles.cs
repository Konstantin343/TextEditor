using TestTools.Utils;

namespace TestTextEditorViewModel.TestData
{
    public static class BaseFiles
    {
        public static readonly string TextFile = EnvironmentHelper.GetResourcePath("text.txt");
        
        public static readonly string CsharpFile = EnvironmentHelper.GetResourcePath("csharp.cs");
        
        public static readonly string JavaFile = EnvironmentHelper.GetResourcePath("java.java");

        public static readonly string ToSaveTxtFile = EnvironmentHelper.GetOutputPath("test.txt");
        
        public static readonly string ToSaveCsFile = EnvironmentHelper.GetOutputPath("test.cs");
        
        public static readonly string ToSaveJavaFile = EnvironmentHelper.GetOutputPath("test.java");
    }
}