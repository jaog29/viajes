using Viajes.Common.Models;

namespace Viajes.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}

}
