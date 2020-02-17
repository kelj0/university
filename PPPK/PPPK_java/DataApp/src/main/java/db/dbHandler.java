/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package db;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.logging.Level;
import java.util.logging.Logger;
import models.Ruta;
import models.Vozac;
import models.Vozilo;

/**
 *
 * @author keljo
 */
public class dbHandler {
    private static final String DEFAULT_JAVA_CLASS = "com.microsoft.sqlserver.jdbc.SQLServerDriver";
    private final String PROJECT_DIRECTORY = System.getProperty("user.dir");
    private static final String URL_FORMAT = "jdbc:sqlserver://KELJO-PC\\SQLEXPRESS;databaseName=PPPK_DATABASE;user=sa;password=SQL"; //CHANGE ME
    private static dbHandler instance = null;
    private static Connection connection = null;
    
    private dbHandler(String url) throws ClassNotFoundException{
        Class.forName(url);
    }
    
    public static dbHandler getInstance(){
        if(instance == null){
            try {
                instance = new dbHandler(DEFAULT_JAVA_CLASS);
            } catch (ClassNotFoundException ex) {
                Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, ex);
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
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, ex);
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
            OpenConnection();
            statement = connection.createStatement();
            statement.executeUpdate(query);
        } catch (SQLException ex) {
            ex.printStackTrace();
        }finally{
            CloseConnection();
        }
    }

    public ResultSet ExecuteQuery(String query){
        Statement statement = null;
        ResultSet rs = null;
        try {
            OpenConnection();
            statement = connection.createStatement();
            rs = statement.executeQuery(query);
        } catch (SQLException ex) {
            System.out.println("Why you do this?");
        }finally{
            CloseConnection();
            return rs;
        }
    }
    
    private void OpenConnection(){
        try {
            connection = DriverManager.getConnection(URL_FORMAT);
        } catch (SQLException e) {
            CloseConnection();
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, e);
        }
        
    }
    
    private void CloseConnection(){
        try{
            connection.close();
        }catch(SQLException e){ 
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, e);
        }

    } 
    
    
    public void InsertVozac(Vozac v){
        CallableStatement cstmt = null;
        ResultSet rs = null;
        try{
            OpenConnection();
            cstmt = connection.prepareCall("{call insert_vozac(?,?,?,?)}");
            cstmt.setString("ime", v.getIme());
            cstmt.setString("prezime", v.getPrezime());
            cstmt.setString("broj_mobitela", v.getBroj_mobitela());
            cstmt.setString("broj_vozacke", v.getBroj_vozacke());
            cstmt.execute();
        }catch(Exception ex){
            ex.printStackTrace();
        }finally{
            CloseConnection();
        }
    }
    
    public void InsertVozilo(Vozilo v){
        CallableStatement cstmt = null;
        try{
            OpenConnection();
            cstmt = connection.prepareCall("{call insert_vozilo(?,?,?,?,?)}");
            cstmt.setString("marka", v.getMarka());
            cstmt.setInt("tip_vozila_id", v.getTip_vozila_id());
            cstmt.setDouble("pocetni_km", v.getPocetni_km());
            cstmt.setDouble("trenutni_km", v.getTrenutni_km());
            cstmt.setInt("godina_proizvodnje", v.getGodina_proizvodnje());
            cstmt.execute();
        }catch(SQLException ex){
            ex.printStackTrace();
        }finally{
            CloseConnection();
        }
    }

    public void InsertRuta(Ruta r) {
        CallableStatement cstmt = null;
        try{
            OpenConnection();
            cstmt = connection.prepareCall("{call insert_ruta(?,?,?,?,?,?,?)}");
            cstmt.setInt("putni_nalog_id", r.getPutni_nalog_id());
            cstmt.setDouble("x_koordinata_a", r.getX_koordinata_a());
            cstmt.setDouble("y_koordinata_a", r.getY_koordinata_a());
            cstmt.setDouble("x_koordinata_b", r.getX_koordinata_b());
            cstmt.setDouble("y_koordinata_b", r.getY_koordinata_b());
            cstmt.setDouble("km_izmedu_a_b", r.getKm_izmedu_a_b());
            cstmt.setDouble("prosjecna_brzina", r.getProsjecna_brzina());
            cstmt.execute();
        }catch (SQLException ex){
            ex.printStackTrace();
        }finally{
            CloseConnection();
        }
        
    }

    public ArrayList<Ruta> SelectRute(int putni_nalog_id) {
        ArrayList<Ruta> l = new ArrayList<Ruta>();
        ResultSet rs = null;
        Statement statement = null;
        try {
            OpenConnection();
            statement = connection.createStatement();
            rs = statement.executeQuery("SELECT * FROM RUTA WHERE putni_nalog_id=" + putni_nalog_id);
            while(rs.next()){
                Ruta r = new Ruta(
                        rs.getInt("id"),
                        rs.getInt("putni_nalog_id"),
                        rs.getDouble("x_koordinata_a"),
                        rs.getDouble("y_koordinata_a"),
                        rs.getDouble("x_koordinata_b"),
                        rs.getDouble("y_koordinata_b"),
                        rs.getDouble("km_izmedu_a_b"),
                        rs.getDouble("prosjecna_brzina")
                );
                l.add(r);
            }
        } catch (SQLException ex) {
            Logger.getLogger(dbHandler.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            CloseConnection();
        }
        return l;
    }
}
