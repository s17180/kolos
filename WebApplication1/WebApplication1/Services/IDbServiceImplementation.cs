using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Responses;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using Task = WebApplication1.Models.Task;

namespace WebApplication1.Services
{
    public class IDbServiceImplementation : IDbService
    {
        string ConString = "Data Source=db-mssql;Initial Catalog=s17180;Integrated Security=True";

        public bool AddTask(TaskRequest req)
        {
            if (req == null)
            {
                throw new AppException("wprowadzony task nie moze byc null");
            }

            try
            {
                using (var con = new SqlConnection(ConString))

                {
                    con.Open();
                    using (var com = new SqlCommand("select * from tasktype where idtasktype = @tasktype", con))
                    {
                        com.Parameters.AddWithValue("tasktype", req.IdTaskType);
                        using (var reader = com.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                reader.Close();
                                using (var command0 = new SqlCommand("INSERT INTO TaskType (idtasktype,name) VALUES (@id,'CREATED')", con))
                                {
                                    command0.Parameters.AddWithValue("id", req.IdTaskType);
                                    command0.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    using (var com = new SqlCommand("Insert Into Task(IdTask, Name, Description, Deadline, IdTeam, IdTaskType, IdAssignedTo, IdCreator)" +
                            "Values(@a,@b,@c,@d,@e,@f,@g,@h)", con))
                    {


                        com.Parameters.AddWithValue("a", req.IdTask);
                        com.Parameters.AddWithValue("b", req.Name);
                        com.Parameters.AddWithValue("c", req.Description);
                        com.Parameters.AddWithValue("d", req.Deadline);
                        com.Parameters.AddWithValue("e", req.IdProject);
                        com.Parameters.AddWithValue("f", req.IdTaskType);
                        com.Parameters.AddWithValue("g", req.IdAssignedTo);
                        com.Parameters.AddWithValue("h", req.IdCreator);
                        com.ExecuteNonQuery();
                        return true;
                    }

                }
            }

            catch (SqlException e)
            {
                throw new AppException("cos poszlo nie tak!");
            }
            return false;
        }

        public List<Task> GetTasks(int id)
        {
            var tasks = new List<Task>();
          
            using (var con = new SqlConnection(ConString))
            using (var commands = new SqlCommand())
            {
                commands.Connection = con;
                commands.CommandText = "select task.idTask,task.name,task.description,task.deadline,task.idproject,task.idtasktype,task.idassignedto,task.idcreator from task inner join project on task.idProject = " +
                    "project.idproject where project.idproject = @id order by task.deadline desc;";

                commands.Parameters.Add(new SqlParameter("id", id));

                con.Open();

                var dr = commands.ExecuteReader();

                while (dr.Read())
                {
                    tasks.Add(
                        new Models.Task
                        {
                            IdTask = Convert.ToInt32(dr["IdTask"].ToString()),
                            Name = dr["Name"].ToString(),
                            Description = dr["Description"].ToString(),
                            Deadline = dr["Deadline"].ToString(),
                            IdProject = Convert.ToInt32(dr["IdProject"].ToString()),
                            IdTaskType = Convert.ToInt32(dr["IdTaskType"].ToString()),
                             IdAssignedTo =Convert.ToInt32(dr["IdProject"].ToString()),
                            IdCreator = Convert.ToInt32(dr["IdCreator"].ToString())
                        });
                }
                dr.Close();
            }

            return tasks;

        }
    }
}
