using HandlebarPractice.Interfaces;
using HandlebarPractice.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HandlebarPractice.Services
{

    public class CustomerService : ICustomer
    {
        private readonly string _connectionString;

        public CustomerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        #region Get Customer By Id


        public Customer GetCustomerById(long id)
        {
            Customer customer = null;

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("usp_Customer_GetById", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Parameters
            cmd.Parameters.AddWithValue("@lngSCustRepId", id);

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                customer = MapCustomer(reader);
            }

            return customer;
        }
        #endregion

        #region Get All Customers
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using SqlConnection con = new SqlConnection(_connectionString);
            // Updated to include the schema name 'Setup'
            using SqlCommand cmd = new SqlCommand("usp_Customer_GetAll", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(MapCustomer(reader));
            }

            return customers;
        }
        #endregion

        #region Save (Insert / Update)
        public void SaveCustomer(Customer customer)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("usp_Customer_Save", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SCustId", customer.SCustId);
            cmd.Parameters.AddWithValue("@SCustName", customer.SCustName);
            cmd.Parameters.AddWithValue("@SCustPhone", customer.SCustPhone);
            cmd.Parameters.AddWithValue("@SCustFax", customer.SCustFax);
            cmd.Parameters.AddWithValue("@SCustEmail", customer.SCustEmail);
            cmd.Parameters.AddWithValue("@SCustAddress1", customer.SCustAddress1);
            cmd.Parameters.AddWithValue("@SCustAddress2", customer.SCustAddress2);
            cmd.Parameters.AddWithValue("@SCustCity", customer.SCustCity);
            cmd.Parameters.AddWithValue("@SCustState", customer.SCustState);
            cmd.Parameters.AddWithValue("@SCustZip", customer.SCustZip);
            cmd.Parameters.AddWithValue("@SCustComments", customer.SCustComments);


            con.Open();
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Delete (Soft Delete)
        public void DeleteCustomer(long id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("usp_Customer_Delete", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SCustId", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Mapper
        private static Customer MapCustomer(IDataReader reader)
        {
            return new Customer
            {
                SCustId = reader["SCustId"] != DBNull.Value ? Convert.ToInt64(reader["SCustId"]) : 0,

                SCustALId = reader["SCustALId"] != DBNull.Value
                            ? Convert.ToInt64(reader["SCustALId"])
                            : null,

                SCustName = reader["SCustName"] as string,
                SCustPhone = reader["SCustPhone"] as string,
                SCustFax = reader["SCustFax"] as string,
                SCustEmail = reader["SCustEmail"] as string,

                SCustAddress1 = reader["SCustAddress1"] as string,
                SCustAddress2 = reader["SCustAddress2"] as string,
                SCustCity = reader["SCustCity"] as string,
                SCustState = reader["SCustState"] as string,
                SCustZip = reader["SCustZip"] as string,

                SShippingAddress = reader["SShippingAddress"] as string,
                SCustComments = reader["SCustComments"] as string,

                SCustSetupBy = reader["SCustSetupBy"] != DBNull.Value
                                ? Convert.ToInt64(reader["SCustSetupBy"])
                                : null,

                SCustSetupDate = reader["SCustSetupDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["SCustSetupDate"])
                                : null,

                SCustUpdateBy = reader["SCustUpdateBy"] != DBNull.Value
                                ? Convert.ToInt64(reader["SCustUpdateBy"])
                                : null,

                SCustUpdateDate = reader["SCustUpdateDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["SCustUpdateDate"])
                                : null,

                SCustRemoveBy = reader["SCustRemoveBy"] != DBNull.Value
                                ? Convert.ToInt64(reader["SCustRemoveBy"])
                                : null,

                SCustRemoveDate = reader["SCustRemoveDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["SCustRemoveDate"])
                                : null
            };
        }

        #endregion
    }

}
