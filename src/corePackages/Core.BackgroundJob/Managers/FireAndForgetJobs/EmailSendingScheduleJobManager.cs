using Core.Mailing;

namespace Core.BackgroundJob.Managers.FireAndForgetJobs
{
    public class EmailSendingScheduleJobManager
    {
        private readonly IMailService _mailService;

        public EmailSendingScheduleJobManager(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public void Process(Mail mail)
        {
            _mailService.SendMail(mail);
        }
    }
}