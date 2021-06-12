using System;
using System.IO;
using System.Threading.Tasks;

namespace GoogleCeviri
{
    class Program
    {

        readonly static String KaynakDosya = "/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources.resx";
        readonly static String HedefDosya = "/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources.resx.txt";


        StreamWriter Dosya;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using StreamWriter file = new(HedefDosya, append: false);
            file.Close();
            CeviriYap();
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

                        string cevirilmisdeger = "AAA" + cevrilecekdeger + "BBB";

                        DosyayaYaz("<value>"+cevirilmisdeger +"</value>") ;



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
            System.Console.ReadLine();

        }

        async public static void DosyayaYaz(string satir)
        {
            using StreamWriter file = new("/Users/mustafa/Documents/Projelerim/Xamarin/Orwino/Localization/AppResources.resx.txt", append: true);
            await file.WriteLineAsync(satir);

        }


    }





}
