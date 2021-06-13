using System;
using System.IO;
using Google.Cloud.Translation.V2;

namespace GoogleCeviri
{
    class Program
    {
        static String KaynakDil = "en";
        static String HedefDil = "de";

        readonly static String KaynakDosya = "/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources.resx";
        readonly static String HedefDosya = "/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources." + HedefDil + ".resx";
        readonly static string credential_path = System.IO.Path.Combine("/Users/mustafa/Library/Mobile Documents/com~apple~CloudDocs/Apple Sertifikalar/MuNaTek Şirket Hesabı ile oluşturulan Sertifikalar/Google Ceviri Servis Hesabi/ceviri-316608-d8775251cb06.json");


        StreamWriter Dosya;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using StreamWriter file = new(HedefDosya, append: false);
            file.Close();
            CeviriYap();
            Console.WriteLine("TAMAMLANDI");

        }


        public static void CeviriYap()
        {

            int counter = 0;
            string line;
            string oncekisatir = " ";

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@KaynakDosya);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();
                System.Console.WriteLine(line);



                if (line.Contains("<value>"))
                {

                    if (oncekisatir.IndexOf("<data name") >= 0)
                    {
                        string cevrilecekdeger = line.Substring(7, line.IndexOf("</value>") - 7);

                        string cevirilmisdeger = GoogleCevir(cevrilecekdeger);



                        DosyayaYaz("<value>" + cevirilmisdeger + "</value>");



                    }
                    else
                    {
                        DosyayaYaz(line);

                    }

                }
                else
                {
                    DosyayaYaz(line);

                }

                oncekisatir = line;

                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            //  System.Console.ReadLine();

        }

        async public static void DosyayaYaz(string satir)
        {
            using StreamWriter file = new(HedefDosya, append: true);
            await file.WriteLineAsync(satir);

        }


        public static string GoogleCevir(string metin)
        {

            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            TranslationClient client = TranslationClient.Create();

            string ceviri_sonuc = client.TranslateText(metin, HedefDil, KaynakDil).TranslatedText;

            return ceviri_sonuc;


        }



    }





}
