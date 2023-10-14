using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml.Linq;
using static Class1;

namespace RepositoryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinedDate { get; set; }
        public IList<string> Courses { get; set; }
    }

    public class Service1 : IService1
    {
        int getLatestIndex()
        {
            string filePath = "D:\\Data_dump\\index.txt";
            int number;
            if (File.Exists(filePath))
            {
                // Read JSON data from an existing file
                string indexText = File.ReadAllText(filePath);
                
                try
                {
                    number = int.Parse(indexText);
                    Console.WriteLine("Parsed integer: " + number);
                }
                catch (FormatException)
                {
                    Console.WriteLine("The input is not a valid integer.");
                    number = -1;
                }
            }
            else
            {
                // Create a new JSON file with dummy data
                number = 0;
                File.WriteAllText(filePath, $"{number}");
            }
            return number;
        }

        int setLatestIndex(int number)
        {
            string filePath = "D:\\Data_dump\\index.txt";
            string numberAsString = $"{number}";
            
            File.WriteAllText(filePath, numberAsString);
            return 1;
        }
        string IService1.createStudent(string Name, string LastName, string Details)
        {
            var serializer = new JavaScriptSerializer();
            //Connect to the database
            string filePath = "D:\\Data_dump\\data.json"; // Replace with the path to your JSON file 
            int id = getLatestIndex();
            id++;
            string newData = "";
            Student newStudent = new Student
            {
                StudentId = id,
                FirstName = Name,
                LastName = LastName,
                JoinedDate = new System.DateTime(),
                Courses = new List<string>
                {
                    "CSE 445",
                    "CSE 545"
                }
            };
            try
            {
                // Code that might throw an exception
                if (File.Exists(filePath))
                {
                    // Read JSON data from an existing file
                    string jsonData = File.ReadAllText(filePath);
                    List<Student> data = serializer.Deserialize<List<Student>>(jsonData);

                    // Process the data
                    data.Add(newStudent);
                    newData = serializer.Serialize(data);
                    File.WriteAllText(filePath, newData);
                }
                else
                {
                    // Create a new JSON file with dummy data
                    string newStudentJson = serializer.Serialize(newStudent);
                    File.WriteAllText(filePath, newStudentJson);
                }
                setLatestIndex(id);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return ex.Message;
            }
            finally
            {
                // This block will always be executed
                Console.WriteLine("Cleanup or finalization code goes here.");
            }
            // Read the file as JSON object, preferably like ORM


            // Add the query to write to the data base
            // Update the JSON data, and override the complete data to JSON again

            // Call save to DB
            // Save the JSON to text file.
            return "Success Created";
        }

        string IService1.updateStudent(string studentId, string studentJson)
        {
            var serializer = new JavaScriptSerializer();
            //Connect to the database
            string filePath = "D:\\Data_dump\\data.json"; // Replace with the path to your JSON file 
            //int id = getLatestIndex();
            int id;
            try
            {
                // Search the record with studentId
                Student gotStudent = null;
                if (File.Exists(filePath))
                {
                    // Read JSON data from an existing file
                    string jsonData = File.ReadAllText(filePath);
                    List<Student> data = serializer.Deserialize<List<Student>>(jsonData);

                    // Process the data
                    int savedIndex = 0;
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (int.Parse(studentId) == data[i].StudentId)
                        {
                            gotStudent = data[i];
                            savedIndex = i;
                            break;
                        }
                    }
                    id = gotStudent.StudentId;
                    // Remove the student
                    data.Remove(gotStudent);
                    // get the new student details from string.

                    Student newStudent = serializer.Deserialize<Student>(studentJson);
                    newStudent.JoinedDate = DateTime.Now;
                    newStudent.StudentId = id;
                    
                    data.Add(newStudent);
                    string updatedData = serializer.Serialize(data);
                    File.WriteAllText(filePath, updatedData);
                }
                else
                {
                    id = 1;
                    // Create a new JSON file with dummy data
                    Student newStudent = serializer.Deserialize<Student>(studentJson);
                    newStudent.JoinedDate = DateTime.Now;
                    newStudent.StudentId = id;
                    setLatestIndex(id);
                    List<Student> list = new List<Student>();
                    list.Add(newStudent);
                    string updatedData = serializer.Serialize(list);
                    File.WriteAllText(filePath, updatedData);
                }
                
                // Delete the record with the Id. Insert new record with Id
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
            return "Successfully Updated";
        }

        string IService1.readStudents()
        {
            var serializer = new JavaScriptSerializer();
            string filePath = "D:\\Data_dump\\data.json"; // Replace with the path to your JSON file 
            
            try
            {
                if (File.Exists(filePath))
                {
                    // Read JSON data from an existing file
                    string jsonData = File.ReadAllText(filePath);
                    
                    return jsonData;
                }
                else
                {
                    return "No data found";
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        string IService1.deleteStudent(string studentId)
        {
            var serializer = new JavaScriptSerializer();

            //Connect to the database
            string filePath = "D:\\Data_dump\\data.json"; // Replace with the path to your JSON file 
            //int id = getLatestIndex();
            int id;
            try
            {
                // Search the record with studentId
                Student gotStudent = null;
                if (File.Exists(filePath))
                {
                    // Read JSON data from an existing file
                    string jsonData = File.ReadAllText(filePath);
                    List<Student> data = serializer.Deserialize<List<Student>>(jsonData);

                    // Process the data
                    int savedIndex = 0;
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (int.Parse(studentId) == data[i].StudentId)
                        {
                            gotStudent = data[i];
                            savedIndex = i;
                            break;
                        }
                    }
                    id = gotStudent.StudentId;
                    // Remove the student
                    data.Remove(gotStudent);
                    
                    string updatedData = serializer.Serialize(data);
                    File.WriteAllText(filePath, updatedData);
                }
                else
                {
                    return "No data to delete";
                }

                // Delete the record with the Id. Insert new record with Id
            }
            catch (Exception e)
            {

                return e.Message;
            }

            return "Successfully Deleted Student";
        }
    }
}
