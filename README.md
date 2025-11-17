# Simple-RESTFUL-API
# 🎓 StudentAPI

**StudentAPI** is a RESTful API built with **ASP.NET Core** for managing student data.  
It allows you to **add, update, delete, and retrieve students**, calculate the **average grade**, and manage student data efficiently.

---

## ⚡ Features

- ✅ Get all students  
- ✅ Get passed students  
- ✅ Calculate the average grade  
- ✅ Get a student by ID  
- ✅ Add a new student  
- ✅ Update an existing student  
- ✅ Delete a student  
- ✅ Upload student images  

---

## 🌐 API Endpoints

| Method | Endpoint | Description |
|--------|---------|-------------|
| GET    | `/api/StudentAPI/All` | Get all students |
| GET    | `/api/StudentAPI/Passed` | Get all passed students |
| GET    | `/api/StudentAPI/Avrage` | Get the average grade of all students |
| GET    | `/api/StudentAPI/{id}` | Get a student by ID |
| POST   | `/api/StudentAPI` | Add a new student |
| PUT    | `/api/StudentAPI/{id}` | Update an existing student by ID |
| DELETE | `/api/StudentAPI/{id}` | Delete a student by ID |
| POST   | `/api/StudentAPI/UploadImage` | Upload an image file for a student |

---

## 📝 Request Body Example

### Add or Update a student
```json
{
  "Name": "Ahmed",
  "age": 25,
  "Grade": 95
}
