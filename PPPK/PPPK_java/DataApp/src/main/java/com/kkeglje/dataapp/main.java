/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kkeglje.dataapp;

import db.dbHandler;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import com.microsoft.sqlserver.jdbc.SQLServerDriver;
/**
 *
 * @author keljo
 */
public class main {
    public static dbHandler dh = dbHandler.getInstance();
    public static void main(String[] args) throws SQLException {
        // vozila/vozaci from CSV to database 
        
        
        System.out.println("Test");
        
        // rute Import/export to XML
        
        // backup db to XML
        // restoredb
        
        // putni nalog generate PDF (hibernate)
        
    }
}
