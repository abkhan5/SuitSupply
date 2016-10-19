using System;

namespace SuitSupply.WorkerJob
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            Console.WriteLine("Starting Worker job");
            try 
            {
            using (var processor = new SuitSupplyProcessor())
            {
                processor.Start();

                Console.WriteLine("Host started");
                Console.WriteLine("Press enter to finish");
                Console.ReadLine();

                processor.Stop();
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                
            
        }
    }
}
