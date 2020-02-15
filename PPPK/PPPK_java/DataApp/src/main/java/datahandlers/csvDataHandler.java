/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package datahandlers;

import db.dbHandler;
import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import javafx.scene.shape.Path;
import models.Vozac;
import models.Vozilo;

/**
 *
 * @author keljo
 */
public class csvDataHandler {
    private static dbHandler db = dbHandler.getInstance();
    public int importVozila(String filename){
        String line = "";
        String cvsSplitBy = ",";
        int broj = 0;
        try (BufferedReader br = new BufferedReader(new FileReader(filename))) {
            while ((line = br.readLine()) != null) {
                String[] data = line.split(cvsSplitBy);
                int tip_vozila_id = Integer.parseInt(data[0]);
                String marka = data[1];
                int godina_proizvodnje = Integer.parseInt(data[2]);
                double pocetni_km = Double.parseDouble(data[3]);
                double trenutni_km = Double.parseDouble(data[4]);
                
                
                Vozilo v = new Vozilo(tip_vozila_id,marka,godina_proizvodnje,pocetni_km,trenutni_km);
                db.InsertVozilo(v);
                ++broj;
            }
        } catch (IOException e) {
            e.printStackTrace();
        }finally{
            return broj;
        }
    }
    
    public int importVozaci(String filename){
        String line = "";
        String cvsSplitBy = ",";
        int broj = 0;
        try (BufferedReader br = new BufferedReader(new FileReader(filename))) {
            while ((line = br.readLine()) != null) {
                String[] data = line.split(cvsSplitBy);
                String ime =           data[0];
                String prezime =       data[1];
                String broj_mobitela = data[2];
                String broj_vozacke =  data[3];
                
                Vozac v = new Vozac(ime,prezime,broj_mobitela,broj_vozacke);
                db.InsertVozac(v);
                ++broj;
            }
        } catch (IOException e) {
            e.printStackTrace();
        }finally{
            return broj;
        }
    }
}
