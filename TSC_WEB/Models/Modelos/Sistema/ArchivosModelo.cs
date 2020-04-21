using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Modelos.Sistema
{
    public class ArchivosModelo
    {
        public byte[] getFile(string ruta, string nombre)
        {
            byte[] fileContent = null;
            System.IO.FileStream fs = new System.IO.FileStream(ruta, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(ruta).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;
        }
    }
}