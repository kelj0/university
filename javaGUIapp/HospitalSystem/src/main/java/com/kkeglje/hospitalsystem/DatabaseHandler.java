/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kkeglje.hospitalsystem;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.text.MessageFormat;

/**
 *
 * @author keljo
 */
public class DatabaseHandler {
    private static final String DEFAULT_JAVA_CLASS = "org.sqlite.JDBC";
    private final String PROJECT_DIRECTORY = System.getProperty("user.dir");
    private static final String URL_FORMAT = "jdbc:sqlite:MAIN_DATABASE.db";
    private static DatabaseHandler instance = null;
    public static Connection connection = null;
    
    private DatabaseHandler(String url) throws ClassNotFoundException{
        Class.forName(url);
        if(!new File(PROJECT_DIRECTORY + "/MAIN_DATABASE.db").exists()){
            ExecuteScript(PROJECT_DIRECTORY + "/database/build-db.sql");
        }
    }
    public void test(){System.out.println("test");}
    
    public static DatabaseHandler getInstance(){
        if(instance == null){
            try {
                instance = new DatabaseHandler(DEFAULT_JAVA_CLASS);
            } catch (ClassNotFoundException ex) {
                Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return instance;
    }
    
    public void ExecuteScript(String path){
        System.out.println("Executing script " + path);
        OpenConnection();
        String content = "";
        try {
            content = new String(Files.readAllBytes(Paths.get(path)));
        }catch (IOException ex) {
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
        for(String query : content.split(";")){
            ExecuteUpdate(query);
        }
        CloseConnection();
        System.out.println("Done");
    }
    
    public void ExecuteUpdate(String query){
        Statement statement = null;
        
        query = query.replaceAll("'", "\\'"); //por favor
        query = query.replaceAll("\"", "\\\"");
        try {
            statement = connection.createStatement();
            statement.executeUpdate(query);
        } catch (SQLException ex) {
            System.out.println("Why you do this?");
        }
    }

    public ResultSet ExecuteQuery(String query){
        Statement statement = null;
        ResultSet rs = null;
        query = query.replaceAll("'", "\\'"); //por favor
        query = query.replaceAll("\"", "\\\"");
        try {
            statement = connection.createStatement();
            rs = statement.executeQuery(query);
        } catch (SQLException ex) {
            System.out.println("Why you do this?");
        }
        return rs;
    }
    
    public ResultSet GetPatients(int ID,boolean all){
        String query = "select "
                            + "p.IDPerson,p.Name,p.BriefStatement,s.Sex,p.DateOfBirth,p.Telephone_1,p.Telephone_2,p.NextOfKinName,"
                            + "p.NextOfKinRelationshipWithPatient,p.NextOfKintelephone_1 ,p.NextOfKintelephone_2 ,kinC.City as 'relativeCity',"
                            + "kinS.State as 'relativeState',kinStr.Street as 'relativeStreet',kinDoor.Doorno as 'relativeDoorno',kinP.Pincode,"
                            + "permC.City as 'permCity',permS.State as 'permState',permStr.Street as 'permStreet',"
                            + "permDoor.Doorno as 'permDoor',permP.Pincode as 'permPincode',presC.City as 'presCity',presS.State as 'presState',"
                            + "presStr.Street as 'presStreet',presDoor.Doorno as 'presDoorno',presP.Pincode as 'presPincode',"
                            + "ms.MartialStatus,p.NumberOfDependents,p.Height,p.Weight,bt.BloodType,p.Occupation,p.GrossAnualIncome,"
                            + "p.Vegetarian,p.Smoker,p.AvgNumberOfCigarettesPerDay,p.Alcoholic,p.AvgNumberOfDrinksPerDay,p.Stimulants ,"
                            + "p.InfoAboutStimulants,p.AvgCoffieTeePerDay,p.AvgSoftDrinksPerDay,p.RegularMeals,ft.FoodType,"
                            + "p.HistoryOfPreviousTreatment,p.PhysicianTreated,p.HospitalTreated,p.Diabetic,dt.DiabeticType,p.Hypertensive,"
                            + "p.CardiacCondition,p.RespiratoryCondition,p.DigestiveCondition,p.OrthopedicCondition,p.MuscularCondition ,"
                            + "p.NeurologicalCondition,p.KnownAlergies ,p.KnownReactionToDrugs ,p.MajorSurgeries "
                        + " from PERSON as 'p' "
                            + " left join SEX as 's' on s.IDSex = p.SexID"
                            + " left join CITY as 'kinC' on kinC.IDCity=p.NextOfKinCityID"
                            + " left join CITY as 'permC' on permC.IDCity=p.PermanentCityID"
                            + " left join CITY as 'presC' on presC.IDCity=p.PresentCityID"
                            + " left join STATE as 'kinS' on kinS.IDState=p.NextOfKinStateID"
                            + " left join STATE as 'permS' on permS.IDState=p.PermanentStateID"
                            + " left join STATE as 'presS' on presS.IDState=p.PresentStateID"
                            + " left join PINCODE as 'kinP' on kinP.IDPincode=p.NextOfKinPincodeID"
                            + " left join PINCODE as 'permP' on permP.IDPincode=p.PermanentPincodeID"
                            + " left join PINCODE as 'presP' on presP.IDPincode=p.PresentPincodeID"
                            + " left join STREET as 'kinStr' on kinStr.IDStreet=p.NextOfKinStreetID"
                            + " left join STREET as 'permStr' on permStr.IDStreet=p.PermanentStreetID"
                            + " left join STREET as 'presStr' on presStr.IDStreet=p.PresentStreetID"
                            + " left join DOORNO as 'kinDoor' on kinDoor.IDDoorno=p.NextOfKinDoornoID"
                            + " left join DOORNO as 'permDoor' on permDoor.IDDoorno=p.PermanentDoornoID"
                            + " left join DOORNO as 'presDoor' on presDoor.IDDoorno=p.PresentDoornoID"
                            + " left join MARTIALSTATUS as 'ms' on ms.IDMartialStatus=p.MartialStatusID"
                            + " left join BLOODTYPE as 'bt' on bt.IDBloodType=p.BloodTypeID"
                            + " left join FOODTYPE as 'ft' on ft.IDFoodType=p.PredominantlyFoodTypeID"
                            + " left join DIABETICTYPE as 'dt' on dt.IDDiabeticType=p.DiabeticTypeID ";
        if(!all){
            query += ("where p.IDPerson = " + Integer.toString(ID));
        }
        return ExecuteQuery(query);
    }
    
    public void InsertMiniForm(String name,String sex, String date, String briefStatement, String tel1, String tel2, String relativeName, String relationRelative){
        ExecuteUpdate(MessageFormat.format(
                "INSERT INTO PERSON(Name,BriefStatement,SexID,DateOfBirth,Telephone_1,Telephone_2,NextOfKinName,NextOfKinRelationshipWithPatient) "
              + "VALUES(''{0}'', ''{1}'', {2}, ''{3}'', ''{4}'', ''{5}'', ''{6}'', ''{7}'')", name,briefStatement,sex,date,tel1,tel2,relativeName,relationRelative));
        System.out.println("[+] New patient added ");
    }
    
    public void InsertFullForm(){
        //todo
    }
    
    public boolean CheckIfPatientFormCompleted(int ID){
        ResultSet rs = ExecuteQuery("SELECT Height from PERSON where IDPerson = " + ID);
        String tmp = "definetly not null";
        try {
            tmp = rs.getString("Height");
        } catch (SQLException ex) {
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            return (tmp == null);
        }
    }
    
    public void OpenConnection(){
        try {
            connection = DriverManager.getConnection(URL_FORMAT);
        } catch (SQLException e) {
            CloseConnection();
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, e);
        }
        
    }
    
    public void CloseConnection(){
        try{
            connection.close();
        }catch(SQLException e){ 
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, e);
        }

    } 
}
