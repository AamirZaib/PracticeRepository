using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LevelsDB
/// </summary>
public class LevelsDB
{
	public LevelsDB()
	{
		
	}


    public DataSet Level_GetAll()
    {
        DBManager objDbManager = new DBManager();

        return objDbManager.ExecuteDataSet("GETALLMASTERLEVELFORMEMR");
    }


    

     public DataSet DetailLevel_GetAll(long LevelItemId)
    {
        DBManager objDbManager = new DBManager();

        objDbManager.AddParameter("LevelItemId", LevelItemId);

        return objDbManager.ExecuteDataSet("GETALLMASTERDetailLEVELFORMEMR");
    }

    public long Addlevel(LevelBO objlevelCodes, SqlTransaction objSqlTransaction = null)
    {
        try
        {
            DBManager objDBManager = new DBManager(objSqlTransaction);
            //check



            objDBManager.AddParameter("LevelId", objlevelCodes.LevelId);
            objDBManager.AddParameter("LevelName", objlevelCodes.LevelName);
            objDBManager.AddParameter("CreatedById", objlevelCodes.CreatedById);
            objDBManager.AddParameter("CreatedDate", objlevelCodes.CreatedDate);
            //objDBManager.AddParameter("Deleted", objlevelCodes.Deleted);
           // objDBManager.AddParameter("Totalweightage", objlevelCodes.Totalweightage);
            objDBManager.ExecuteNonQuery("InsertLEVEL");

            return long.Parse(objDBManager.Parameters[0].Value.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public long AddSublevel(LevelBO objlevelCodes, SqlTransaction objSqlTransaction = null)
    
    {
        try
        {
            DBManager objDBManager = new DBManager(objSqlTransaction);
            //check

             
            objDBManager.AddParameter("LevelId", objlevelCodes.LevelId);


            objDBManager.AddParameter("LevelItemId", objlevelCodes.LevelItemId);
           
            objDBManager.AddParameter("LevelItemName", objlevelCodes.LevelItemName);
            objDBManager.AddParameter("Weightage", objlevelCodes.weightage);
            objDBManager.AddParameter("CreatedById", objlevelCodes.CreatedById);
            objDBManager.AddParameter("CreatedDate", objlevelCodes.CreatedDate);

            objDBManager.ExecuteNonQuery("LevelItems_Add");

            return long.Parse(objDBManager.Parameters[0].Value.ToString());
        }
       catch (Exception ex)
        {
            throw ex;
        }
    }





    public long AddDetaillevel(LevelBO objlevelCodes, SqlTransaction objSqlTransaction = null)
    {
        try
        {
            DBManager objDBManager = new DBManager(objSqlTransaction);
            //check


         


            objDBManager.AddParameter("LevelItemId", objlevelCodes.LevelItemId);
            objDBManager.AddParameter("LevelItemDetailId", objlevelCodes.LevelItemDetailId);
            objDBManager.AddParameter("ItemDetail", objlevelCodes.ItemDetail);
            objDBManager.AddParameter("CreatedById", objlevelCodes.CreatedById);
            objDBManager.AddParameter("CreatedDate", objlevelCodes.CreatedDate);

            objDBManager.ExecuteNonQuery("DetailLevelItems_Add");

            return long.Parse(objDBManager.Parameters[0].Value.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    //EditSubLevel
    public int EditSubLevel(LevelBO objlevelCodes, SqlTransaction objSqlTransaction = null)
    {
        try
        {
            DBManager objDBManager = new DBManager();


            objDBManager.AddParameter("LevelId", objlevelCodes.LevelId);
            objDBManager.AddParameter("LevelItemId", objlevelCodes.LevelItemId);
            objDBManager.AddParameter("LevelItemName", objlevelCodes.LevelItemName);
            objDBManager.AddParameter("Weightage", objlevelCodes.weightage);
            objDBManager.AddParameter("ModifiedById", objlevelCodes.ModifiedById);
            objDBManager.AddParameter("ModifiedDate", objlevelCodes.ModifiedDate);

            return objDBManager.ExecuteNonQuery("Update_SubLevel");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int DeleteSublevel(LevelBO objlevelCodes, SqlTransaction objSqlTransaction = null)
    {// need check
        try
        {
            DBManager objDBManager = new DBManager(objSqlTransaction);

            objDBManager.AddParameter("LevelItemId", objlevelCodes.LevelItemId);
            objDBManager.AddParameter("ModifiedById", objlevelCodes.ModifiedById);
            objDBManager.AddParameter("ModifiedDate", objlevelCodes.ModifiedDate);

            return objDBManager.ExecuteNonQuery("Sublevel_Delete");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}