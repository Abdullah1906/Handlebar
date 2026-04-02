namespace HandlebarPractice.Interfaces
{
    public interface IPdf
    {
        byte[] GeneratePdf(string htmlContent);
        
    }
}
