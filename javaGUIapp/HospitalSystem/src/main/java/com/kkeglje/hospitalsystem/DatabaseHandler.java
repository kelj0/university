/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kkeglje.hospitalsystem;

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
        OpenConnection();
        ExecuteScript(PROJECT_DIRECTORY + "/databse/build-db.sql");
        CloseConnection();
    }
    
    public static DatabaseHandler getInstance() throws ClassNotFoundException{
        if(instance == null){
            instance = new DatabaseHandler(DEFAULT_JAVA_CLASS);
        }
        return instance;
    }
    
    public static void ExecuteScript(String path){
        try {
            String content = new String(Files.readAllBytes(Paths.get(path)));
            Statement statement = connection.createStatement();
            statement.setQueryTimeout(30);
            for(String query : content.split(";")){
                ResultSet rs = statement.executeQuery(query);
            }
        } catch (IOException | SQLException ex) {
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public static void OpenConnection(){
        try {
            connection = DriverManager.getConnection(URL_FORMAT);
        } catch (SQLException e) {
            CloseConnection();
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, e);
        }
        
    }
    
    public static void CloseConnection(){
        try{
            connection.close();
        }catch(SQLException e){ 
            Logger.getLogger(DatabaseHandler.class.getName()).log(Level.SEVERE, null, e);
        }

    } 
}
