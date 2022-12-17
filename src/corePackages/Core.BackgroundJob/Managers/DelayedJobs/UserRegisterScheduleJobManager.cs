using Core.Mailing;

namespace Core.BackgroundJob.Managers.DelayedJobs
{
    public class UserRegisterScheduleJobManager
    {
        private readonly IMailService _mailService;

        public UserRegisterScheduleJobManager(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public void Process(int userId)
        {
            _mailService.SendMail(new Mail { TextBody = userId.ToString(), ToEmail = "emrecan.ayar@hotmail.com", Subject = "HangFire Test" });
        }
    }
}