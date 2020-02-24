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
import datahandlers.csvDataHandler;
import datahandlers.pdfDataHandler;
import datahandlers.xmlDataHandler;
import java.nio.file.Paths;
/**
 *
 * @author keljo
 */
public class main {
    public static dbHandler dh = dbHandler.getInstance();
    public static final String DATA_DIRECTORY_PATH = Paths.get(System.getProperty("user.dir"),"DATA").toString();
    public static void main(String[] args) throws SQLException {
        // vozila/vozaci from CSV to database 
        csvDataHandler cdh = new csvDataHandler();
        System.out.println("Uvoz vozaca..");
        int n = cdh.importVozaci(Paths.get(DATA_DIRECTORY_PATH,"vozaci.csv").toString());
        System.out.println("Broj uvezenih vozaca: " + n);
        
        System.out.println("Uvoz vozila..");
        n = cdh.importVozila(Paths.get(DATA_DIRECTORY_PATH,"vozila.csv").toString());
        System.out.println("Broj uvezenih vozila: " + n);
        
        // rute Import/export to XML
        xmlDataHandler xdh = new xmlDataHandler();
        System.out.println("Uvoz ruta..");
        n = xdh.importRute(Paths.get(DATA_DIRECTORY_PATH,"rute.xml").toString());
        System.out.println("Broj uvezenih ruta: " + n);
        
        System.out.println("Izvoz ruta..");
        n = xdh.exportRute(1,DATA_DIRECTORY_PATH,"EXPORT_rute.xml");
        System.out.println("Broj izvezenih ruta: " + n);
    
        System.out.println("Generiranje PDF-a");
        pdfDataHandler pdh = new pdfDataHandler();
        pdh.generatePutniNalogPdf(1,DATA_DIRECTORY_PATH,"putni_nalog_1_report.pdf");
        System.out.println("PDF generiran..");
    }
}
