﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class SoHoKhauDAO:DBConnection<SoHoKhauDTO>
    {
        public SoHoKhauDAO() : base() { }

        public override DataSet getAll()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                sqlda = new MySqlDataAdapter("SELECT *, 'Delete' as 'Change' FROM sohokhau", conn);
                cmdbuilder = new MySqlCommandBuilder(sqlda);
                sqlda.InsertCommand = cmdbuilder.GetInsertCommand();
                sqlda.UpdateCommand = cmdbuilder.GetUpdateCommand();
                sqlda.DeleteCommand = cmdbuilder.GetDeleteCommand();
                dataset = new DataSet();
                sqlda.Fill(dataset, "sohokhau");
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

        public override bool insert_table(SoHoKhauDTO data)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                DataRow dr = dataset.Tables["sohokhau"].NewRow();
                dr["sosohokhau"] = data.SoSoHoKhau;
                dr["machuho"] = data.MaChuHoThuongTru;
                dr["diachi"] = data.DiaChi;
                dr["ngaycap"] = data.NgayCap;
                dr["sodangky"] = data.SoDangKy;

                dataset.Tables["sohokhau"].Rows.Add(dr);
                dataset.Tables["sohokhau"].Rows.RemoveAt(dataset.Tables["sohokhau"].Rows.Count - 1);
                sqlda.Update(dataset, "sohokhau");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            } 
            return true;
        }
        public override bool insert(SoHoKhauDTO sohk)
        {
            try
            {

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string sql = "insert into sohokhau values(@sosohokhau, @machuho,  @diachi, @ngaycap, @sodangky)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sosohokhau", sohk.SoSoHoKhau);
                cmd.Parameters.AddWithValue("@machuho", sohk.MaChuHoThuongTru);
                cmd.Parameters.AddWithValue("@diachi", sohk.DiaChi);
                cmd.Parameters.AddWithValue("@ngaycap", sohk.NgayCap);
                cmd.Parameters.AddWithValue("@sodangky", sohk.SoDangKy);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
            return true;

        }
        public bool XoaSoHK(string soSoHoKhau)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string sql = "delete from sohokhau where sosohokhau=@sosohokhau";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sosohokhau", soSoHoKhau);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }
        public override bool delete(int row)
        {
            try
            {
                dataset.Tables["sohokhau"].Rows[row].Delete();
                sqlda.Update(dataset, "sohokhau");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;

        }

        public override bool update(SoHoKhauDTO sohk, int r)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {

                string sql = "update sohokhau set machuho=@chuho, diachi=@diachithuongtru, ngaycap=@ngaycap, sodangky=@sodangky" +
                    " where sosohokhau=@sosohokhau";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sosohokhau", sohk.SoSoHoKhau);
                cmd.Parameters.AddWithValue("@chuho", sohk.MaChuHoThuongTru);
                cmd.Parameters.AddWithValue("@diachithuongtru", sohk.DiaChi);
                cmd.Parameters.AddWithValue("@ngaycap", sohk.NgayCap.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@sodangky", sohk.SoDangKy);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            } 
            
        }
        public DataSet TimKiem(string query)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                if (!String.IsNullOrEmpty(query)) query = "where " + query;
                sqlda = new MySqlDataAdapter("SELECT * FROM sohokhau " + query, conn); /*where sosohokhau IS NOT NULL*/
                cmdbuilder = new MySqlCommandBuilder(sqlda);
                sqlda.InsertCommand = cmdbuilder.GetInsertCommand();
                sqlda.UpdateCommand = cmdbuilder.GetUpdateCommand();
                sqlda.DeleteCommand = cmdbuilder.GetDeleteCommand();
                dataset = new DataSet();
                sqlda.Fill(dataset, "sohokhau");
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



        /// <summary>
        /// Tự động tạo mã 12 ký tự cho mã định danh
        /// </summary>
        /// <param name="gioitinh"></param>
        /// <param name="namsinh"></param>
        /// <returns></returns>


    }
    
}
