using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using StudentAPI.DataSimulation;
//using StudentAPI.Model;
using Business;
using DataAccess;

namespace StudentAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/StudentAPI")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudent()
        {
            List<StudentDTO> students = Student.GetAllStudents();
            if (students.Count == 0) 
            { 
                return NotFound("No data");
            }

            return Ok(students);
        }

        [HttpGet("Passed",Name = "Get_Passed_Students")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllPassedStudent()
        {
            var passed = Student.GetPassedStudents();

            if (passed.Count == 0)
            {
                return NotFound("There's no data");
            }
            return Ok(passed);
        }

        [HttpGet("Avrage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAllAvrageStudent()
        {
            //StudentData.Students.Clear();
            var Students = DataAccess.StudentData.GetAllStudents();
            if(Students.Count == 0)
            {
                return NotFound("There's no data");
            }
            return Ok(Students.Average(student => student.Grade));
        }


        [HttpGet("{id}",Name ="GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentByID(int id) 
        {

            if (id<1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

        
            var student = Student.Find(id);

            if (student == null) {

                return NotFound($"Student with id {id} is not found");
            }
            return Ok(student.SDTO);
        
        }

        [HttpPost(Name ="AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<StudentDTO> AddStudent(StudentDTO newstudent) 
        {

            if (newstudent == null || string.IsNullOrEmpty(newstudent.Name) || newstudent.Age < 0 || newstudent.Grade < 0)
            {
                return BadRequest($"Invalid Data");
            }
            //var Students = DataAccess.StudentData.GetAllStudents();
            Student student = new Student(newstudent,Student.enMode.AddNew);
            student.Save();
            newstudent.Id = student.ID;
            return CreatedAtRoute("GetStudentByID", new {id = newstudent.Id} , newstudent);
        
        }

        [HttpDelete("{id}",Name ="DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> DeleteStudentBy(int id) 
        {

            if (id <= 0) 
            {
                return BadRequest("not accepted id");
            }

            Student student=Student.Find(id);
            if (student == null)
            {
                return NotFound($"There's no student with id :{id}");
            }

            if (!Student.DeleteStudent(id))
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }


            return Ok("Deleted succesfuly");
        
        }

        [HttpPut("{id}",Name ="UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<StudentDTO> UpdateStudent(int id,StudentDTO student1) 
        {

            if (id <= 0) 
            {
                return BadRequest("not accepted id");
            }

            Student student = Student.Find(id);

            if (student == null)
            {
                return NotFound($"There's no student with id :{id}");
            }

            student.Name= student1.Name;
            student.Age= student1.Age;
            student.Grade = student1.Grade;

            if (student.Save())
            {
                return Ok("Updated succesfuly");
            }
            return BadRequest(StatusCodes.Status500InternalServerError);
            
        }

        [HttpPost("UploadImage")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UploadImage(IFormFile ImageFile) 
        {

            if (ImageFile == null || ImageFile.Length==0) 
            {
                return BadRequest("no file uploaded");
            }

            var directory = @"D:\MyUploades";

            var filename=Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath=Path.Combine(directory, filename);

            if (!Directory.Exists(directory)) 
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            return Ok( new { filePath });


        }


    }
}
