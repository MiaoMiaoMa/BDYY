using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataProvider;
using System.IO;

namespace BDYY_WEB.Classes
{
    public class UploadFiles
    {
        internal  UploadFilesProvider db = new UploadFilesProvider();

        // Retrieve the filer root location of Document directory
        public string DocumentsRootDirectory
        {
            get { return HttpContext.Current.Session["AttachmentDir"].ToString(); }
        }

        public void SaveUploadFiles(Stream stream, string documentPathAndFilename, out string fileExt, out Guid documentGuid, string patientID)
        {
            documentGuid = Guid.NewGuid();
            fileExt = string.Empty;
            // 创建文件目录，如果目录不存在
            CreateDirectory();
            string documentPath = DocumentsRootDirectory;
            // 获取GUID文件名
            string documentFilename = documentGuid + Path.GetExtension(documentPathAndFilename);
            fileExt = GetFileExt(documentFilename);

            // 保存到物理磁盘
            this.Save(documentPath + documentFilename, stream);

            //保存到数据库
            db.createFile(documentGuid, documentPathAndFilename, patientID);
        }

        // returns the file extension
        public static string GetFileExt(string fileName)
        {
            string fileExt = String.Empty;

            if (fileName.Contains("."))
                fileExt = fileName.Substring(fileName.LastIndexOf('.') + 1);

            return fileExt;
        }

        public void CreateDirectory()
        {
            if (!Directory.Exists(DocumentsRootDirectory))
                Directory.CreateDirectory(DocumentsRootDirectory);
        }

        // Save the file to disk
        public void Save(string documentPathAndFilename, Stream stream)
        {
            Stream s = stream;
            byte[] binaryData = new byte[s.Length];
            long bytesRead = s.Read(binaryData, 0, binaryData.Length);
            s.Close();

            using (FileStream fs = File.Create(documentPathAndFilename))
                fs.Write(binaryData, 0, binaryData.Length);
        }

        // Get a text file from disk
        public string Get(string documentPathAndFilename)
        {
            string data = null;

            try
            {
                StreamReader stream = new StreamReader(documentPathAndFilename);
                data = stream.ReadToEnd();
                stream.Close();
            }
            catch { }

            return data;
        }
    }
}