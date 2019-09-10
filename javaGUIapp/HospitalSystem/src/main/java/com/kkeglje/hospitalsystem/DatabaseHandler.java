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
        try {
            statement = connection.createStatement();
            statement.executeUpdate(query);
        } catch (SQLException ex) {
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public ResultSet ExecuteQuery(String query){
        Statement statement = null;
        ResultSet rs = null;
        try {
            statement = connection.createStatement();
            rs = statement.executeQuery(query);            
        } catch (SQLException ex) {
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
        return rs;
    }
    
    public void InsertMiniForm(String name,String sex, String date, String briefStatement, String tel1, String tel2, String relativeName, String relationRelative){
        //ExecuteQuery(MessageFormat.format("{0} {1} {2} {3} {4} {5} {6}", name,sex,date,briefStatement,tel1,tel2,relativeName,relationRelative));
        System.out.println(
                MessageFormat.format(
                        "name:{0} sex:{1} date:{2} brief:{3} tel1:{4} tel2:{5} name:{6} relation:{7}", 
                        name,sex,date,briefStatement,tel1,tel2,relativeName,relationRelative
                )
        );
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
