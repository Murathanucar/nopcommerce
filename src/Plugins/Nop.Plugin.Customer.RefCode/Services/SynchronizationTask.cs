using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Client.ReferanceCode.Services
{
    /// <summary>
    /// Represents a schedule task to synchronize contacts
    /// </summary>
    public class SynchronizationTask : IScheduleTask
    {
        #region Fields

        private readonly RewardPointManager _rewardPointManager;

        #endregion

        #region Ctor

        public SynchronizationTask(RewardPointManager rewardPointManager)
        {
            _rewardPointManager = rewardPointManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute task
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task ExecuteAsync()
        {
            await _rewardPointManager.SynchronizeAsync();

        }

        #endregion
    }
}