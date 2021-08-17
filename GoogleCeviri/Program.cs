using System;
using System.IO;
using Google.Cloud.Translation.V2;

namespace GoogleCeviri
{
    class Program
    {
        static String KaynakDil = "en";
        static String HedefDil = "ru";

        readonly static String KaynakDosya = "/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources.resx";
        readonly static String HedefDosya = "/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources." + HedefDil + ".resx";
        readonly static string credential_path = System.IO.Path.Combine("/Users/mustafa/Library/Mobile Documents/com~apple~CloudDocs/Apple Sertifikalar/MuNaTek Şirket Hesabı ile oluşturulan Sertifikalar/Google Ceviri Servis Hesabi/ceviri-316608-d8775251cb06.json");

        readonly static String KaynakDosyaPlaz = "/Users/mustafa/Downloads/PulAZ.EN.txt";
        readonly static String HedefDosyaPlaz = "/Users/mustafa/Downloads/PulAZ." + HedefDil.ToUpper() + ".txt";




        StreamWriter Dosya;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           // using StreamWriter file = new(HedefDosya, append: false);
           // file.Close();
           // CeviriYapOrwino(KaynakDosya, HedefDosya);


            using StreamWriter filePlz = new(HedefDosyaPlaz, append: false);
            filePlz.Close();

            CeviriYapPLAZ(KaynakDosyaPlaz, HedefDosyaPlaz);
            Console.WriteLine("TAMAMLANDI");

        }


        public static void CeviriYapOrwino(String pKaynakDosya, string pHedefDosya)
        {

            int counter = 0;
            string line;
            string oncekisatir = " ";

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(pKaynakDosya);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();



                if (line.Contains("<value>"))
                {

                    if (oncekisatir.IndexOf("<data name") >= 0)
                    {
                        string cevrilecekdeger = line.Substring(7, line.IndexOf("</value>") - 7);

                        string cevirilmisdeger = GoogleCevir(cevrilecekdeger);



                        DosyayaYaz(pHedefDosya, "<value>" + cevirilmisdeger + "</value>");
                        System.Console.WriteLine(cevrilecekdeger + " >> " + cevirilmisdeger);



                    }
                    else
                    {
                        DosyayaYaz(pHedefDosya, line);

                    }

                }
                else
                {
                    DosyayaYaz(pHedefDosya, line);

                }

                oncekisatir = line;

                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            //  System.Console.ReadLine();

        }


        public static void CeviriYapPLAZ(String pKaynakDosyaPlaz, string pHedefDosyaPlaz)
        {

            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(pKaynakDosyaPlaz);
            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();



                if (line.Contains(" = "))
                {


                    string cevrilecekdeger = line.Substring(0, line.IndexOf("=") - 1).Trim().Remove(0, 1).Substring(0,(line.Substring(0, line.IndexOf("=") - 1).Trim().Remove(0, 1).Length-1));

                        string cevirilmisdeger = GoogleCevir(cevrilecekdeger);


                        DosyayaYaz(pHedefDosyaPlaz, line.Substring(0, line.IndexOf("=")+1) + " \"" +cevirilmisdeger + "\";");
                        System.Console.WriteLine(cevrilecekdeger + " >> " + cevirilmisdeger);



                 

                }
                else
                {
                    DosyayaYaz(pHedefDosyaPlaz, line);

                }


                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            //  System.Console.ReadLine();

        }



        async public static void DosyayaYaz(String pHdefDosya, string satir)
        {
            using StreamWriter file = new(pHdefDosya, append: true);
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
