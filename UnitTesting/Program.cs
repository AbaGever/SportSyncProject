using DBL2;

namespace UnitTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DrillsListDB instance = new DrillsListDB();

            int testWorkoutId = 1;

            // Call the function
            List<string> drills = await instance.GetDrillsNamesInWorkOut(testWorkoutId);

            // Output the results
            if (drills != null && drills.Count > 0)
            {
                Console.WriteLine("Drill names for Workout ID {0}:", testWorkoutId);
                foreach (var drill in drills)
                {
                    Console.WriteLine(drill);
                }
            }
            else
            {
                Console.WriteLine("No drills found for Workout ID {0}.", testWorkoutId);
            }
        }
    }
}
