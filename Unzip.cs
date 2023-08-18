using System.IO.Compression;

namespace PabsUnzip
{
    internal class Unzip
    {
        protected string[] unzipArgs;

        public Unzip(string[] args) 
        {
            unzipArgs = args ?? new string[0];
        }

        public void Exec()
        {
            try
            {
                if(unzipArgs.Length > 0) 
                {
                    UnzipWithArgs();
                }
                else
                {
                    throw new ArgumentException("Atleast one (zipFilename) argument is required");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UnzipWithArgs()
        {
            try
            {
                string srcZip = unzipArgs[0];

                if(!srcZip.Contains(".zip") || !IsZipValid(srcZip))
                {
                    throw new Exception("Must be a valid ZIP file");
                }

                if(unzipArgs.Length == 1)
                {
                    string dstZip = Path.GetFileName(srcZip);
                    string target = string.Format(@"{0}\{1}", srcZip.Replace(dstZip, ""), dstZip.Replace(".zip", ""));
                    ZipFile.ExtractToDirectory(srcZip, target, true);
                }
                else if(unzipArgs.Length == 2)
                {
                    string dstZip = unzipArgs[1];
                    ZipFile.ExtractToDirectory(srcZip, dstZip, true);
                }
                else
                {
                    throw new Exception("Only two arguments are expected");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool IsZipValid(string path)
        {
            try
            {
                using (var zipFile = ZipFile.OpenRead(path))
                {
                    var entries = zipFile.Entries;
                    return true;
                }
            }
            catch (InvalidDataException)
            {
                return false;
            }
        }
    }
}
