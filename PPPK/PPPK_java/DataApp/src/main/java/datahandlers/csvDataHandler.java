/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package datahandlers;

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
    public void importVozaci(String filename){
        String line = "";
        String cvsSplitBy = ",";

        try (BufferedReader br = new BufferedReader(new FileReader(filename))) {
            while ((line = br.readLine()) != null) {
                String[] data = line.split(cvsSplitBy);
                int id = Integer.parseInt(data[0]);
                int tip_vozila_id = Integer.parseInt(data[1]);
                String marka = data[2];
                int godina_proizvodnje = Integer.parseInt(data[3]);
                double pocetni_km = Double.parseDouble(data[4]);
                double trenutni_km = Double.parseDouble(data[5]);
                
                Vozilo v = new Vozilo(id,tip_vozila_id,marka,godina_proizvodnje,pocetni_km,trenutni_km);
                
                //dbHandler.addVozilo(v);
            }

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    
    public void importVozila(String filename){
        String line = "";
        String cvsSplitBy = ",";

        try (BufferedReader br = new BufferedReader(new FileReader(filename))) {
            while ((line = br.readLine()) != null) {
                String[] data = line.split(cvsSplitBy);
                int id = Integer.parseInt(data[0]);
                String ime =           data[1];
                String prezime =       data[2];
                String broj_mobitela = data[3];
                String broj_vozacke =  data[4];
                
                Vozac v = new Vozac(id,ime,prezime,broj_mobitela,broj_vozacke);
                //dbHandler.addVozac(v);
            }

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
