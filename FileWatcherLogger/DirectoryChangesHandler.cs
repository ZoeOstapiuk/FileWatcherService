using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherLogger
{
    public class DirectoryChangesHandler
    {
        private IFileWatcherLogger logger;
        private FileSystemWatcher watcher;
        private string path;

        public DirectoryChangesHandler(string path)
        {
            Path = path;
            watcher = new FileSystemWatcher(Path);
            logger = new FileWatcherLogger(Path);

            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(path));
                }

                path = value;
            }
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            logger.Renamed(e.OldFullPath, e.FullPath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            logger.Changed(e.FullPath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            logger.Created(e.FullPath);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            logger.Deleted(e.FullPath);
        }
    }
}
