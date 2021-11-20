using System.Text.Json;

namespace MusicQuizAPI.Exceptions
{
    public class ErrorDetails
    {
        public int status { get; set; }
        public string message { get; set; }

        public ErrorDetails(int status, string msg)
        {
            this.status = status;
            this.message = msg;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}