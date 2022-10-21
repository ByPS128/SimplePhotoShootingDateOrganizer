using System.IO;
using System.Linq;

namespace PhotoOrganiser.Services
{
    internal class FileMover
    {
        public int MoveFiles(string rootFolder, string fileMask, bool useSubFolders)
        {
            int couner = 0;
            MoveFilesInternal(ref couner, rootFolder, fileMask, useSubFolders);

            return couner;
        }

        private void MoveFilesInternal(ref int counter, string rootFolder, string fileMask, bool useSubFolders)
        {
            if (useSubFolders)
            {
                var subDirs = Directory.GetDirectories(rootFolder);
                for (var i = 0; i < subDirs.Length; i++)
                {
                    var subFolder = Path.Combine(rootFolder, subDirs[i]);
                    MoveFilesInternal(ref counter, subFolder, fileMask, useSubFolders);
                }
            }

            var files = Directory.GetFiles(rootFolder, fileMask).ToList();
            for (var i = 0; i < files.Count; i++)
            {
                var fileName = Path.GetFileName(files[i]);
                var sourceFileName = files[i];
                var creationDateTime = File.GetCreationTimeUtc(sourceFileName);
                var updateDateTime = File.GetLastWriteTimeUtc(sourceFileName);
                var usedFileDateTime = creationDateTime < updateDateTime ? creationDateTime : updateDateTime;
                var constructedFolderName = usedFileDateTime.ToString("yyy_MM_dd");
                var constructedFolder = Path.Combine(rootFolder, constructedFolderName);
                if (Directory.Exists(constructedFolder) is false)
                {
                    Directory.CreateDirectory(constructedFolder);
                }

                var destinationFileName = Path.Combine(constructedFolder, fileName);
                File.Move(sourceFileName, destinationFileName);
                counter++;
            }
        }
    }
}
