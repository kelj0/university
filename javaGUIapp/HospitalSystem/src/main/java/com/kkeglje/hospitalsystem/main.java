/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kkeglje.hospitalsystem;
import com.kkeglje.hospitalsystem.DatabaseHandler;
import java.util.Scanner;
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
        System.out.print("[1] Mini form\n"
                         + "[2] Comprehensive Form\n"
                         + "[3] Patients\n"
                         + "[q] Quit\n>");
    }    
    
    private static void MiniForm(){
        System.out.println("Mini form");
        dh.InsertMiniForm("testName", "sex", "date", "statement", "tel1", "tel2", "relativeName", "relationRelative");
    }
    
    private static void ComprehensiveForm(){
        System.out.println("Big form");
    }
    
    private static void Patients(){
        System.out.println("Patients");
    }
    
}
