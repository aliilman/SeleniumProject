using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.VirtualAuth;

class Program 
{
    static void Main()
    {
        var options = new ChromeOptions();

        options.AddArgument("--start-maximized");
        options.AddArgument("--remote-allow-origins=*");

        
       

        // Tarayıcı sürücüsünü başlatma
        IWebDriver driver = new ChromeDriver(options);


        // Web sayfasını açma
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/basic_auth");


        Extencions.waitforpageload(driver);//sayfa hazır olana kadar bekle


        
    }

    public static void Arsiv()
    {

        //https://www.selenium.dev/documentation/

        // Tarayıcı ayarlarını oluşturma
        //https://github.com/GoogleChrome/chrome-launcher/blob/main/docs/chrome-flags-for-tools.md
        var options = new ChromeOptions();

        options.AddArgument("--start-maximized");
        // options.AddArgument("--headless"); //ekran açılmadan çalıştırır

        // Tarayıcı sürücüsünü başlatma
        IWebDriver driver = new ChromeDriver(options);

        Thread.Sleep(10000);

        // Web sayfasını açma
        driver.Navigate().GoToUrl("https://www.trendyol.com/giris");


        Extencions.waitforpageload(driver);//sayfa hazır olana kadar bekle

        ///Diğer Bekleme fonksionu
        //ImplicitWait 
        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);


        // Web sayfasındaki bir elementi bulma ve işlemler yapma
        try
        {
            IWebElement sifre = driver.FindElement(By.Name("login-password"));
            IWebElement mail = driver.FindElement(By.Id("login-email"));
            IWebElement uyeolbuton = driver.FindElement(By.XPath("//button[@type='submit']"));

            #region Locators tipleri
            //driver.FindElement(By.Id("name"));
            //driver.FindElement(By.LinkText("name"));
            //driver.FindElement(By.Name(""));
            //driver.FindElement(By.TagName("name"));
            //driver.FindElement(By.XPath("name"));
            //driver.FindElement(By.ClassName("name"));
            //driver.FindElement(By.CssSelector("name"));
            #endregion

            mail.Clear();
            sifre.Clear();

            /// Interacting with web elements
            mail.SendKeys("deneme@gmail.com");
            sifre.SendKeys("deneme123");
            //uyeolbuton.Click();


            ///Available relative locators
            //Above //Below //Left of //Right of //Near

            By tiklayınızMetniKonumu = RelativeBy.WithLocator(By.ClassName("clickable-text")).Below(By.Name("login-password"));
            IWebElement tıklamaMetni = driver.FindElement(tiklayınızMetniKonumu);
            if (tıklamaMetni != null)
            {
                // tıklamaMetni.Click();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("hata" + ex);
        }

        Thread.Sleep(2000);

        ///İleri, Geri, Yenile  
        driver.Navigate().Back();
        // driver.Navigate().Forward();
        //driver.Navigate().Refresh();

        //*************************************************




        // Opens a new tab and switches to new tab
        driver.SwitchTo().NewWindow(WindowType.Tab);
        driver.Navigate().GoToUrl("https://www.trendyol.com/apple/iphone-15-pro-max-256-gb-beyaz-titanyum-p-762254857");
        // Opens a new window and switches to new window
        //driver.SwitchTo().NewWindow(WindowType.Window);

        Extencions.waitforpageload(driver);

        try
        {
            IWebElement fiyat = driver.FindElement(RelativeBy.WithLocator(By.CssSelector("span.prc-dsc")).Above(By.ClassName("slicing-attributes")));

            ///Information about web elements
            Console.WriteLine("Iphone 15 Fiyatı:    " + fiyat.Text);
            Console.WriteLine("Fiyat Görünürlüğü:   " + fiyat.Displayed);
            Console.WriteLine("Fiyat Etkinliği:     " + fiyat.Enabled);
            Console.WriteLine("Fiyat Is Selected:   " + fiyat.Selected);
            Console.WriteLine("Fiyat TagName:       " + fiyat.TagName);
            Console.WriteLine("Fiyat Location:      " + fiyat.Location);
            Console.WriteLine("Fiyat Size:          " + fiyat.Size);
            Console.WriteLine("Fiyat GetCssValue:   " + fiyat.GetCssValue("background-color"));
            Console.WriteLine("Fiyat GetAttribute:  " + fiyat.GetAttribute("value"));

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }

        //IWebElement scroll = driver.FindElement(By.Id("footer-container"));
        //new Actions(driver)
        //    .ScrollToElement(scroll)
        //    // .ScrollFromOrigin(scrollOrigin, 0, 200)
        //    //.ScrollByAmount(0, deltaY)
        //    .Perform();


        //    new Actions(driver)
        //.KeyDown(Keys.Shift)
        //.SendKeys("a")
        //.KeyUp(Keys.Shift)
        //.SendKeys("b")
        //.Perform();

        //new Actions(driver)
        //    .KeyDown(Keys.Control)
        //    .KeyUp(Keys.Alt)
        //    .KeyUp(Keys.LeftAlt)
        //    .KeyUp(Keys.Escape)
        //    .KeyUp(Keys.Space)
        //    .KeyUp(Keys.Backspace)
        //    .Perform();


        Console.ReadLine();
        driver.Quit();

    }
}
public static class Extencions
{
    public static void waitforpageload(this IWebDriver driver)
    {
        try
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver; //js kodlarını yazmak için
            WebDriverWait webDriverWait = new WebDriverWait(driver, new TimeSpan(0, 0, 2));
            webDriverWait.Until<bool>((IWebDriver x) => javaScriptExecutor.ExecuteScript("return document.readyState").Equals("complete"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("waitforpageload : hatsı");
        }

    }
}
