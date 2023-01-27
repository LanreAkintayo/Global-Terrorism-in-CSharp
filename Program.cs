using System;
using System.IO;
using System.Linq;

namespace GlobalTerrorismProgram
{

    class Feature
    {
        private string feature;

        public Feature(string _feature)
        {
            feature = _feature;
        }

        public int GetLength()
        {
            return feature.Split(",").Length;
        }

        public int[] GetFeaturesArray()
        {
            int length = GetLength();
            int[] features = new int[length];

            int i = 0;

            foreach (string digit in feature.Split(","))
            {
                features[i] = Convert.ToInt32(digit);
                i++;
            }

            return features;
        }

        public int GetSum()
        {
            int sum = 0;
            foreach (int digit in GetFeaturesArray())
            {
                sum += digit;
            }

            return sum;
        }

        public string GetFeature(){
            return feature;
        }
    }

    class Program
    {

        public static int GetID(Feature feature, int index, Feature[] allFeatures)
        {

            int sum = feature.GetSum();

            int id = -1;

            for (int i = 0; i < allFeatures.Length; i++)
            {
                Feature currentFeature = allFeatures[i];
                int currentSum = currentFeature.GetSum();

                if (Enumerable.SequenceEqual(feature.GetFeaturesArray(), currentFeature.GetFeaturesArray()) && sum == currentSum && index != i)
                {
                    id = i + 1;
                }
            }
            return id;
        }

         public static void DisplayOutput(int[] ids, Feature[] features){

            Console.WriteLine("{0, -10} {1, -20} {2, -10} {3, -10}", "S/N", "Features", "Sum", "Similarity ID");
                Console.WriteLine("----------------------------------------------------------------");

            for(int i = 0; i < ids.Length; i++)
            {
                string currentFeature = features[i].GetFeature();
                int sum = features[i].GetSum();
                Console.WriteLine("{0, -10} {1, -20} {2, -10} {3, -10}", i + 1, currentFeature, sum, ids[i] == -1 ? "" : ids[i]);
                Console.WriteLine("----------------------------------------------------------------");
            }
        }

        public static void Main(String[] args)
        {
            // Open the file
            string terrorismFile = "terrorism.txt";
            StreamReader file = new StreamReader(terrorismFile);

            // Declare some variables for later
            int noOfFeatures = Convert.ToInt32(file.ReadLine());
            Feature[] features = new Feature[noOfFeatures];
            int[] ids = new int[noOfFeatures];

            // Populate the features array with features from the file
            for (int i = 0; i < noOfFeatures; i++)
            {
                Feature currentFeature = new Feature(file.ReadLine());
                features[i] = currentFeature;
            }

            // Obtain the ID of each feature and then fill up the id array with those IDs.
            for (int i = 0; i < noOfFeatures; i++)
            {
                Feature feature = features[i];
                int ID = GetID(feature, i, features);
                ids[i] = ID;
            }

            DisplayOutput(ids, features);
        }
    }
}