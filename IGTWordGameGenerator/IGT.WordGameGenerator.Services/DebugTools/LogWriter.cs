using System.IO;

namespace IGT.WordGameGenerator.Services.DebugTools
{
    public class LogWriter
    {
        private static string FILE_PATH = "C:\\IGT_LOG";
        private static LogWriter instance;

        public static LogWriter GetInstance()
        {
            if (instance == null)
            {
                instance = new LogWriter();
            }
            return instance;
        }

        private LogWriter()
        {
            FileStream fileCreation = File.Create(FILE_PATH);
            fileCreation.Close();
        }

        public void WriteLog(string message)
        {
            string time = System.DateTime.Now.ToString();
            StreamWriter writer = File.AppendText(FILE_PATH);
            writer.Write(time + " ");
            writer.WriteLine(message);
            writer.Close();
        }
    }
}