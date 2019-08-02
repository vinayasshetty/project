using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace studentlib
{
    public class Adoconnected
    {
        SqlConnection con;
        SqlCommand cmd;
        public Adoconnected()
        {
            con = new SqlConnection();
            //con.ConnectionString = "Data Source=localhost;Initial Catalog=studentresultmanagement;Integrated Security=True";
            //read connection string from config file
            string constr = ConfigurationManager.ConnectionStrings["sqlconstr"].ConnectionString;
            con.ConnectionString = constr;
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public List<student> SelectAllstudents()
        {
            try
            {
                List<student> stdLst = new List<student>();
                //configure command for select all stmt
                cmd.CommandText = "select * from student ";
                cmd.CommandType = CommandType.Text;
                //open connection
                con.Open();
                //Execute command
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    //retrieve the current record values
                    student std = new student();
                    std.usn = (string)sdr[0];
                    std.name = (string)sdr[1];
                    std.email = (string)sdr[2];
                    std.address = (string)sdr[3];
                    std.semester = (string)sdr[4];
                    //add the record to collection
                    stdLst.Add(std);
                }

                sdr.Close();
                return stdLst;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            con.Close();

        }
        public subject selectsubid(string branchid,int semester)
        {
            try
            {

                subject sb = new subject();
                cmd.CommandText = "select subjectid from subject where branchid=" + branchid+"semester="+semester;
                cmd.CommandType = CommandType.Text;
               
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   sb.subjectid = (string)(sdr[0]);
                   sb.subjectname = (String)(sdr[1]);
                   sb.branchid = (string)(sdr[2]);
                   sb.semester = (int)(sdr[3]);
                }

                return sb;
                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            con.Close();

        }
        public void Updatscore(string usn, int marks)
        {
            try
            {
                cmd.CommandText = "update marks set marks=@ma where usn="+usn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usn", usn);
                cmd.Parameters.AddWithValue("@ma", marks);
                con.Open();
                int recordsAffected = cmd.ExecuteNonQuery();
                if (recordsAffected == 0)
                {
                    throw new Exception("ecode does not exist");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            con.Close();
        }

    }
    
}
