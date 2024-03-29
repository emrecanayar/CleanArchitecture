﻿using Core.BackgroundJob.Managers.DelayedJobs;

namespace Core.BackgroundJob.Schedules
{
    public static class DelayedJobs
    {
        [Obsolete]
        public static void SendMailRegisterJobs(int userId)
        {
            Hangfire.BackgroundJob.Schedule<UserRegisterScheduleJobManager>
                 (
                  job => job.Process(userId),
                  TimeSpan.FromSeconds(10));
        }
    }
}