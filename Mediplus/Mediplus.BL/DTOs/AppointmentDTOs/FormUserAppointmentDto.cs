using System.ComponentModel.DataAnnotations;

namespace Mediplus.BL.DTOs.AppointmentDTOs;

public class FormUserAppointmentDto
{
	[Display(Prompt = "Name")]
	public string Name { get; set; }

	[Display(Prompt = "Surname")]
	public string Surname { get; set; }

	[Display(Prompt = "FIN Code")]
	public string FINCode { get; set; }

	[Display(Prompt = "Phone Number")]
	public string PhoneNumber { get; set; }

	[Display(Prompt = "Write Your Message Here.....")]
	public string Message { get; set; }
}
