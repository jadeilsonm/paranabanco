namespace Core.Entities;

public class SendEmailBody
{
    public Details From { get; set; }
    public List<Details> To { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public string Html { get; set; }
}

public class Details
{
    public string Email { get; set; }
    
}