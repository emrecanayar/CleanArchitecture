using System.Text;

namespace Core.Helpers.Helpers
{
    public static class FileHelper
    {
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        public static void ClearFile(string filePath)
        {
            File.Delete(filePath);
            CreateFile(filePath);
        }

        public static void CreateFile(string filePath)
        {
            try
            {
                if (!IsExistFile(filePath))
                {
                    FileInfo file = new FileInfo(filePath);

                    FileStream fs = file.Create();

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message);
            }
        }

        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                if (!IsExistFile(filePath))
                {
                    FileInfo file = new FileInfo(filePath);

                    FileStream fs = file.Create();

                    fs.Write(buffer, 0, buffer.Length);

                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message);
            }
        }

        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }

        public static void DeleteFile(string file)
        {
            if (File.Exists(file))

                File.Delete(file);
        }

        public static void ExistsFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);

                fs.Close();
            }
        }

        public static string GetDateFile()
        {
            return DateTime.Now.ToString("HHmmssff");
        }

        public static string GetExtension(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return fi.Extension;
        }

        public static string GetFileName(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return fi.Name;
        }

        public static string GetFileNameNoExtension(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return fi.Name.Split('.')[0];
        }

        public static int GetFileSize(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return (int)fi.Length;
        }

        public static int GetLineCount(string filePath)
        {
            string[] rows = File.ReadAllLines(filePath);

            return rows.Length;
        }

        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            string sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }

                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }

        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            File.WriteAllText(filePath, text, encoding);
        }

        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
    }
}