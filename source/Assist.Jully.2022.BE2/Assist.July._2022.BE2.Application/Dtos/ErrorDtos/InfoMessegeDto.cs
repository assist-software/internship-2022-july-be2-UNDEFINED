namespace Assist.July._2022.BE2.Application.Dtos.InfoDtos
{
    public class InfoMessegeDto
    {
        public string Ok { get; set; } = "{'message':'ok'}";
        public string Error { get; set; } = "{'message':'User not found'}";
        public string ErrorEmail { get; set; } = "{'message':'Email not found'}";
        public string Take { get; set; } = "{'message':'Email already taken'}";
        public string ErrorEmpty { get; set; } = "{'message':'Empty'}";
    }
}
