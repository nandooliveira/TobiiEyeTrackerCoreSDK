using System;
using System.IO;
using Tobii.Interaction;

namespace Interaction_Streams_101
{
    /// <summary>
    /// The data streams provide nicely filtered eye-gaze data from the eye tracker 
    /// transformed to a convenient coordinate system. The point on the screen where 
    /// your eyes are looking (gaze point), and the points on the screen where your 
    /// eyes linger to focus on something (fixations) are given as pixel coordinates 
    /// on the screen. The positions of your eyeballs (eye positions) are given in 
    /// space coordinates in millimeters relative to the center of the screen.
    /// 
    /// Let's see how is simple to find out where are you looking at the screen
    /// using GazePoint data stream, accessible from Streams property of Host instance.
    /// </summary>
    public class Program
    {
        private const string Format = "{0}\t{1}\t{2}";

        public static void Main(string[] args)
        {
            // Everything starts with initializing Host, which manages connection to the 
            // Tobii Engine and provides all the Tobii Core SDK functionality.
            // NOTE: Make sure that Tobii.EyeX.exe is running
            var host = new Host();

            // 2. Create stream. 
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();

            // Open file to write data
            FileStream fs = new FileStream(@"C:\Users\nando\Desktop\gaze_data.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            // 3. Get the gaze data!
            gazePointDataStream.GazePoint((x, y, ts) => {
                string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");                
                string point = string.Format(Format, (int) x, (int) y, now);
                sw.WriteLine(point);
                Console.WriteLine(point);
            });

            // okay, it is 4 lines, but you won't be able to see much without this one :)
            Console.ReadKey();

            // we will close the coonection to the Tobii Engine before exit.
            host.DisableConnection();

            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
