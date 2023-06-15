using System.Data;
using System.Data.SqlClient;

namespace Validation.Models
{
    public class StudentDataBase
    {
        string _connectionString = string.Empty;
        public StudentDataBase(string connnectionString)
        {
            _connectionString = connnectionString;
        }

        public List<StudentModel> Students()
        {
            List<StudentModel> students = new List<StudentModel>();

            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_AllStudents";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        students.Add(new StudentModel()
                        {
                            RollNumber = reader.GetInt32("RollNumber"),
                            Name = reader.GetString("Name"),
                            Gender = reader.GetString("Gender"),
                            Mobile = reader.GetString("Mobile"),
                            Email = reader.GetString("Email"),
                            Age = reader.GetInt32("Age")
                        });
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return students;
        }

        public StudentModel StudentByRollNumber(int rollNumber)
        {
            StudentModel student = new StudentModel();

            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_StudentByRollNumber";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNumber", rollNumber);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        student = new StudentModel()
                        {
                            RollNumber = reader.GetInt32("RollNumber"),
                            Name = reader.GetString("Name"),
                            Gender = reader.GetString("Gender"),
                            Mobile = reader.GetString("Mobile"),
                            Email = reader.GetString("Email"),
                            Age = reader.GetInt32("Age")
                        };
                        break;
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return student;
        }

        public bool Insert(StudentModel student)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_InsertStudent";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                SqlParameter status = new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return (bool)status.Value;
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return false;
        }

        public bool Update(StudentModel student)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_UpdateStudent";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                SqlParameter status = new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return (bool)status.Value;
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return false;
        }

        public bool Delete(int rollNumber)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_DeleteStudent";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNumber", rollNumber);

                SqlParameter status = new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return (bool)status.Value;
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return false;
        }
    }
}
