using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Application.Services.Email
{
    public interface IEmailSender
    {
        public bool Send(string to, string subject, string message);
        public Task  SendAsync(string to, string subject, string message);

    }
}
