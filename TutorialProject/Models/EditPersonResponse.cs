namespace TutorialProject.Models
{
    public class EditPersonResponse
    {
        public string Message { get; set; }
        public EditPersonResponseType ResponseType { get; set; }
        public Person? Person { get; set; }
    }
}
