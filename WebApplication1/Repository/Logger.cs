using NLog;

namespace WebApplication1.Repository
{
    public class Logger
    {
        private static NLog.Logger _log;

        public Logger()
        {
            _log = LogManager.GetLogger("Logger");
        }

        public void Saving(string id)
        {
            _log.Info("Saving id number {0} ", id);
        }

        public void Saved(string id)
        {
            _log.Info("Saved id number {0} ", id);
        }

        public void Reading(string id)
        {
            _log.Debug("Retrieving item with id {0} ", id);
        }

        public void DidNotFindItem(string id)
        {
            _log.Debug("Did not find item with id {0} ", id);
        }

        public void ReturningItem(string id)
        {
            _log.Debug("Returning item with id {0} ", id);
        }
        public void UpdateItem(string id)
        {
            _log.Debug("Updating item with id {0} ", id);
        }
        public void DeleteItem(string id)
        {
            _log.Debug("Updating item with id {0} ", id);
        }
    }
}