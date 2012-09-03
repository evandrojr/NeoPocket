using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace NeoZip
{
    public class Zip
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zipFile">File to be be created</param>
        /// <param name="lstFiles">Files to be zipped</param>
        public static void ZipFiles(string zipFile, List<string> lstFiles)
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFile)))
            {

                s.SetLevel(6); // 0 - store only to 9 - means best compression

                byte[] buffer = new byte[4096];

                foreach (string file in lstFiles)
                {

                    // Using GetFileName makes the result compatible with XP
                    // as the resulting path is not absolute.
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                    // Setup the entry data as required.

                    // Crc and size are handled by the library for seakable streams
                    // so no need to do them here.

                    // Could also use the last write time or similar for the file.
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {

                        // Using a fixed size buffer here makes no noticeable difference for output
                        // but keeps a lid on memory usage.
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                // Finish/Close arent needed strictly as the using statement does this automatically

                // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                // the created file would be invalid.
                s.Finish();

                // Close is important to wrap things up and unlock the file.
                s.Close();
            }
        }

        public static void UnzipFiles(string zippedFile, string directoryToUnzip)
        {
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zippedFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    Directory.CreateDirectory(directoryToUnzip);

                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryToUnzip + directoryName);
                    }
                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(directoryToUnzip + directoryName + theEntry.Name))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }



    }
}
