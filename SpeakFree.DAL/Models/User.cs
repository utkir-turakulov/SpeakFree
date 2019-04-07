using Microsoft.AspNetCore.Identity;

namespace SpeakFree.DAL.Models
{
	public class User: IdentityUser
    {
	    /// <summary>
	    /// Отчество
	    /// </summary>
	    public string Patronymic { get; set; }

	    /// <summary>
	    /// Фамилия
	    /// </summary>
	    public string Surename { get; set; }
	}
}
