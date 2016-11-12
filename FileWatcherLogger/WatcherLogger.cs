using System;
using log4net;
using System.IO;

namespace FileWatcherLogger
{
    internal class FileWatcherLogger : IFileWatcherLogger
    {
        private readonly ILog logger;

        public FileWatcherLogger(string path)
        {
            logger = LogManager.GetLogger(path);
        }

        public void Changed(string fileName)
        {
            logger.Info(String.Format("{0} file was changed", fileName));
        }

        public void Created(string fileName)
        {
            logger.Info(String.Format("{0} file was created", fileName));
        }

        public void Deleted(string fileName)
        {
            logger.Info(String.Format("{0} file was deleted", fileName));
        }

        public void Renamed(string oldName, string newName)
        {
            logger.Info(String.Format("{0} file was renamed to: {1}", oldName, newName));
        }
    }

    internal interface IFileWatcherLogger
    {
        void Deleted(string fileName);
        void Created(string fileName);
        void Changed(string fileName);
        void Renamed(string oldName, string newName);
    }
}
