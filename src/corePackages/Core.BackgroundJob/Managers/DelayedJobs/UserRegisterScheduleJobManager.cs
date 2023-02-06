using Core.Mailing;
using MimeKit;

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
            var toEmailList = new List<MailboxAddress>
                {
                new("Emre Can Ayar","emrecan.ayar@hotmail.com")
                };
            _mailService.SendMail(new Mail { TextBody = userId.ToString(), ToList = toEmailList, Subject = "HangFire Test" });
        }
    }
}