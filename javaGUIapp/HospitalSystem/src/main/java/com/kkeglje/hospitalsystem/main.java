/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kkeglje.hospitalsystem;
import com.kkeglje.hospitalsystem.DatabaseHandler;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.MessageFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;
/**
 *
 * @author keljo
 */
public class main { 
    private static DatabaseHandler dh = DatabaseHandler.getInstance();
    public static void main(String args[]){
        
        boolean quit = false;
        Scanner scan = new Scanner(System.in);
        do{
            PrintChooserMenue();
            switch(scan.next()){
                case "1":
                    MiniForm();
                    break;
                case "2":
                    ComprehensiveForm();
                    break;
                case "3":
                    Patients();
                    break;
                case "q":
                    quit = true;
                    break;
                default:
                    System.out.println("Wrong input!");
                    break;
            }
        }while(!quit);
    }
    
    private static void PrintChooserMenue(){
        System.out.print("=================\n[1] Mini form\n"
                         + "[2] Comprehensive Form\n"
                         + "[3] Patients\n"
                         + "[q] Quit\n>");
    }    
    
    private static void MiniForm(){
        Scanner scan = new Scanner(System.in);
        System.out.println("========\nMini form\n========");
        String name,sex,date,statement,tel1,tel2,relativeName,relationRelative;
        System.out.println("Enter your Surname MiddleName Name");
        name = scan.nextLine();
        Boolean check = false;
        do{
            scan = new Scanner(System.in);
            dh.OpenConnection();
            ResultSet rs = dh.ExecuteQuery("select * from SEX");
            System.out.println("Your gender?");
            ArrayList<Integer> allowedNumbers = new ArrayList<Integer>();
            try {
                while(rs.next()){
                    int ID = rs.getInt("IDSex");
                    System.out.printf("%d) %s\n",ID,rs.getString("Sex"));
                    allowedNumbers.add(ID);
                }
            } catch (SQLException ex) {
                Logger.getLogger(main.class.getName()).log(Level.SEVERE, null, ex);
            }finally{
                dh.CloseConnection();
            }
            
            sex = scan.next();
            
            if(allowedNumbers.contains(Integer.parseInt(sex))){
                check=true;
            }else{
                System.out.println("Wrong selection!");
            }
        }while(!check);
        
        String day,month,year;
        check = false;
        scan = new Scanner(System.in);
        do{
            System.out.print("Day of birth(01-31)\n>");
            day = scan.next();
            System.out.print("Month of birth(01-12)\n>");
            month = scan.next();
            System.out.print("Year of birth(yyyy)\n>");
            year = scan.next();
            if( (0 < Integer.parseInt(day) && Integer.parseInt(day) <= 31) &&
                (0 < Integer.parseInt(month) && Integer.parseInt(month) <= 12) &&
                (1900 < Integer.parseInt(year) && Integer.parseInt(year) <= Calendar.getInstance().get(Calendar.YEAR))
              ){
                check = true;
            }else{
                System.out.println("Date is out of range!");
            }
            
        }while(!check);
        date = year + "-" + month + "-" + day + " 10:00:00";
        
        scan = new Scanner(System.in);
        System.out.println("Brief statement");
        statement = scan.nextLine();
        
        check = false;
        do{
            scan = new Scanner(System.in);
            System.out.print("Phone1 \n>");
            tel1 = scan.nextLine();
            scan = new Scanner(System.in);
            System.out.print("Phone2 \n>");
            tel2 = scan.nextLine();
            if(tel1.matches("[a-zA-Z]") || tel2.matches("[a-zA-Z]")){
                System.out.println("Invalid numbers!");
            }else{
                check = true;
            }
        }while(!check);
        scan = new Scanner(System.in);
        System.out.println("Enter your relative Surname MiddleName Name");
        relativeName = scan.nextLine();
        scan = new Scanner(System.in);
        System.out.println("Enter your relation with relative");
        relationRelative = scan.nextLine();
        dh.OpenConnection();
        dh.InsertMiniForm(name, sex, date, statement, tel1, tel2, relativeName, relationRelative);
        dh.CloseConnection();
    }
    
