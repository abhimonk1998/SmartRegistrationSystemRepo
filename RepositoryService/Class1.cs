using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

public class Class1
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinedDate { get; set; }
        public IList<string> Courses { get; set; }
    }
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            var serializer = new JavaScriptSerializer();
            //string jsonString = serializer.Serialize(student);
            //var deserializedResult = serializer.Deserialize<List<Student>>(jsonString);
            //Connect to the database
            string filePath = "data.json"; // Replace with the path to your JSON file 
            Student newStudent = new Student
            {
                FirstName = "Abhijeet",
                LastName = "Khedekar",
                JoinedDate = new System.DateTime(),
                Courses = new List<string>
                {
                    "CSE 446",
                    "CSE 578"
                }
            };

            // Read the file as JSON object, preferably like ORM
            if (File.Exists(filePath))
            {
                // Read JSON data from an existing file
                string jsonData = File.ReadAllText(filePath);
                List<Student> data = serializer.Deserialize<List<Student>>(jsonData);
                Console.WriteLine("Capacity is" + data.Capacity);
                // Add the query to write to the data base
                // Update the JSON data, and override the complete data to JSON again

                data.Add(newStudent);
                string newData = serializer.Serialize(data);
                File.WriteAllText(filePath, newData);
            } else
            {
                // Create a new JSON file with dummy data
                string newStudentJson = serializer.Serialize(newStudent);
                File.WriteAllText(filePath, newStudentJson);
            }
            // Call save to DB
            // Save the JSON to text file.
            
            Console.Read();
        }
    }
}
