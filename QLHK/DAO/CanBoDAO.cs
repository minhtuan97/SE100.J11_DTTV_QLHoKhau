﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using DTO;
using MySql.Data.MySqlClient;

namespace DAO
{
    public class CanBoDAO : DBConnection
    {
        public CanBoDAO() : base() { }
        public DataSet GetAllCanBo()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                }
                sqlda = new MySqlDataAdapter("SELECT *, 'Delete' as 'Change' FROM canbo", conn);
                cmdbuilder = new MySqlCommandBuilder(sqlda);
                sqlda.InsertCommand = cmdbuilder.GetInsertCommand();
                sqlda.UpdateCommand = cmdbuilder.GetUpdateCommand();
                sqlda.DeleteCommand = cmdbuilder.GetDeleteCommand();
                dataset = new DataSet();
                sqlda.Fill(dataset, "canbo");
                return dataset;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public bool AddCanBo( CanBo cb)
        {
            try
            {
                DataRow dr = dataset.Tables["canbo"].NewRow();
                dr["macanbo"] = cb.MaCanBo;
                dr["tendangnhap"] = cb.TenDangNhap;
                dr["matkhau"] = cb.MatKhau;
                dataset.Tables["canbo"].Rows.Add(dr);
                dataset.Tables["canbo"].Rows.RemoveAt(dataset.Tables["canbo"].Rows.Count - 1);
                sqlda.Update(dataset, "canbo");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return true;

        }
        public bool XoaCanBo(int row)
        {
            try
            {
                dataset.Tables["canbo"].Rows[row].Delete();
                sqlda.Update(dataset, "canbo");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;

        }
        public bool SuaCanBo(CanBo cb, int r)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();

            }
            try
            {
                string sql= "update canbo set tendangnhap =@tencb , matkhau=@mk where macanbo =@macb";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tencb", cb.TenDangNhap);
                cmd.Parameters.AddWithValue("@mk", cb.MatKhau);
                cmd.Parameters.AddWithValue("@macb", cb.MaCanBo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
