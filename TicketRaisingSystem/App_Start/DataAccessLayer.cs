using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace TicketRaisingSystem.App_Start
{
    public class DataAccessLayer
    {
        string Str = ConfigurationManager.ConnectionStrings["TicketRaisingSystem"].ConnectionString;
        SqlConnection Con; SqlCommand Cmd;
        #region Execute Scalar
        public string InsertExecuteScalar(string StoredProcedure, string[] ParameterNames, string[] ParameterValues)
        {
            string Result = string.Empty;
            try
            {
                Con = new SqlConnection(Str); Cmd = new SqlCommand(); Cmd.Connection = Con; Cmd.CommandText = StoredProcedure; Cmd.Parameters.Clear();
                if (ParameterNames.Length == ParameterValues.Length)
                {
                    for (int i = 0; i < ParameterNames.Length; i++)
                    {
                        Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                    }
                    Con.Open();
                    Result = Cmd.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(Result))
                        return Result;
                    else
                        return ("Data not returning Value.");
                }
                else { new Exception("ParameterNames and ParameterValues are not same."); }
            }
            catch (Exception Ex)
            {
                new Exception("Something went wrong.", Ex);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                    Con.Close();
            }
            return Result;
        }
        #endregion
        #region Execute NonQuery
        public bool InsertData(string StoredProcedure, string[] ParameterNames, string[] ParameterValues)
        {
            bool Result = false;
            try
            {
                Con = new SqlConnection(Str); Cmd = new SqlCommand(); Cmd.Connection = Con; Cmd.CommandText = StoredProcedure; Cmd.Parameters.Clear();
                if (ParameterNames.Length == ParameterValues.Length)
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < ParameterNames.Length; i++)
                    {
                        Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                    }
                    Con.Open();
                    int A = Cmd.ExecuteNonQuery();
                    if (A > 0)
                        Result = true;
                    else
                        Result = false;
                }
                else { new Exception("ParameterNames and Values are not equal."); }
            }
            catch (Exception Ex)
            {
                new Exception("Something went wrong.", Ex);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
            return Result;
        }
        #endregion
        #region DataRetrival
        public DataSet RetriveData(string StoredProcedure, string[] ParameterNames = null, string[] ParameterValues = null)
        {
            DataSet DsResult = new DataSet();
            try
            {
                Con = new SqlConnection(Str); Cmd = new SqlCommand(); Cmd.Connection = Con; Cmd.CommandText = StoredProcedure; Cmd.Parameters.Clear();
                if (ParameterNames != null && ParameterValues != null)
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < ParameterValues.Length; i++)
                    {
                        Cmd.Parameters.AddWithValue(ParameterNames[i], ParameterValues[i]);
                    }
                }
                SqlDataAdapter Da = new SqlDataAdapter(Cmd);
                Da.Fill(DsResult);
                if (DsResult.Tables[0].Rows.Count > 0)
                    return DsResult;
                else
                    return DsResult;
            }
            catch (Exception Ex)
            {
                new Exception("Something went wrong.", Ex);
            }
            return DsResult;
        }
        #endregion
    }
}