    private static void ComprehensiveForm(){
        System.out.println("Implement me");
    }
    
    private static void Patients(){
        Scanner scan = new Scanner(System.in);
        ArrayList<String> allowedIDs = new ArrayList<String>();

        System.out.println("[ID] Surname Middle Name | Brief statement | Relative Name | State | City | Street address");
        dh.OpenConnection();
        ResultSet rs = dh.GetPatients(0,true);
        try {
            while(rs.next()){
                allowedIDs.add(rs.getString("IDPerson"));
                if(!dh.CheckIfPatientFormCompleted(rs.getInt("IDPerson"))){
                    System.out.println(
                            MessageFormat.format(
                                    "[{0}] {1} | {2} | {3} | {4} | {5} | {6} {7}, {8}\n===============\n",rs.getInt("IDPerson"),rs.getString("Name"),rs.getString("briefStatement"),
                                    rs.getString("NextOfKinName"), rs.getString("presState") , rs.getString("presCity"), rs.getString("presStreet"),
                                    rs.getString("presDoorno"), rs.getString("presPincode")
                            )
                    );
                }else{
                    System.out.println(
                            MessageFormat.format(
                                    "[{0}] Name: {1},Telephone: {4} \nStatement: {2},\nRelative name: {3} \nOnly mini form done!\n===============\n",
                                    rs.getInt("IDPerson"),rs.getString("Name"),rs.getString("briefStatement"),
                                    rs.getString("NextOfKinName"), rs.getString("Telephone_1")
                            )
                    );
                }
            }
        } catch (SQLException ex) {
            Logger.getLogger(main.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            dh.CloseConnection();
        }
        
        scan = new Scanner(System.in);
        System.out.print("For more info enter [ID] of patient\nTo go back enter 'q'\n>");
        do{
            String tmp = scan.next();
            if("q".equals(tmp)){
                break;
            }else if(allowedIDs.contains(tmp)){
                PatientMoreInfo(Integer.parseInt(tmp));
                return;
            }else{
                System.out.print("Wrong input!\n>");
            }
            scan = new Scanner(System.in);
        }while(true);
    }
    
    public static void PatientMoreInfo(int ID){
        dh.OpenConnection();
        ResultSet rs = dh.GetPatients(ID, false);
        try {
            rs.next();
        } catch (SQLException ex) {
            Logger.getLogger(main.class.getName()).log(Level.SEVERE, null, ex);
        }
        try {
            System.out.println(
                    MessageFormat.format(
                            "Name: {0}\n Brief statement:{1}\n Sex: {2}\n Birth: {3}\n Tel1:{4} Tel2:{5}\n "
                          + "State: {10}\n City: {11}\n Vegetarian: {12}\n Gross anual income: ${13}k"
                          + "\n==========\nRelative\n==========\n Name: {6} Relationship: {7}\n City{8}\n State: {9}\n",
                            rs.getString("Name"), rs.getString("BriefStatement"), rs.getString("Sex"), rs.getString("DateOfBirth"), rs.getString("Telephone_1"),
                            rs.getString("Telephone_2"), rs.getString("NextOfKinName"), rs.getString("NextOfKinRelationshipWithPatient"), rs.getString("relativeCity"), rs.getString("relativeState"),
                            rs.getString("presState"), rs.getString("presCity"), rs.getString("Vegetarian"), rs.getString("GrossAnualIncome")
                    ));
        } catch (SQLException ex) {
            Logger.getLogger(main.class.getName()).log(Level.SEVERE, null, ex);
        }finally{
            dh.CloseConnection();
        }
        System.out.println("=====================");
        /*switch(){
            case ovisno o inputu, nakon switcha updatea person tablicu sa updateanim stvarima
        }*/
    }
}
