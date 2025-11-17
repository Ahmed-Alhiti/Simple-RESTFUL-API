
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;



class Program
{
    static readonly HttpClient _httpClient = new HttpClient();
    

    static async Task Main(string[] args)
    {
        _httpClient.BaseAddress = new Uri("https://localhost:7143/api/StudentAPI/");
        
        await GetStudents();
        await GetPassedStudents();
        await GetAvrageStudents();
        await GetStudentBy_ID(3);

        var student = new clsStudent { age = 25, Name = "ameen", Grade = 95 };

        await AddNewStudent(student);

        await DeleteStudent(0);
        var updatedstudent = new clsStudent { age = 25, Name = "Ahmed fawaz al-hiti", Grade = 99 };
        await UpdateStudent(4, updatedstudent);

        await GetStudents();
    }

    static async Task GetStudents()
    {
        try
        {
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API");
            var student = await _httpClient.GetFromJsonAsync<List<clsStudent>>("all");

            if (student != null)
            {
                foreach (var item in student)
                {
                    Console.WriteLine($"ID :{item.Id} Name :{item.Name} Age :{item.age} Grade :{item.Grade} ");
                }
                
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task GetPassedStudents()
    {
        try
        {
            
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API / Passed Student");
            var student = await _httpClient.GetFromJsonAsync<List<clsStudent>>("passed");

            if (student != null)
            {
                foreach (var item in student)
                {
                    Console.WriteLine($"ID :{item.Id} Name :{item.Name} Age :{item.age} Grade :{item.Grade} ");
                }
                
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task GetAvrageStudents()
    {
        try
        {
            
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API / Avrage Student");
            var student = await _httpClient.GetFromJsonAsync<double>("avrage");
            Console.WriteLine($"Avrage :{student} ");
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task GetStudentBy_ID(int id)
    {
        try
        {
            
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API / Student By ID");

            var respons = await _httpClient.GetAsync($"{id}");

            if (respons.IsSuccessStatusCode)
            {
                var student = await respons.Content.ReadFromJsonAsync<clsStudent>();
                if (student != null)
                {
                    Console.WriteLine($"ID :{student.Id} Name :{student.Name} Age :{student.age} Grade :{student.Grade}");
                }
            }
            else if (respons.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Student not found");
            }
            else 
            {
                Console.WriteLine($"Bad requst : Not accepted id {id}");            
            }                        
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task AddNewStudent(clsStudent student)
    {
        try
        {
            
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API / Adding New Student");

            var respons = await _httpClient.PostAsJsonAsync("",student);

            if (respons.IsSuccessStatusCode)
            {
                var addedstudent = await respons.Content.ReadFromJsonAsync<clsStudent>();
                Console.WriteLine($"ID:{addedstudent.Id} Name:{addedstudent.Name} Age:{addedstudent.age} Grade:{addedstudent.Grade}");
            }
            else if (respons.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"Bad requst : Invaild Data");
            }
                                    
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task DeleteStudent(int id)
    {
        try
        {
            
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API / Delete Student");

            var respons = await _httpClient.DeleteAsync($"{id}");

            if (respons.IsSuccessStatusCode)
            {
                Console.WriteLine($"student with id :{id} has deleted succesfuly");
            }
            else if (respons.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"Bad requst : Invaild Data");
            }
            else if (respons.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Not Found");
            }
                                    
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    static async Task UpdateStudent(int id , clsStudent student)
    {
        try
        {
            
            Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>");
            Console.WriteLine("Student API / Update Student");

            
            var respons = await _httpClient.PutAsJsonAsync($"{id}",student);

            if (respons.IsSuccessStatusCode)
            {
                Console.WriteLine($"student with id :{id} has Updated succesfuly");
            }
            else if (respons.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"Bad requst : Invaild Data");
            }
            else if (respons.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Not Found");
            }
                                    
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }



    public class clsStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public int Grade { get; set; }

    }
}
