using HandlebarPractice.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace HandlebarPractice.Services
{
    public class PdfService : IPdf
    {
        public byte[] GeneratePdf(string htmlContent)
        {

            return Task.Run(async () => await GeneratePdfAsync(htmlContent)).GetAwaiter().GetResult();
        }

        private async Task<byte[]> GeneratePdfAsync(string htmlContent)
        {
            // 1. BrowserFetcher no longer needs 'using'. 
            // It downloads the default browser revision.
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            // 2. Launch the browser
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true // 'true' is the new HeadlessMode.True
            });

            // 3. Create a new page and set the HTML
            await using var page = await browser.NewPageAsync();

            // It is safer to use WaitUntilNavigation.Networkidle0 to ensure 
            // all styles/images are loaded before printing
            await page.SetContentAsync(htmlContent, new NavigationOptions
            {
                WaitUntil = new[] { WaitUntilNavigation.Networkidle0 }
            });

            // 4. Generate the PDF bytes
            return await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new PuppeteerSharp.Media.MarginOptions
                {
                    Top = "20px",
                    Bottom = "20px",
                    Left = "20px",
                    Right = "20px"
                }
            });
        }
    }
}